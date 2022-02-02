using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SantoPecado.Server
{
    [Route("notifications")]
    [ApiController]
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly BurgerStoreContext _db;

        public NotificationsController(BurgerStoreContext db)
        {
            _db = db;
        }

        [HttpPut("subscribe")]
        public async Task<NotificationSubscription> Subscribe(NotificationSubscription subscription)
        {
            // Storing at most one subscription per user, so delete old ones.
            var userId = GetUserId();
            var oldSubscriptions = _db.NotificationSubscriptions.Where(e => e.UserId == userId);
            _db.NotificationSubscriptions.RemoveRange(oldSubscriptions);

            // Store new subscription
            subscription.UserId = userId;
            _db.NotificationSubscriptions.Attach(subscription);

            await _db.SaveChangesAsync();
            return subscription;
        }

        private string GetUserId()
        {
            return HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
