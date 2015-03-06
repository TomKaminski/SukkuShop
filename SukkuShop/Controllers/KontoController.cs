using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SukkuShop.Identity;
using SukkuShop.Infrastructure.Generic;
using SukkuShop.Models;

namespace SukkuShop.Controllers
{
    [Authorize]
    public partial class KontoController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IAppRepository _appRepository;

        public KontoController(ApplicationUserManager userManager, ApplicationSignInManager signInManager,
            IAuthenticationManager authenticationManager, ApplicationDbContext dbContext, IAppRepository appRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
            _dbContext = dbContext;
            _appRepository = appRepository;
        }

        [AllowAnonymous]
        public virtual async Task<ActionResult> ResendActivationEmail(string email)
        {
            var userid = _userManager.FindByEmail(email).Id;
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(userid);
            var callbackUrl = Url.Action(MVC.Konto.Aktywacja(userid, code), Request.Url.Scheme);
            ActivationMailBuilder(callbackUrl, email);
            return new EmptyResult();
        }


        // GET: /Account/Login
        [AllowAnonymous]
        public virtual ActionResult Zaloguj(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction(MVC.Home.Index());
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Zaloguj(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    return View("KontoNieaktywne", (object)model.Email);
                }
            }
            
            var result =
                await _signInManager.PasswordSignInAsync(model.Email, model.PasswordLogin, model.RememberMe, false);
            switch (result)
            {
                case SignInStatus.Success:
                    Session["username"] = user.Name;
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View(MVC.Shared.Views.Blokada);
                default:
                    ModelState.AddModelError("", "Nieprawidłowa nazwa użytkownika lub hasło");
                    return View(model);
            }
        }


        // GET: /Account/Register
        [AllowAnonymous]
        public virtual ActionResult Zarejestruj()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction(MVC.Home.Index());
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Zarejestruj(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    KontoFirmowe = model.KontoFirmowe
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action(MVC.Konto.Aktywacja(user.Id, code), Request.Url.Scheme);
                    ActivationMailBuilder(callbackUrl, model.Email);
                    return View(MVC.Konto.Views.RegisterSuccess);
                }
                ModelState.AddModelError("", "Istnieje już użytkownik o podanym adresie Email.");
            }
            return View(model);
        }


        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public virtual async Task<ActionResult> Aktywacja(int userId, string code)
        {
            if (code == null)
            {
                return View(MVC.Shared.Views._Blad);
            }
            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? MVC.Konto.Views.KontoAktywne : MVC.Shared.Views._Blad);
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public virtual ActionResult ZapomnianeHaslo()
        {
            var model = new ForgotPasswordViewModel();
            return View(model);
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ZapomnianeHaslo(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.result = "Sent";
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    model.result = "NoUser";
                    return PartialView("_ZapomnianeHasloPartial", model);
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetujHaslo", "Konto", new {userId = user.Id, code}, Request.Url.Scheme);
                ResetPasswordMailBuilder(callbackUrl, model.Email);
                return PartialView("_ZapomnianeHasloPartial", model);
            }
            return PartialView("_ZapomnianeHasloPartial", model);
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public virtual ActionResult ResetujHaslo(string code)
        {
            var model = new ResetPasswordViewModel
            {
                Code = code
            };
            return code == null ? View(MVC.Error.Views.Error) : View(model);
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ResetujHaslo(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ResetujHasloPartial", model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                model.result = "NoUser";
                return PartialView("_ResetujHasloPartial", model);
            }
            var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                model.result = "Sent";
                return PartialView("_ResetujHasloPartial", model);
            }
            ModelState.AddModelError("", "Klucz nie pasuje do adresu email.");
            return PartialView("_ResetujHasloPartial", model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Wyloguj()
        {
            _authenticationManager.SignOut();
            Session["username"] = null;
            return RedirectToAction(MVC.Home.Index());
        }


        ////////////MANAGE//////////////

        // GET: DaneOsobowe main view
        [HttpGet]
        [Authorize]
        public virtual ActionResult DaneOsobowe()
        {
            var user = _userManager.FindById(User.Identity.GetUserId<int>());
            return View("Index", user.KontoFirmowe);
        }


        // GET: Zmień hasło
        [HttpGet]
        [Authorize]
        public virtual ActionResult ZmienHaslo()
        {
            var model = new ChangePasswordViewModel();
            return PartialView("_ChangePassword", model);
        }

        // POST: Zmień hasło
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public virtual ActionResult ZmienHaslo(ChangePasswordViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return PartialView("_ChangePassword", model);
            }
            var result = _userManager.ChangePassword(int.Parse(User.Identity.GetUserId()), model.OldPassword,
                model.NewPassword);
            if (result.Succeeded)
            {
                var user = _userManager.FindById(int.Parse(User.Identity.GetUserId()));
                if (user != null)
                {
                    _signInManager.SignIn(user, false, false);
                }
                model.Success = true;
                return PartialView("_ChangePassword", model);
            }
            ModelState.AddModelError("OldPassword","Nieprawidłowe hasło.");
            return PartialView("_ChangePassword", model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public virtual ActionResult ZmianaEmaila(int userId, string code,string newEmail)
        {
            if (code == null)
                return View(MVC.Shared.Views._Blad);            
            var result =  _userManager.VerifyUserToken(userId, "ChangeEmail", code);
            if (result)
            {
                var user = _userManager.FindById(userId);
                user.Email = newEmail;
                user.UserName = newEmail;
                _userManager.Update(user);
                _userManager.UpdateSecurityStamp(userId);
                _authenticationManager.IfNotNull(manager => manager.SignOut());
                Session["username"] = null;
                return RedirectToAction(MVC.Konto.ChangeEmailSuccess(newEmail));
            }
            return View(MVC.Shared.Views._Blad);
        }

        [AllowAnonymous]
        public virtual ViewResult ChangeEmailSuccess(string model)
        {
            return View((object) model);
        }

        //GET: User data - konto osobiste
        [HttpGet]
        [Authorize]
        public virtual ActionResult ChangeUserInfoViewModel()
        {
            var user = _userManager.FindById(User.Identity.GetUserId<int>());
            var model = new ChangeUserInfoViewModel
            {
                Miasto = user.City ?? "Nie podano",
                Email = user.Email,
                LastName = user.LastName ?? "Nie podano",
                Name = user.Name ?? "Nie podano",
                Numer = user.Number ?? "Nie podano",
                Telefon = user.PhoneNumber ?? "Nie podano",
                KodPocztowy = user.PostalCode ?? "Nie podano",
                Ulica = user.Street ?? "Nie podano",
                Success = null
            };
            return PartialView("_ChangeUserInfoViewModel", model);
        }

        //POST: change User data - konto osobiste
        [HttpPost]
        [Authorize]
        public virtual ActionResult ChangeUserInfoViewModel(ChangeUserInfoViewModel model)
        {
            var userExists = _userManager.FindByEmail(model.Email);
            if (userExists != null)
                if (userExists.Id != User.Identity.GetUserId<int>())
                    ModelState.AddModelError("Email", "Adres email jest używany!");
            if (ModelState.IsValid)
            {
                var user = _userManager.FindById(User.Identity.GetUserId<int>());
                user.City = model.Miasto;
                user.LastName = model.LastName;
                user.Name = model.Name;
                user.PostalCode = model.KodPocztowy;
                user.Number = model.Numer;
                user.PhoneNumber = model.Telefon;
                user.Street = model.Ulica;
                user.KontoFirmowe = false;
                if (user.Email != model.Email)
                {
                    var code = _userManager.GenerateUserToken("ChangeEmail", user.Id);
                    var callbackUrl = Url.Action("ZmianaEmaila", "Konto", new { userId = user.Id, code,newEmail = model.Email,oldEmail = user.Email }, Request.Url.Scheme);
                    ChangeEmailMailBuilder(callbackUrl, model.Email, user.Email);
                    ViewBag.EmailChanged = true;
                }                
                var result = _userManager.Update(user);
                model.Success = result.Succeeded;
                return PartialView("_ChangeUserInfoViewModel", model);
            }
            return PartialView("_ChangeUserInfoViewModel", model);
        }

        //GET: User data - konto firmowe
        [HttpGet]
        [Authorize]
        public virtual ActionResult ChangeUserFirmaInfoViewModel()
        {
            var user = _userManager.FindById(User.Identity.GetUserId<int>());
            var model = new ChangeUserFirmaInfoViewModel
            {
                Miasto = user.City ?? "Nie podano",
                Email = user.Email,
                NazwaFirmy = user.NazwaFirmy ?? "Nie podano",
                Nip = user.AccNip ?? "Nie podano",
                Numer = user.Number ?? "Nie podano",
                Telefon = user.PhoneNumber ?? "Nie podano",
                KodPocztowy = user.PostalCode ?? "Nie podano",
                Ulica = user.Street ?? "Nie podano",
                Success = null
            };
            return PartialView("_ChangeUserFirmaInfoViewModel", model);
        }

        //GET: change User data - konto firmowe
        [HttpPost]
        [Authorize]
        public virtual ActionResult ChangeUserFirmaInfoViewModel(ChangeUserFirmaInfoViewModel model)
        {
            var userExists = _userManager.FindByEmail(model.Email);
            if (userExists != null)
                if (userExists.Id != User.Identity.GetUserId<int>())
                    ModelState.AddModelError("Email", "Adres email jest używany!");
            if (ModelState.IsValid)
            {
                var user = _userManager.FindById(User.Identity.GetUserId<int>());
                user.City = model.Miasto;
                user.NazwaFirmy = model.NazwaFirmy;
                user.AccNip = model.Nip;
                user.PostalCode = model.KodPocztowy;
                user.Number = model.Numer;
                user.PhoneNumber = model.Telefon;
                user.Street = model.Ulica;
                user.KontoFirmowe = true;
                if (user.Email != model.Email)
                {
                    var code = _userManager.GenerateUserToken("ChangeEmail", user.Id);
                    var callbackUrl = Url.Action("ZmianaEmaila", "Konto", new { userId = user.Id, code, newEmail = model.Email, oldEmail = user.Email }, Request.Url.Scheme);
                    ChangeEmailMailBuilder(callbackUrl, model.Email, user.Email);
                    ViewBag.EmailChanged = true;
                }     
                var result = _userManager.Update(user);
                model.Success = result.Succeeded;
                return PartialView("_ChangeUserFirmaInfoViewModel", model);
            }
            return PartialView("_ChangeUserFirmaInfoViewModel", model);
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult HistoriaZamowien()
        {
            var userId = _userManager.FindById(User.Identity.GetUserId<int>()).Id;
            var model = _appRepository.GetAll<Orders>(m => m.UserId == userId).Select(x => new AccountOrderItemModel
            {
                ActualState = x.OrderInfo,
                Id = x.OrderId,
                OrderDate = x.OrderDate,
                TotalPrice = x.TotalPrice
            }).ToList();
            var viewModel = model.Select(itemModel => new AccountOrderItemViewModel
            {
                ActualState = itemModel.ActualState,
                Id = itemModel.Id,
                OrderDate = itemModel.OrderDate.ToShortDateString(),
                TotalPrice = itemModel.TotalPrice
            }).ToList();
            return View(viewModel);
        }

        public virtual ActionResult AnulujZamówienie(int id)
        {
            
            var order = _appRepository.GetSingle<Orders>(m => m.OrderId == id);
            var orderDetails = _appRepository.GetAll<OrderDetails>(x => x.OrderId == id);
            if (order != null && (order.OrderInfo != "Wysłane" && order.OrderInfo != "Anulowane"))
            {
                order.OrderInfo = "Anulowane";
                foreach (var item in orderDetails)
                {
                    var productItem = _appRepository.GetSingle<Products>(j => j.ProductId == item.ProductId);
                    productItem.Quantity += item.Quantity;
                    productItem.OrdersCount++;
                    _dbContext.Products.AddOrUpdate(productItem);
                }
                _dbContext.Orders.AddOrUpdate(order);
                try
                {
                    _dbContext.SaveChanges();
                    var email = new OrderCancelEmail
                    {
                        To = User.Identity.Name,
                        Id = id
                    };
                    email.Send();
                }
                catch (Exception)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public virtual ActionResult SzczegółyZamówienia(int id)
        {
            var userId = _userManager.FindById(User.Identity.GetUserId<int>());
            var order = _appRepository.GetSingle<Orders>(m => m.OrderId == id);
            if (userId.Id != order.UserId)
                return RedirectToAction(MVC.Konto.HistoriaZamowien());
            var payment = _appRepository.GetSingle<PaymentType>(x => x.PaymentId == order.PaymentId);
            var shipping = _appRepository.GetSingle<ShippingType>(x => x.ShippingId == order.ShippingId);
            var orderDetails = _appRepository.GetAll<OrderDetails>(x => x.OrderId == order.OrderId);
            var model = new AccountOrderViewModelsSummary
            {
                Id = id,
                Firma = userId.KontoFirmowe,
                OrderPayment = new SharedShippingOrderSummaryModels
                {
                    Description = payment.PaymentDescription,
                    Id=order.PaymentId,
                    Name = payment.PaymentName,
                    Price = order.FreeShippingPayment ? 0 : payment.PaymentPrice
                },
                OrderShipping = new SharedShippingOrderSummaryModels
                {
                    Description = shipping.ShippingDescription,
                    Id=order.ShippingId,
                    Name = shipping.ShippingName,
                    Price = order.FreeShippingPayment ? 0 : shipping.ShippingPrice
                },
                TotalTotalValue = order.TotalPrice.ToString("c"),
                OrderInfo = order.OrderInfo,
                OrderDat = order.OrderDate.ToShortDateString(),
                Discount = order.Discount,
                DiscountValue = (order.TotalPrice - (order.ProductsPrice + (order.FreeShippingPayment ? 0 : payment.PaymentPrice) + (order.FreeShippingPayment ? 0 : shipping.ShippingPrice))).ToString("c"),
                UserAddressModel = new CartAddressModel
                {
                    Imie = order.Name,
                    KodPocztowy = order.PostalCode,
                    Miasto = order.City,
                    NazwaFirmy = order.NazwaFirmy,
                    Nazwisko = order.Surname,
                    Nip = order.OrderNip,
                    Numer = order.Number,
                    Telefon = order.Phone,
                    Ulica = order.Street
                },
                OrderViewItemsTotal = new OrderViewItemsTotal
                {
                    OrderProductList = orderDetails.Select(x =>
                    {
                        var product = _appRepository.GetSingle<Products>(k => k.ProductId == x.ProductId);
                        return new OrderItemSummary
                        {
                            Name = product.Name,
                            Image = product.IconName ?? "NoPhoto_small",
                            Price = x.ProdPrice,
                            Quantity = x.Quantity,
                            TotalValue = x.SubTotalPrice,
                            Packing = product.Packing,
                        };
                    }).ToList(),
                    TotalValue = order.ProductsPrice                      
                }
            };
            return View(model);
        }

        #region Helpers

        private void ActivationMailBuilder(string callbackUrl, string sendTo)
        {
            var email = new ActivationEmail
            {
                CallbackUrl = callbackUrl,
                To = sendTo
            };
            email.Send();
        }

        private void ResetPasswordMailBuilder(string callbackUrl, string sendTo)
        {
            var email = new ResetEmail
            {
                CallbackUrl = callbackUrl,
                To = sendTo
            };
            email.Send();
        }

        private void ChangeEmailMailBuilder(string callbackUrl, string sendTo,string oldEmail)
        {
            var email = new ChangeEmail
            {
                CallbackUrl = callbackUrl,
                To = sendTo,
                OldEmail = oldEmail
            };
            email.Send();
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(MVC.Home.Index());
        }


        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie,
                DefaultAuthenticationTypes.TwoFactorCookie);
            _authenticationManager.SignIn(new AuthenticationProperties {IsPersistent = isPersistent},
                await user.GenerateUserIdentityAsync(_userManager));
        }

        #endregion
    }
}