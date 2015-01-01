using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using SukkuShop.Models;

namespace SukkuShop.Controllers
{
    public partial class ZamowienieController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ZamowienieController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual ActionResult Krok1(Cart shoppingCart)
        {
            if (!shoppingCart.Lines.Any())
                return RedirectToAction(MVC.Koszyk.Index());
            var model = OrderViewModels(shoppingCart);
            return View(model);
        }
        public virtual ActionResult Krok2(Cart shoppingCart)
        {
            if (shoppingCart.PaymentId == 0 || shoppingCart.ShippingId == 0)
                return RedirectToAction(MVC.Zamowienie.Krok1());
            if (!shoppingCart.Lines.Any())
                return RedirectToAction(MVC.Koszyk.Index());
            return View();
        }
        public virtual ActionResult Podsumowanie()
        {
            return View();
        }

        public void SetShipping(Cart shoppingCart,int id)
        {
            shoppingCart.ShippingId = id;            
        }

        public void SetPayment(Cart shoppingCart, int id)
        {
            shoppingCart.PaymentId = id;
        }

        public void SaveToDatabase(Cart shoppingCart)
        {
            
            Orders Orders = new Orders();
            Orders.OrderDate = DateTime.Now;
            Orders.ShippingId = shoppingCart.ShippingId;
            Orders.PaymentId = shoppingCart.PaymentId;
            // Orders.SentDate -> panel admina [czeckbox, że wysłane]?
            // Orders.OrderInfo -> wtf tutaj?
            // Orders.ProductsPrice -> suma SubTotalPrice z Order Details
            // Orders.TotalPrice -> ProductsPrice - Discount
            // Orders.Discout -> to z użytkownika jakoś trzeba brać
            // Orders.Name -> jw
            // Orders.Surname -> jw
            // Orders.SpecialAddress -> jw [co to w ogole jest]
            Orders.City = shoppingCart.OrderAdress.City;
            Orders.Street = shoppingCart.OrderAdress.Street;
            Orders.Number = shoppingCart.OrderAdress.Number;
            Orders.PostalCode = shoppingCart.OrderAdress.PostalCode;
            
            foreach (var item in shoppingCart.Lines)
            {
                OrderDetails OrderD = new OrderDetails();
                OrderD.ProductId = item.Id;
                OrderD.Quantity = item.Quantity;
                var productPrice = _dbContext.Products.First(i => i.ProductId == item.Id).Price;
                OrderD.SubTotalPrice += item.Quantity*productPrice;
                _dbContext.OrderDetails.Add(OrderD);
            }
        }

        private OrderViewModels OrderViewModels(Cart shoppingCart)
        {
            var productList = new List<OrderItem>();
            decimal totalValue = 0;
            foreach (var item in shoppingCart.Lines)
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.ProductId == item.Id);
                if (product == null) continue;
                productList.Add(new OrderItem
                {
                    Name = product.Name,
                    Price =
                        ((product.Price - ((product.Price * product.Promotion) / 100)) ?? product.Price).ToString("c"),
                    Quantity = item.Quantity,
                    TotalValue =
                        (((product.Price - ((product.Price * product.Promotion) / 100)) ?? product.Price) * item.Quantity)
                            .ToString("c")
                });
                totalValue += ((product.Price - ((product.Price * product.Promotion) / 100)) ?? product.Price) *
                              item.Quantity;
            }
            var model = new OrderViewModels
            {
                OrderProductList = productList,
                TotalValue = totalValue.ToString("c").Replace(",", ".")
            };
            return model;
        }
    }
}