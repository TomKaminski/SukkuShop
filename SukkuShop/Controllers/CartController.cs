using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
        //[OutputCache(Duration = 1)]
        public ActionResult AddToCart(int id, Cart shoppingCart, int quantity = 1)
        {
            shoppingCart.AddItem(id, quantity);
            var value = CalcTotalValue(shoppingCart);
            return PartialView("_CartInfoPartialView",value.ToString("c"));
        }
        //[OutputCache(Duration = 1)]
        public ActionResult TotalValue(Cart shoppingCart)
        {
            var value = CalcTotalValue(shoppingCart);
            return PartialView("_CartInfoPartialView", value.ToString("c"));
        }

        private decimal CalcTotalValue(Cart shoppingCart)
        {
            return (from line in shoppingCart.Lines let firstOrDefault = _dbContext.Products.FirstOrDefault(e => e.ProductId == line.Id) where firstOrDefault != null select (firstOrDefault.Price - ((firstOrDefault.Price*firstOrDefault.Promotion)/100))*line.Quantity ?? firstOrDefault.Price*line.Quantity).Sum();
        }

        // GET: Cart
        public ActionResult Index(Cart shoppingCart)
        {
        //chcesz mi powiedizec, ze jestem debilem i sie nie nadaje. ok - przyjalem
            Products tmpProducts = _dbContext.Products.Where(x=>x.ProductId ==)
            CartViewModels CVM = new CartViewModels();
            foreach (var item in shoppingCart.Lines)
            {
               CVM.CartProductsList.Add(new CartProduct());
            }
            return View();
            
        }
    }
}