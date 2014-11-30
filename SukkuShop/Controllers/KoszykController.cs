using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SukkuShop.Models;

namespace SukkuShop.Controllers 
{

    public partial class KoszykController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        
        public KoszykController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        //[OutputCache(Duration = 1)]
        public virtual ActionResult AddToCart(int id, Cart shoppingCart, int quantity = 1)
        {
            shoppingCart.AddItem(id, quantity);
            var value = CalcTotalValue(shoppingCart);
            return PartialView(MVC.Shared.Views._CartInfoPartialView,value.ToString("c"));
        }

        public virtual ActionResult RemoveFromCart(int id, Cart shoppingCart)
        {
            shoppingCart.RemoveLine(id);
            return RedirectToAction(MVC.Koszyk.Index());
        }
        //[OutputCache(Duration = 1)]
        public virtual ActionResult TotalValue(Cart shoppingCart)
        {
            var value = CalcTotalValue(shoppingCart);
            return PartialView(MVC.Shared.Views._CartInfoPartialView, value.ToString("c"));
        }

        private decimal CalcTotalValue(Cart shoppingCart)
        {
            decimal sum = 0;
            foreach (Cart.CartLine line in shoppingCart.Lines)
            {
                Products firstOrDefault = _dbContext.Products.FirstOrDefault(e => e.ProductId == line.Id);
                if (firstOrDefault != null) sum += (firstOrDefault.Price - ((firstOrDefault.Price*firstOrDefault.Promotion)/100))*line.Quantity ?? firstOrDefault.Price*line.Quantity;
            }
            return sum;
        }

        // GET: Cart
        public virtual ActionResult Index(Cart shoppingCart)
        {
            var productList = new List<CartProduct>();
            decimal totalValue = 0;
            foreach (var item in shoppingCart.Lines)
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.ProductId == item.Id);
                if (product == null) continue;
                productList.Add(new CartProduct
                {
                    Id = product.ProductId,
                    Description = product.Description,
                    Name = product.Name,
                    Price =
                        ((product.Price - ((product.Price*product.Promotion)/100)) ?? product.Price).ToString("c"),
                    Quantity = item.Quantity,
                    TotalValue =
                        (((product.Price - ((product.Price*product.Promotion)/100)) ?? product.Price)*item.Quantity)
                            .ToString("c"),
                            Image = product.ImageName
                            
                });
                totalValue += ((product.Price - ((product.Price*product.Promotion)/100)) ?? product.Price)*
                              item.Quantity;
            }
            var model = new CartViewModels
            {
                CartProductsList = productList,
                TotalValue = totalValue.ToString("c")
            };
            return View(model);
            
        }
    }
}