using Microsoft.AspNetCore.Mvc;

namespace SantoPecado.Server
{
    [Route("burgers")]
    [ApiController]
    public class BurgersController : Controller
    {
        private readonly BurgerStoreContext _db;

        public BurgersController(BurgerStoreContext db)
        {
            _db = db;
        }
    }
}
