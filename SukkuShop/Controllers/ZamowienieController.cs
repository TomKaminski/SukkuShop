using System;
using System.Collections.Generic;
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

        public virtual async Task<ActionResult> UserOrderSubmit(Cart shoppingCart, UserAddressModel model)
        {
            shoppingCart.OrderAdress = new OrderAdress
            {
                City = model.Miasto,
                Name = model.Imie,
                Nazwisko = model.Nazwisko,
                Number = model.Numer,
                PostalCode = model.KodPocztowy,
                Street = model.Ulica,
                Telefon = model.Telefon
            };
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

        public async virtual Task<ActionResult> NewAddressOrder(NewOrderAddressModel model, Cart shoppingCart)
        {
            shoppingCart.OrderAdress = new OrderAdress
            {
                City = model.Miasto,
                Name = model.Imie,
                Nazwisko = model.Nazwisko,
                Number = model.Numer,
                PostalCode = model.KodPocztowy,
                Street = model.Ulica,
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
            }

            return RedirectToAction(MVC.Zamowienie.Krok2());
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

        public virtual ActionResult Podsumowanie(Cart shoppingCart)
        {
            if (!shoppingCart.Lines.Any() || shoppingCart.ShippingId == 0 || shoppingCart.PaymentId == 0 || shoppingCart.OrderAdress == null )
                return RedirectToAction(MVC.Koszyk.Index());
            OrderPaymentSummary paymentModel;
            OrderShippingSummary shippingModel;
            string totaltotalvalue;
            var orderitemsummary = OrderViewItemsTotal(shoppingCart, out paymentModel, out shippingModel, out totaltotalvalue);
            var userModel = new UserAddressModel
            {
                Imie = shoppingCart.OrderAdress.Name,
                KodPocztowy = shoppingCart.OrderAdress.PostalCode,
                Miasto = shoppingCart.OrderAdress.City,
                Nazwisko = shoppingCart.OrderAdress.Nazwisko,
                Numer = shoppingCart.OrderAdress.Number,
                Telefon = shoppingCart.OrderAdress.Telefon,
                Ulica = shoppingCart.OrderAdress.Street
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

        public void SetShipping(Cart shoppingCart,int id)
        {
            shoppingCart.ShippingId = id;            
        }

        public void SetPayment(Cart shoppingCart, int id)
        {
            shoppingCart.PaymentId = id;
        }

        public async void SaveToDatabase(Cart shoppingCart)
        {
            
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
             //Orders.SentDate -> panel admina [czeckbox, że wysłane]?
             //Orders.OrderInfo -> wtf tutaj?
             
             //Orders.TotalPrice -> ProductsPrice + shippingPrice + PaymentPrice                       
             // Orders.SpecialAddress -> CZO TO JEST
            // trzeba discounta usunac

            //Orders.ProductsPrice -> suma SubTotalPrice z Order Details [nie umiem] :(

            var listakurwa = new List<OrderDetails>();
            foreach (var item in shoppingCart.Lines)
            {
                var orderD = new OrderDetails
                {
                    ProductId = item.Id, 
                    Quantity = item.Quantity
                };
                var productPrice = _dbContext.Products.First(i => i.ProductId == item.Id).Price;
                orderD.SubTotalPrice = item.Quantity*productPrice;
                _dbContext.OrderDetails.Add(orderD);
                listakurwa.Add(orderD);
            }
            var orders = new Orders
            {
                Name = user.Name,
                Surname = user.LastName,
                OrderDate = DateTime.Now,
                ShippingId = shoppingCart.ShippingId,
                PaymentId = shoppingCart.PaymentId,
                City = shoppingCart.OrderAdress.City,
                Street = shoppingCart.OrderAdress.Street,
                Number = shoppingCart.OrderAdress.Number,
                PostalCode = shoppingCart.OrderAdress.PostalCode,
                OrderDetails = listakurwa
            };
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