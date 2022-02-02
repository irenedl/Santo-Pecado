using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebPush;
using System.Text.Json;

namespace SantoPecado.Server
{
    [Route("orders")]
    [ApiController]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly BurgerStoreContext _db;

        public OrdersController(BurgerStoreContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderWithStatus>>> GetOrders()
        {
            var orders = await _db.Orders
                .Where(o => o.UserId == GetUserId())
                .Include(o => o.DeliveryLocation)
                .Include(o => o.Burgers).ThenInclude(p => p.Special)
                .Include(o => o.Burgers).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
                .OrderByDescending(o => o.CreatedTime)
                .ToListAsync();

            return orders.Select(o => OrderWithStatus.FromOrder(o)).ToList();
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderWithStatus>> GetOrderWithStatus(int orderId)
        {
            var order = await _db.Orders
                .Where(o => o.OrderId == orderId)
                .Where(o => o.UserId == GetUserId())
                .Include(o => o.DeliveryLocation)
                .Include(o => o.Burgers).ThenInclude(p => p.Special)
                .Include(o => o.Burgers).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
                .SingleOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            return OrderWithStatus.FromOrder(order);
        }

        [HttpPost]
        public async Task<ActionResult<int>> PlaceOrder(Order order)
        {
            order.CreatedTime = DateTime.Now;
            order.DeliveryLocation = new LatLong(36.8463311, -2.4500576);
            order.UserId = GetUserId();

            // Enforce existence of Burger.SpecialId and Topping.ToppingId
            // in the database - prevent the submitter from making up
            // new specials and toppings
            foreach (var burger in order.Burgers)
            {
                burger.SpecialId = burger.Special.Id;
                burger.Special = null;

                foreach (var topping in burger.Toppings)
                {
                    topping.ToppingId = topping.Topping.Id;
                    topping.Topping = null;
                }
            }

            _db.Orders.Attach(order);
            await _db.SaveChangesAsync();

            // In the background, send push notifications if possible
            var subscription = await _db.NotificationSubscriptions.Where(e => e.UserId == GetUserId()).SingleOrDefaultAsync();
            if (subscription != null)
            {
                _ = TrackAndSendNotificationsAsync(order, subscription);
            }

            return order.OrderId;
        }

        private string GetUserId()
        {
            return HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private static async Task TrackAndSendNotificationsAsync(Order order, NotificationSubscription subscription)
        {
            // In a realistic case, some other backend process would track
            // order delivery progress and send us notifications when it
            // changes. Since we don't have any such process here, fake it.
            await Task.Delay(OrderWithStatus.PreparationDuration);
            await SendNotificationAsync(order, subscription, "Su pedido ha sido enviado.");

            await Task.Delay(OrderWithStatus.DeliveryDuration);
            await SendNotificationAsync(order, subscription, "Su pedido ha sido entregado. ¡Disfrútelo!");
        }

        private static async Task SendNotificationAsync(Order order, NotificationSubscription subscription, string message)
        {
            // For a real application, generate your own
            var publicKey = "BLC8GOevpcpjQiLkO7JmVClQjycvTCYWm6Cq_a7wJZlstGTVZvwGFFHMYfXt6Njyvgx_GlXJeo5cSiZ1y4JOx1o";
            var privateKey = "OrubzSz3yWACscZXjFQrrtDwCKg-TGFuWhluQ2wLXDo";

            var pushSubscription = new PushSubscription(subscription.Url, subscription.P256dh, subscription.Auth);
            var vapidDetails = new VapidDetails("mailto:<someone@example.com>", publicKey, privateKey);
            var webPushClient = new WebPushClient();
            try
            {
                var payload = JsonSerializer.Serialize(new
                {
                    message,
                    url = $"myorders/{order.OrderId}",
                });
                await webPushClient.SendNotificationAsync(pushSubscription, payload, vapidDetails);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error al enviar notificación push: " + ex.Message);
            }
        }
    }
}
