using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SukkuShop.Identity;
using SukkuShop.Models;

namespace SukkuShop.Controllers
{
    public partial class ZamowienieController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ApplicationUserManager _userManager;

        public ZamowienieController(ApplicationUserManager userManager,ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public virtual ActionResult Krok1(Cart shoppingCart)
        {
            if (!shoppingCart.Lines.Any())
                return RedirectToAction(MVC.Koszyk.Index());
            var model = OrderViewModels(shoppingCart);
            return View(model);
        }
        public async virtual Task<ActionResult>Krok2(Cart shoppingCart)
        {
            if (shoppingCart.PaymentId == 0 || shoppingCart.ShippingId == 0)
                return RedirectToAction(MVC.Zamowienie.Krok1());
            if (!shoppingCart.Lines.Any())
                return RedirectToAction(MVC.Koszyk.Index());
            
            if (User.Identity.IsAuthenticated)
            {
                var model = new UserAddressModel();
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
                model.Imie = user.Name ?? "Nie podano";
                model.Nazwisko = user.LastName ?? "Nie podano";
                model.Ulica = user.Street ?? "Nie podano";
                model.Telefon = user.PhoneNumber ?? "Nie podano";
                model.KodPocztowy = user.PostalCode ?? "Nie podano";
                model.Miasto = user.City ?? "Nie podano";
                model.Numer = user.Number ?? "Nie podano";
                return View(model);
            }
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