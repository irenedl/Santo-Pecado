using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantoPecado.Server
{
    [Route("toppings")]
    [ApiController]
    public class ToppingsController : Controller
    {
        private readonly BurgerStoreContext _db;

        public ToppingsController(BurgerStoreContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Topping>>> GetToppings()
        {
            return await _db.Toppings.OrderBy(t => t.Name).ToListAsync();
        }
    }
}
