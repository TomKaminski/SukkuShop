using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
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
            if (!shoppingCart.Lines.Any() || shoppingCart.ShippingId == 0 || shoppingCart.PaymentId == 0 || shoppingCart.UserAddressModel==null)
                return RedirectToAction(MVC.Koszyk.Index());
            SharedShippingOrderSummaryModels paymentModel;
            SharedShippingOrderSummaryModels shippingModel;
            decimal totaltotalvalue;
            var discount=0;
            var user =  _userManager.FindById(User.Identity.GetUserId<int>());
            if (user != null)
            {
                discount = user.Rabat;
            }
            var orderitemsummary = OrderViewItemsTotal(shoppingCart, out paymentModel, out shippingModel, out totaltotalvalue,discount);
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
                Ulica = shoppingCart.UserAddressModel.Ulica,
                Email = shoppingCart.Email
            };
            
            var orderModel = new OrderViewModelsSummary
            {
                OrderViewItemsTotal = orderitemsummary,
                OrderPayment = paymentModel,
                OrderShipping = shippingModel,
                UserAddressModel = userModel,
                TotalTotalValue = totaltotalvalue - Convert.ToDecimal((orderitemsummary.TotalValue * discount) / 100),
                Discount = discount,
                DiscountValue = Convert.ToDecimal((orderitemsummary.TotalValue * discount) / 100).ToString("c").Replace(",", ".")

            };
            if (shoppingCart.UserAddressModel.Nip != null)
                orderModel.Firma = true;
            return View(orderModel);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Podsumowanie(OrderViewModelsSummary model, Cart shoppingCart)
        {


            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
            int? userId = null;
            var discount = 0;
            if (user != null)
            {
                userId = user.Id;
                discount = user.Rabat;
            }
                
            var orders = new Orders();
            var listakurwa = new List<OrderDetails>();
            decimal hehe = 0;
            decimal discountVal = 0;
            var paymentPrice = model.OrderPayment;
            var shippingPrice = model.OrderShipping;
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (var item in model.OrderViewItemsTotal.OrderProductList.ToList())
                    {
                        if (!item.ItemRemoved)
                        {
                            var product = _dbContext.Products.First(i => i.ProductId == item.Id);
                            
                            if (product.Quantity - product.ReservedQuantity >= item.Quantity)
                            {
                                var orderD = new OrderDetails
                                {
                                    ProductId = item.Id,
                                    Quantity = item.Quantity,
                                    ProdPrice = item.Price,
                                    SubTotalPrice = item.TotalValue
                                };
                                _dbContext.OrderDetails.Add(orderD);
                                listakurwa.Add(orderD);
                                hehe += item.TotalValue;
                                product.ReservedQuantity += item.Quantity;
                                product.OrdersCount++;
                                _dbContext.Products.AddOrUpdate(product);
                            }
                            else
                            {
                                if (product.Quantity - product.ReservedQuantity <= 0)
                                {
                                    item.ItemRemoved = true;
                                    model.HasErrors = true;
                                }
                                else if (product.Quantity - product.ReservedQuantity < item.Quantity)
                                {

                                    item.QuantityChanged = true;
                                    item.OldQuantity = item.Quantity;
                                    item.Quantity = (product.Quantity??0) - product.ReservedQuantity;
                                    item.TotalValue = item.Price*item.Quantity;
                                    var firstOrDefault = shoppingCart.Lines.FirstOrDefault(x => x.Id == item.Id);
                                    if (firstOrDefault != null)
                                        firstOrDefault.Quantity = item.Quantity;
                                    model.HasErrors = true;
                                    hehe += item.TotalValue;
                                }
                            }
                        }
                        else
                        {
                            model.OrderViewItemsTotal.OrderProductList.Remove(item);

                        }
                    }
                    discountVal = Convert.ToDecimal(hehe * discount / 100);
                    if (model.HasErrors)
                    {
                        model.OrderViewItemsTotal.TotalValue = hehe;
                        model.TotalTotalValue = hehe + model.OrderShipping.Price + model.OrderPayment.Price - discountVal;
                        return View(model);
                    }
                    if (model.OrderViewItemsTotal.OrderProductList.Count == 0)
                    {
                        shoppingCart.Clear();
                        return RedirectToAction(MVC.Koszyk.Index());
                    }
                    
                    orders = new Orders
                    {
                        Email = model.UserAddressModel.Email,
                        ProductsPrice = hehe,
                        TotalPrice = (hehe + paymentPrice.Price + shippingPrice.Price) - discountVal,
                        Name = model.UserAddressModel.Imie,
                        Surname = model.UserAddressModel.Nazwisko,
                        OrderDate = DateTime.Today,
                        SentDate = DateTime.Today,
                        ShippingId = model.OrderShipping.Id,
                        PaymentId = model.OrderPayment.Id,
                        City = model.UserAddressModel.Miasto,
                        Street = model.UserAddressModel.Ulica,
                        Number = model.UserAddressModel.Numer,
                        PostalCode = model.UserAddressModel.KodPocztowy,
                        OrderDetails = listakurwa,
                        UserId = userId,
                        OrderInfo = "Przyjęte",
                        UserHints = model.UserHints,
                        NazwaFirmy = model.UserAddressModel.NazwaFirmy,
                        OrderNip = model.UserAddressModel.Nip,
                        Phone = model.UserAddressModel.Telefon,
                        Discount = discount
                    };
                    _dbContext.Orders.Add(orders);
                    await _dbContext.SaveChangesAsync();
                    transaction.Complete();
                }
                catch(Exception ex)
                {
                    if (ex.GetType() != typeof(UpdateException))
                    {
                    }
                }
                
            }
            var email = new OrderSumEmail
            {
                To = model.UserAddressModel.Email,
                CallbackUrl = Url.Action(MVC.Konto.HistoriaZamowien()),
                Id = orders.OrderId,
                OrderViewModelsSummary = new OrderViewModelsSummary
                {
                    Firma = model.UserAddressModel.Nip != null,
                    TotalTotalValue = orders.TotalPrice,
                    Discount = discount,
                    DiscountValue = discountVal.ToString("c"),
                    OrderPayment = new SharedShippingOrderSummaryModels
                    {
                        Description = paymentPrice.Description,
                        Name = paymentPrice.Name,
                        Price = paymentPrice.Price
                    },
                    OrderShipping = new SharedShippingOrderSummaryModels
                    {
                        Description = shippingPrice.Description,
                        Name = shippingPrice.Name,
                        Price = shippingPrice.Price
                    },
                    UserAddressModel = model.UserAddressModel,
                    OrderViewItemsTotal = new OrderViewItemsTotal
                    {
                        TotalValue = hehe,
                        OrderProductList = listakurwa.Select(m => new OrderItemSummary
                        {
                            Image = m.Products.IconName,
                            Name = m.Products.Name,
                            Price = m.Products.Price??0,
                            Quantity = m.Quantity,
                            TotalValue = m.SubTotalPrice,
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


        private OrderViewItemsTotal OrderViewItemsTotal(Cart shoppingCart, out SharedShippingOrderSummaryModels paymentModel,
            out SharedShippingOrderSummaryModels shippingModel, out decimal totaltotalvalue,int discount)
        {

            var orderitemsummary = OrderViewItemsSummary(shoppingCart);
            var payment = _dbContext.PaymentTypes.First(x => x.PaymentId == shoppingCart.PaymentId);
            var shipping = _dbContext.ShippingTypes.First(x => x.ShippingId == shoppingCart.ShippingId);
            paymentModel = new SharedShippingOrderSummaryModels
            {
                Name = payment.PaymentName,
                Price = payment.PaymentPrice,
                Description = payment.PaymentDescription,
                Id = payment.PaymentId
            };
            shippingModel = new SharedShippingOrderSummaryModels
            {
                Name = shipping.ShippingName,
                Price = shipping.ShippingPrice,
                Description = shipping.ShippingDescription,
                Id=shipping.ShippingId
            };
            var value = orderitemsummary.TotalValue + paymentModel.Price + shippingModel.Price;
            totaltotalvalue = value-(value*Convert.ToDecimal(discount/100));
            
            return orderitemsummary;
        }

        private OrderViewModels OrderViewModels(Cart shoppingCart)
        {
            var discount = 0;
            var user = _userManager.FindById(User.Identity.GetUserId<int>());
            if (user != null)
                discount = user.Rabat;
            var productList = new List<OrderItem>();
            decimal totalValue = 0;
            foreach (var item in shoppingCart.Lines)
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.ProductId == item.Id);
                if (product == null) continue;
                var price = (product.Price - ((product.Price * product.Promotion) / 100)) ?? product.Price;
                var priceFloored = Math.Floor((price??0) * 100) / 100;

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
                TotalValue = totalValue.ToString("c").Replace(",", "."),
                Discount = discount,
                DiscountValue = Convert.ToDecimal((totalValue*discount)/100).ToString("c").Replace(",",".")
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
                var priceFloored = decimal.Round(Math.Floor((price??0) * 100) / 100,2);
                productList.Add(new OrderItemSummary
                {
                    Name = product.Name,
                    Price = decimal.Round(priceFloored,2),
                    Quantity = item.Quantity,
                    TotalValue = priceFloored * item.Quantity,
                    Image=product.ImageName,
                    Packing = product.Packing,
                    Id = product.ProductId
                });
                totalValue += (priceFloored * item.Quantity);
            }
            var model = new OrderViewItemsTotal
            {
                OrderProductList = productList,
                TotalValue = decimal.Round(totalValue,2)
            };
            return model;
        }
    }
}