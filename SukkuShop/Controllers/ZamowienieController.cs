using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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


        [HttpGet]
        public virtual ActionResult ZalogujOrder()
        {
            return PartialView("_LoginOrderPartial");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ZalogujOrder(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.PasswordLogin, model.RememberMe, false);
                switch (result)
                {
                    case SignInStatus.Success:
                        var user = await _userManager.FindByEmailAsync(model.Email);
                        Session["username"] = user.Name;
                        return JavaScript(string.Format("document.location = '{0}';", Url.Action(MVC.Zamowienie.Krok2())));
                    default:
                        ModelState.AddModelError("", "Nieprawidłowy adres email i/lub hasło");
                        return PartialView("_LoginOrderPartial", model);
                }
            }
            return PartialView("_LoginOrderPartial", model);
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
            bool? model = null;
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
                model = user.AccNip == null;
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public virtual ActionResult ChangeAddressPartial()
        {
            var user = _userManager.FindById(User.Identity.GetUserId<int>());

            var model = new UserAddressModel
            {
                Imie = user.Name ?? "Nie podano",
                Nazwisko = user.LastName ?? "Nie podano",
                Ulica = user.Street ?? "Nie podano",
                Telefon = user.PhoneNumber ?? "Nie podano",
                KodPocztowy = user.PostalCode ?? "Nie podano",
                Miasto = user.City ?? "Nie podano",
                Numer = user.Number ?? "Nie podano"
            };
            return PartialView("_ChangeAddressPartial",model);
        }

        [HttpPost]
        public virtual ActionResult ChangeAddressPartial(UserAddressModel model, Cart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                shoppingCart.Email = User.Identity.Name;
                shoppingCart.UserAddressModel = new CartAddressModel
                {
                    Imie = model.Imie,
                    KodPocztowy = model.KodPocztowy,
                    Miasto = model.Miasto,
                    Nazwisko = model.Nazwisko,
                    Numer = model.Numer,
                    Telefon = model.Telefon,
                    Ulica = model.Ulica
                };
                return JavaScript(string.Format("document.location = '{0}';", Url.Action(MVC.Zamowienie.Podsumowanie())));
            }
            return PartialView("_ChangeAddressPartial",model);
        }

        [HttpGet]
        public virtual ActionResult ChangeAddressFirmaPartial()
        {
            var user = _userManager.FindById(User.Identity.GetUserId<int>());

            var model = new FirmaAddressModel
            {
                NazwaFirmy = user.NazwaFirmy ?? "Nie podano",
                Nip = user.AccNip ?? "Nie podano",
                Ulica = user.Street ?? "Nie podano",
                Telefon = user.PhoneNumber ?? "Nie podano",
                KodPocztowy = user.PostalCode ?? "Nie podano",
                Miasto = user.City ?? "Nie podano",
                Numer = user.Number ?? "Nie podano"
            };
            return PartialView("_ChangeAddressFirmaPartial", model);
        }

        [HttpPost]
        public virtual ActionResult ChangeAddressFirmaPartial(FirmaAddressModel model, Cart shoppingCart)
        {

            if (ModelState.IsValid)
            {
                shoppingCart.Email = User.Identity.Name;
                shoppingCart.UserAddressModel = new CartAddressModel
                {
                    NazwaFirmy = model.NazwaFirmy,
                    KodPocztowy = model.KodPocztowy,
                    Miasto = model.Miasto,
                    Nip = model.Nip,
                    Numer = model.Numer,
                    Telefon = model.Telefon,
                    Ulica = model.Ulica
                };
                return JavaScript(string.Format("document.location = '{0}';", Url.Action(MVC.Zamowienie.Podsumowanie())));
            }
            return PartialView("_ChangeAddressFirmaPartial", model);
        }

        [HttpGet]
        public virtual PartialViewResult NewAddressOrderPartial()
        {
            return PartialView("_NewAddressOrderPartial");
        }

        [HttpPost]
        public virtual async Task<ActionResult> NewAddressOrderPartial(NewOrderAddressModel model, Cart shoppingCart)
        {
            if (!model.NewAccount)
                ModelState["Password"].Errors.Clear();
            
            if (ModelState.IsValid)
            {
                shoppingCart.Email = model.Email;
                shoppingCart.UserAddressModel = new CartAddressModel
                {
                    Imie = model.Imie,
                    KodPocztowy = model.KodPocztowy,
                    Miasto = model.Miasto,
                    Nazwisko = model.Nazwisko,
                    Numer = model.Numer,
                    Telefon = model.Telefon,
                    Ulica = model.Ulica
                };
                if (model.NewAccount)
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
                        Number = model.Numer,
                        KontoFirmowe = false,
                        EmailConfirmed = true
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false, false);
                        Session["username"] = user.Name;
                        return JavaScript(string.Format("document.location = '{0}';", Url.Action(MVC.Zamowienie.Podsumowanie())));
                    }
                    ModelState.AddModelError("Email", "Istnieje już taki użytkownik!");
                    return PartialView("_NewAddressOrderPartial", model);
                }
                return JavaScript(string.Format("document.location = '{0}';", Url.Action(MVC.Zamowienie.Podsumowanie())));
            }
            return PartialView("_NewAddressOrderPartial", model);
        }

        [HttpGet]
        public virtual PartialViewResult NewAddressOrderFirmaPartial()
        {
            return PartialView("_NewAddressOrderFirmaPartial");
        }

        [HttpPost]
        public virtual async Task<ActionResult> NewAddressOrderFirmaPartial(NewOrderAddressFirmaModel model, Cart shoppingCart)
        {
            if (!model.NewAccount)
                ModelState["Password"].Errors.Clear();
            if (ModelState.IsValid)
            {
                shoppingCart.Email = model.Email;
                shoppingCart.UserAddressModel = new CartAddressModel
                {
                    NazwaFirmy = model.NazwaFirmy,
                    KodPocztowy = model.KodPocztowy,
                    Miasto = model.Miasto,
                    Nip = model.Nip,
                    Numer = model.Numer,
                    Telefon = model.Telefon,
                    Ulica = model.Ulica
                };
                if (model.NewAccount)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        NazwaFirmy = model.NazwaFirmy,
                        AccNip = model.Nip,
                        PhoneNumber = model.Telefon,
                        Street = model.Ulica,
                        PostalCode = model.KodPocztowy,
                        City = model.Miasto,
                        Number = model.Numer,
                        KontoFirmowe = true,
                        EmailConfirmed = true
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false, false);
                        Session["username"] = user.Name;
                        return JavaScript(string.Format("document.location = '{0}';", Url.Action(MVC.Zamowienie.Podsumowanie())));
                    }
                    ModelState.AddModelError("Email","Istnieje już taki użytkownik!");
                    return PartialView("_NewAddressOrderFirmaPartial", model);
                }
                return JavaScript(string.Format("document.location = '{0}';", Url.Action(MVC.Zamowienie.Podsumowanie())));
            }
            return PartialView("_NewAddressOrderFirmaPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Wyloguj()
        {
            _authenticationManager.SignOut();
            Session["username"] = null;
            return RedirectToAction(MVC.Zamowienie.Krok2());
        }

       
        [HttpGet]
        public virtual ActionResult Podsumowanie(Cart shoppingCart)
        {
            if (!shoppingCart.Lines.Any() || shoppingCart.ShippingId == 0 || shoppingCart.PaymentId == 0)
                return RedirectToAction(MVC.Koszyk.Index());
            OrderPaymentSummary paymentModel;
            OrderShippingSummary shippingModel;
            string totaltotalvalue;
            var orderitemsummary = OrderViewItemsTotal(shoppingCart, out paymentModel, out shippingModel, out totaltotalvalue);
            var userModel = new CartAddressModel
            {
                NazwaFirmy = shoppingCart.UserAddressModel.NazwaFirmy,
                Nip = shoppingCart.UserAddressModel.Nip,
                Imie = shoppingCart.UserAddressModel.Imie,
                KodPocztowy = shoppingCart.UserAddressModel.KodPocztowy,
                Miasto = shoppingCart.UserAddressModel.Miasto,
                Nazwisko = shoppingCart.UserAddressModel.Nazwisko,
                Numer = shoppingCart.UserAddressModel.Numer,
                Telefon = shoppingCart.UserAddressModel.Telefon,
                Ulica = shoppingCart.UserAddressModel.Ulica
            };
            
            var orderModel = new OrderViewModelsSummary
            {
                OrderViewItemsTotal = orderitemsummary,
                OrderPayment = paymentModel,
                OrderShipping = shippingModel,
                UserAddressModel = userModel,
                TotalTotalValue = String.Format("{0:0.00}", totaltotalvalue)
            };
            if (shoppingCart.UserAddressModel.Nip != null)
                orderModel.Firma = true;
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

            int? userId = null;
            if (user != null)
                userId = user.Id;

            var listakurwa = new List<OrderDetails>();
            decimal hehe = 0;
            foreach (var item in shoppingCart.Lines)
            {
                var product = _dbContext.Products.First(i => i.ProductId == item.Id);
                var price = (product.Price - ((product.Price * product.Promotion) / 100)) ?? product.Price;
                var priceFloored = Math.Floor(price * 100) / 100;
                var orderD = new OrderDetails
                {
                    ProductId = item.Id,
                    Quantity = item.Quantity,
                    ProdPrice = priceFloored,
                    SubTotalPrice = item.Quantity*priceFloored
                };
                _dbContext.OrderDetails.Add(orderD);
                listakurwa.Add(orderD);
                hehe += orderD.SubTotalPrice;
                product.Quantity -= item.Quantity;
                product.OrdersCount++;
                _dbContext.Products.AddOrUpdate(product);
            }
            var paymentPrice = _dbContext.PaymentTypes.First(i => i.PaymentId == shoppingCart.PaymentId);
            var shippingPrice = _dbContext.ShippingTypes.First(i => i.ShippingId == shoppingCart.ShippingId);
            var orders = new Orders
            {
                Email = shoppingCart.Email,
                ProductsPrice = hehe,
                TotalPrice = hehe + paymentPrice.PaymentPrice + shippingPrice.ShippingPrice,
                Name = shoppingCart.UserAddressModel.Imie,
                Surname = shoppingCart.UserAddressModel.Nazwisko,
                OrderDate = DateTime.Today,
                SentDate = DateTime.Today,
                ShippingId = shoppingCart.ShippingId,
                PaymentId = shoppingCart.PaymentId,
                City = shoppingCart.UserAddressModel.Miasto,
                Street = shoppingCart.UserAddressModel.Ulica,
                Number = shoppingCart.UserAddressModel.Numer,
                PostalCode = shoppingCart.UserAddressModel.KodPocztowy,
                OrderDetails = listakurwa,
                UserId = userId,
                OrderInfo = "Przyjęte",
                UserHints = shoppingCart.UserHints,
                NazwaFirmy = shoppingCart.UserAddressModel.NazwaFirmy,
                OrderNip = shoppingCart.UserAddressModel.Nip,
                Phone = shoppingCart.UserAddressModel.Telefon
            };
            _dbContext.Orders.Add(orders);
            await _dbContext.SaveChangesAsync();
            var email = new OrderSumEmail
            {
                To = shoppingCart.Email,
                CallbackUrl = Url.Action(MVC.Konto.HistoriaZamowien()),
                Id = orders.OrderId,
                OrderViewModelsSummary = new OrderViewModelsSummary
                {
                    Firma = shoppingCart.UserAddressModel.Nip != null,
                    TotalTotalValue = (hehe + paymentPrice.PaymentPrice + shippingPrice.ShippingPrice).ToString("c"),
                    OrderPayment = new OrderPaymentSummary
                    {
                        Description = paymentPrice.PaymentDescription,
                        Name = paymentPrice.PaymentName,
                        Price = paymentPrice.PaymentPrice.ToString("c")
                    },
                    OrderShipping = new OrderShippingSummary
                    {
                        Description = shippingPrice.ShippingDescription,
                        Name = shippingPrice.ShippingName,
                        Price = shippingPrice.ShippingPrice.ToString("c")
                    },
                    UserAddressModel = shoppingCart.UserAddressModel,
                    OrderViewItemsTotal = new OrderViewItemsTotal
                    {
                        TotalValue = hehe.ToString("c"),
                        OrderProductList = listakurwa.Select(m => new OrderItemSummary
                        {
                            Image = m.Products.ImageName,
                            Name = m.Products.Name,
                            Price = m.Products.Price.ToString(),
                            Quantity = m.Quantity,
                            TotalValue = m.SubTotalPrice.ToString(),
                            Packing = m.Products.Packing
                        }).ToList()
                    }
                }
            };
            email.Send();
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


        private OrderViewItemsTotal OrderViewItemsTotal(Cart shoppingCart, out OrderPaymentSummary paymentModel,
            out OrderShippingSummary shippingModel, out string totaltotalvalue)
        {
            var orderitemsummary = OrderViewItemsSummary(shoppingCart);
            var payment = _dbContext.PaymentTypes.First(x => x.PaymentId == shoppingCart.PaymentId);
            var shipping = _dbContext.ShippingTypes.First(x => x.ShippingId == shoppingCart.ShippingId);
            paymentModel = new OrderPaymentSummary
            {
                Name = payment.PaymentName,
                Price = payment.PaymentPrice.ToString(),
                Description = payment.PaymentDescription
            };
            shippingModel = new OrderShippingSummary
            {
                Name = shipping.ShippingName,
                Price = shipping.ShippingPrice.ToString(),
                Description = shipping.ShippingDescription
            };
            var value = Convert.ToDecimal(orderitemsummary.TotalValue.Replace(".",",").Replace(" zł","")) + Convert.ToDecimal(paymentModel.Price) +
                        Convert.ToDecimal(shippingModel.Price);
            totaltotalvalue = value.ToString();
            
            return orderitemsummary;
        }

        private OrderViewModels OrderViewModels(Cart shoppingCart)
        {
            var productList = new List<OrderItem>();
            decimal totalValue = 0;
            foreach (var item in shoppingCart.Lines)
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.ProductId == item.Id);
                if (product == null) continue;
                var price = (product.Price - ((product.Price * product.Promotion) / 100)) ?? product.Price;
                var priceFloored = Math.Floor(price * 100) / 100;

                productList.Add(new OrderItem
                {
                    Name = product.Name,
                    Price =
                        priceFloored.ToString("c"),
                    Quantity = item.Quantity,
                    TotalValue =
                        (priceFloored*item.Quantity).ToString("c"),
                            Packing = product.Packing
                });
                totalValue += (priceFloored * item.Quantity);
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
                var price = (product.Price - ((product.Price * product.Promotion) / 100)) ?? product.Price;
                var priceFloored = Math.Floor(price * 100) / 100;
                productList.Add(new OrderItemSummary
                {
                    Name = product.Name,
                    Price =
                        priceFloored.ToString("c"),
                    Quantity = item.Quantity,
                    TotalValue =
                        (priceFloored * item.Quantity).ToString("c"),
                    Image=product.ImageName,
                    Packing = product.Packing
                });
                totalValue += (priceFloored * item.Quantity);
            }
            var model = new OrderViewItemsTotal
            {
                OrderProductList = productList,
                TotalValue = totalValue.ToString("c").Replace(",", ".")
            };
            return model;
        }
    }
}