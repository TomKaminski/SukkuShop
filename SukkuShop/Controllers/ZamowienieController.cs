using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SukkuShop.Identity;
using SukkuShop.Models;

namespace SukkuShop.Controllers
{
    public partial class ZamowienieController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly IAuthenticationManager _authenticationManager;

        public ZamowienieController(IAuthenticationManager authenticationManager,ApplicationUserManager userManager, ApplicationDbContext dbContext, ApplicationSignInManager signInManager)
        {
            _authenticationManager = authenticationManager;
            _signInManager = signInManager;
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
            {
                ModelState.AddModelError("", "Proszę wybrać metodę dostawy oraz metodę płatności");
                return View(MVC.Zamowienie.Views.Krok1);
            }
                
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

        [HttpPost]
        public virtual async Task<ActionResult> Krok2(Cart shoppingCart, UserAddressModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
                user.Name = model.Imie;
                user.LastName = model.Nazwisko;
                user.Number = model.Numer;
                user.PhoneNumber = model.Telefon;
                user.PostalCode = model.KodPocztowy;
                user.City = model.Miasto;
                user.Street = model.Ulica;
                await _userManager.UpdateAsync(user);
                return RedirectToAction(MVC.Zamowienie.Podsumowanie());
            }
            return View(MVC.Zamowienie.Views.Krok2);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Wyloguj()
        {
            _authenticationManager.SignOut();
            Session["username"] = null;
            return RedirectToAction(MVC.Zamowienie.Krok2());
        }

        public async virtual Task<ActionResult> NewAddressOrder(NewOrderAddressModel model, Cart shoppingCart)
        {
            var modelPlz = new UserAddressModel
            {
                Imie = model.Imie,
                Miasto = model.Miasto,
                Nazwisko = model.Nazwisko,
                Numer = model.Numer,
                KodPocztowy = model.KodPocztowy,
                Ulica = model.Ulica,
                Telefon = model.Telefon
            };
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Imie,
                    LastName = model.Nazwisko,
                    PhoneNumber = model.Telefon,
                    Street = model.Ulica,
                    PostalCode = model.KodPocztowy,
                    City = model.Miasto,
                    Number = model.Numer
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false, false);
                    Session["username"] = user.Name;
                    return RedirectToAction(MVC.Zamowienie.Podsumowanie());
                }
                ModelState.AddModelError("", "Istnieje już użytkownik o podanym adresie Email");
                return View(MVC.Zamowienie.Views.Krok2,modelPlz);
            }
            return View(MVC.Zamowienie.Views.Krok2, modelPlz);
        }

        private OrderViewItemsTotal OrderViewItemsTotal(Cart shoppingCart, out OrderPaymentSummary paymentModel,
            out OrderShippingSummary shippingModel, out string totaltotalvalue)
        {
            var orderitemsummary = OrderViewItemsSummary(shoppingCart);
            var payment = _dbContext.PaymentTypes.First(x => x.PaymentId == shoppingCart.PaymentId);
            var shipping = _dbContext.ShippingTypes.First(x => x.ShippingId == shoppingCart.ShippingId);
            paymentModel = new OrderPaymentSummary
            {
                Name = payment.PaymentName,
                Price = payment.PaymentPrice.ToString()
            };
            shippingModel = new OrderShippingSummary
            {
                Name = shipping.ShippingName,
                Price = shipping.ShippingPrice.ToString()
            };

            switch (shipping.ShippingId)
            {
                case 1:
                    shippingModel.Description = "Poczta Polska Kurier48 OPIS";
                    break;
                case 2:
                    shippingModel.Description = "Poczta Polska Przesyłka Ekonomiczna OPIS";
                    break;
                case 3:
                    shippingModel.Description = "Kurier Siódemka OPIS";
                    break;
                case 4:
                    shippingModel.Description = "Paczkomaty OPIS";
                    break;
                case 5:
                    shippingModel.Description = "Odbiór osobisty OPIS";
                    break;
            }

            switch (payment.PaymentId)
            {
                case 1:
                    paymentModel.Description = "Przedpłata na konto OPIS";
                    break;
                case 2:
                    paymentModel.Description = "Płatność za pobraniem OPIS";
                    break;
                case 3:
                    paymentModel.Description = "PAYU OPIS";
                    break;
            }
            totaltotalvalue =
                Convert.ToString(Convert.ToDecimal(orderitemsummary.TotalValue) + Convert.ToDecimal(paymentModel.Price) +
                                 Convert.ToDecimal(shippingModel.Price));
            return orderitemsummary;
        }

        [HttpGet]
        public async virtual Task<ActionResult> Podsumowanie(Cart shoppingCart)
        {
            if (!shoppingCart.Lines.Any() || shoppingCart.ShippingId == 0 || shoppingCart.PaymentId == 0 || !User.Identity.IsAuthenticated)
                return RedirectToAction(MVC.Koszyk.Index());
            OrderPaymentSummary paymentModel;
            OrderShippingSummary shippingModel;
            string totaltotalvalue;
            var orderitemsummary = OrderViewItemsTotal(shoppingCart, out paymentModel, out shippingModel, out totaltotalvalue);
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
            var userModel = new UserAddressModel
            {
                Imie = user.Name,
                KodPocztowy = user.PostalCode,
                Miasto = user.City,
                Nazwisko = user.LastName,
                Numer = user.Number,
                Telefon = user.PhoneNumber,
                Ulica = user.Street
            };
            var orderModel = new OrderViewModelsSummary
            {
                OrderViewItemsTotal = orderitemsummary,
                OrderPayment = paymentModel,
                OrderShipping = shippingModel,
                UserAddressModel = userModel,
                TotalTotalValue = totaltotalvalue
            };
            return View(orderModel);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Podsumowanie(string userhints, Cart shoppingCart)
        {
            if (!shoppingCart.Lines.Any())
            {
                return RedirectToAction(MVC.Koszyk.Index());
            }
            shoppingCart.UserHints = userhints;
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());

            var listakurwa = new List<OrderDetails>();
            decimal hehe = 0;
            foreach (var item in shoppingCart.Lines)
            {
                var orderD = new OrderDetails
                {
                    ProductId = item.Id,
                    Quantity = item.Quantity
                };
                var productPrice = _dbContext.Products.First(i => i.ProductId == item.Id).Price;
                orderD.SubTotalPrice = item.Quantity * productPrice;
                _dbContext.OrderDetails.Add(orderD);
                listakurwa.Add(orderD);
                hehe += orderD.SubTotalPrice;
                var prod = _dbContext.Products.First(m => m.ProductId == item.Id);
                prod.Quantity -= item.Quantity;
                _dbContext.Products.AddOrUpdate(prod);
            }
            var paymentPrice = _dbContext.PaymentTypes.First(i => i.PaymentId == shoppingCart.PaymentId).PaymentPrice;
            var shippingPrice = _dbContext.ShippingTypes.First(i => i.ShippingId == shoppingCart.ShippingId).ShippingPrice;
            var orders = new Orders
            {
                ProductsPrice = hehe,
                TotalPrice = hehe + paymentPrice + shippingPrice,
                Name = user.Name,
                Surname = user.LastName,
                OrderDate = DateTime.Today,
                SentDate = DateTime.Today,
                ShippingId = shoppingCart.ShippingId,
                PaymentId = shoppingCart.PaymentId,
                City = user.City,
                Street = user.Street,
                Number = user.Number,
                PostalCode = user.PostalCode,
                OrderDetails = listakurwa,
                UserId = user.Id,
                User = user,
                OrderInfo = "Przyjęte",
                UserHints = shoppingCart.UserHints

            };
            _dbContext.Orders.Add(orders);
            await _dbContext.SaveChangesAsync();
            shoppingCart.Clear();
            return View("OrderSubmitted",orders.OrderId);
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

        private OrderViewItemsTotal OrderViewItemsSummary(Cart shoppingCart)
        {
            var productList = new List<OrderItemSummary>();
            decimal totalValue = 0;
            foreach (var item in shoppingCart.Lines)
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.ProductId == item.Id);
                if (product == null) continue;
                productList.Add(new OrderItemSummary
                {
                    Name = product.Name,
                    Price =
                        ((product.Price - ((product.Price * product.Promotion) / 100)) ?? product.Price).ToString("c"),
                    Quantity = item.Quantity,
                    TotalValue =
                        (((product.Price - ((product.Price * product.Promotion) / 100)) ?? product.Price) * item.Quantity)
                            .ToString("c"),
                    Image=product.ImageName
                });
                totalValue += ((product.Price - ((product.Price * product.Promotion) / 100)) ?? product.Price) *
                              item.Quantity;
            }
            var model = new OrderViewItemsTotal
            {
                OrderProductList = productList,
                TotalValue = totalValue.ToString()
            };
            return model;
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie,
                DefaultAuthenticationTypes.TwoFactorCookie);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent },
                await user.GenerateUserIdentityAsync(_userManager));
        }
    }
}