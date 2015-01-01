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
            var result = CartViewModels(shoppingCart);
            return PartialView("CartTable",result);
        }

        public virtual JsonResult IncreaseQuantity(int id, Cart shoppingCart)
        {
            
            var firstOrDefault = _dbContext.Products.Where(x => x.ProductId == id).Select(k => new
            {
                k.Price,
                k.Quantity,
                k.Promotion
            }).First();
            
            var quantity = shoppingCart.Lines.FirstOrDefault(x => x.Id == id).Quantity;
            if (firstOrDefault.Quantity <= quantity) return Json(false);
            shoppingCart.AddItem(id);
            var data = (firstOrDefault.Price - ((firstOrDefault.Price*firstOrDefault.Promotion)/100))*(quantity+1) ??
                       firstOrDefault.Price*(quantity+1);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult DecreaseQuantity(int id, Cart shoppingCart)
        {
            
            shoppingCart.DecreaseQuantity(id);
            var firstOrDefault = _dbContext.Products.Where(x => x.ProductId == id).Select(k => new
            {
                k.Price,
                k.Quantity,
                k.Promotion
            }).First();
            var quantity = shoppingCart.Lines.FirstOrDefault(x => x.Id == id).Quantity;
            var data = (firstOrDefault.Price - ((firstOrDefault.Price * firstOrDefault.Promotion) / 100)) * quantity ?? firstOrDefault.Price * quantity;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult TotalPriceJson(Cart cart)
        {
            var data = CalcTotalValue(cart);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

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
                var firstOrDefault = _dbContext.Products.FirstOrDefault(e => e.ProductId == line.Id);
                if (firstOrDefault != null) sum += (firstOrDefault.Price - ((firstOrDefault.Price*firstOrDefault.Promotion)/100))*line.Quantity ?? firstOrDefault.Price*line.Quantity;
            }
            return sum;
        }

        // GET: Cart
        public virtual ActionResult Index(Cart shoppingCart)
        {
            var model = CartViewModels(shoppingCart);
            return View(model);
        }

        private CartViewModels CartViewModels(Cart shoppingCart)
        {
            var productList = new List<CartProduct>();
            decimal totalValue = 0;
            foreach (var item in shoppingCart.Lines)
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.ProductId == item.Id);
                var categoryName = _dbContext.Categories.FirstOrDefault(x => x.CategoryId == product.CategoryId).Name;
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
                    Image = product.ImageName,
                    MaxQuantity = product.Quantity,
                    CategoryName = categoryName
                });
                totalValue += ((product.Price - ((product.Price*product.Promotion)/100)) ?? product.Price)*
                              item.Quantity;
            }
            var model = new CartViewModels
            {
                CartProductsList = productList,
                TotalValue = totalValue.ToString("c").Replace(",",".")
            };
            return model;
        }
    }
}