using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json.Schema;
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
            var value = CalcTotalValue(shoppingCart);
            return PartialView("_CartInfoPartialView",value.ToString("c"));
        }

        public ActionResult TotalValue(Cart shoppingCart)
        {
            var value = CalcTotalValue(shoppingCart);
            return PartialView("_CartInfoPartialView", value.ToString("c"));
        }

        private decimal CalcTotalValue(Cart shoppingCart)
        {
            decimal value = 0;
            foreach (var line in shoppingCart.Lines)
            {
                var firstOrDefault = _dbContext.Products.FirstOrDefault(e => e.ProductId == line.Id);
                if (firstOrDefault != null)
                    value += (firstOrDefault.Price - ((firstOrDefault.Price*firstOrDefault.Promotion)/100))*line.Quantity ??
                             firstOrDefault.Price*line.Quantity;
            }
            return value;
        }

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
    }
}