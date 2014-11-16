using System.Linq;
using System.Web.Mvc;
using SukkuShop.Models;

namespace SukkuShop.Controllers 
{
   
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        
        public CartController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public ActionResult AddToCart(int id, Cart shoppingCart, int quantity = 1)
        {
            shoppingCart.AddItem(id, quantity);
            return RedirectToAction("Produkty", "Sklep");
        }
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
    }
}