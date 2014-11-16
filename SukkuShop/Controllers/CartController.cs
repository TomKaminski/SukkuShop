using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ActionResult AddToCart(int ProductId, Cart shoppingCart, int Quantity = 1)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.ProductId == ProductId);
            shoppingCart.AddItem(new Cart.CartProduct
            {
                Id=product.ProductId,
                Name = product.Name,
                Price = product.Price,
                QuantityInStock = product.Quantity             
            },Quantity);
            return View();
        }
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
    }
}