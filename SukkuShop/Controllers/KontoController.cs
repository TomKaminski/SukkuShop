using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SukkuShop.Identity;
using SukkuShop.Models;

namespace SukkuShop.Controllers
{
    [Authorize]
    public partial class KontoController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly IAuthenticationManager _authenticationManager;

        public KontoController(ApplicationUserManager userManager, ApplicationSignInManager signInManager,
            IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
        }

        [AllowAnonymous]
        public virtual async Task<ActionResult> ResendActivationEmail(string email)
        {
            var userid = _userManager.FindByEmail(email).Id;
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(userid);
            var callbackUrl = Url.Action(MVC.Konto.Aktywacja(userid,code), Request.Url.Scheme);
            await _userManager.SendEmailAsync(userid, "Confirm your account", ActivationMailBuilder(callbackUrl));
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
            if (!_userManager.IsEmailConfirmed(_userManager.FindByEmail(model.Email).Id))
            {
                return View("KontoNieaktywne", (object)model.Email);
            }
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.PasswordLogin, model.RememberMe, false);
            switch (result)
            {
                case SignInStatus.Success:
                    var user = await _userManager.FindByEmailAsync(model.Email);
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
                    await _userManager.SendEmailAsync(user.Id, "Aktywacja konta", ActivationMailBuilder(callbackUrl));
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
                return View(MVC.Error.Views.Error);
            }
            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return RedirectToAction(result.Succeeded ? MVC.Konto.Zaloguj() : MVC.Konto.Zarejestruj());
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public virtual ActionResult ZapomnianeHaslo()
        {
            return View();
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
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View(MVC.Konto.Views.ZapomnianeHasloPotwierdzenie);
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetujHaslo", "Konto", new {userId = user.Id, code}, Request.Url.Scheme);
                await _userManager.SendEmailAsync(user.Id, "Reset Password", ResetPasswordMailBuilder(callbackUrl));
                return RedirectToAction(MVC.Konto.ZapomnianeHasloPotwierdzenie());
            }
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public virtual ActionResult ZapomnianeHasloPotwierdzenie()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public virtual ActionResult ResetujHaslo(string code)
        {
            return code == null ? View(MVC.Error.Views.Error) : View();
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
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(MVC.Konto.ResetujHasloPotwierdzenie());
            }
            var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(MVC.Konto.ResetujHasloPotwierdzenie());
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public virtual ActionResult ResetujHasloPotwierdzenie()
        {
            return View();
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
        public virtual async Task<ActionResult> Index()
        {
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
            return View(user.KontoFirmowe);
        }


        // GET: Zmień hasło
        [HttpGet]
        public virtual ActionResult ZmienHaslo()
        {
            return PartialView("_ChangePassword");
        }
        // POST: Zmień hasło
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ZmienHaslo(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ChangePassword",model);
            }
            var result =
                await
                    _userManager.ChangePasswordAsync(int.Parse(User.Identity.GetUserId()), model.OldPassword,
                        model.NewPassword);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(int.Parse(User.Identity.GetUserId()));
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction(MVC.Konto.Index());
            }
            AddErrors(result);
            return PartialView("_ChangePassword");
        }

        //GET: User data - konto osobiste
        [HttpGet]
        public virtual async Task<ActionResult> ChangeUserInfoViewModel()
        {
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
            var model = new ChangeUserInfoViewModel
            {
                City = user.City ?? "Nie podano",
                Email = user.Email,
                LastName = user.LastName ?? "Nie podano",
                Name = user.Name ?? "Nie podano",
                Number = user.Number ?? "Nie podano",
                Phone = user.PhoneNumber ?? "Nie podano",
                PostalCode = user.PostalCode ?? "Nie podano",
                Street = user.Street ?? "Nie podano"
            };
            return PartialView("_ChangeUserInfoViewModel", model);
        }

        //POST: change User data - konto osobiste
        [HttpPost]
        public virtual async Task<ActionResult> ChangeUserInfoViewModel(ChangeUserInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
                user.City = model.City;
                user.LastName = model.LastName;
                user.Name = model.Name;
                user.PostalCode = model.PostalCode;
                user.Number = model.Number;
                user.PhoneNumber = model.Phone;
                user.Street = model.Street;
                user.KontoFirmowe = false;
                await _userManager.UpdateAsync(user);
                return JavaScript(string.Format("document.location = '{0}';", Url.Action(MVC.Home.Index())));
            }
            return PartialView("_ChangeUserInfoViewModel", model);
        }

        //GET: User data - konto firmowe
        [HttpGet]
        public virtual async Task<ActionResult> ChangeUserFirmaInfoViewModel()
        {
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
            var model = new ChangeUserFirmaInfoViewModel
            {
                City = user.City ?? "Nie podano",
                Email = user.Email,
                NazwaFirmy = user.NazwaFirmy ?? "Nie podano",
                Nip = user.AccNip ?? "Nie podano",
                Number = user.Number ?? "Nie podano",
                Phone = user.PhoneNumber ?? "Nie podano",
                PostalCode = user.PostalCode ?? "Nie podano",
                Street = user.Street ?? "Nie podano"
            };
            return PartialView("_ChangeUserFirmaInfoViewModel", model);
        }

        //GET: change User data - konto firmowe
        [HttpPost]
        public virtual async Task<ActionResult> ChangeUserFirmaInfoViewModel(ChangeUserFirmaInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
                user.City = model.City;
                user.NazwaFirmy = model.NazwaFirmy;
                user.AccNip = model.Nip;
                user.PostalCode = model.PostalCode;
                user.Number = model.Number;
                user.PhoneNumber = model.Phone;
                user.Street = model.Street;
                user.KontoFirmowe = true;
                await _userManager.UpdateAsync(user);
                return JavaScript(string.Format("document.location = '{0}';", Url.Action(MVC.Home.Index())));
            }
            return PartialView("_ChangeUserFirmaInfoViewModel", model);
        }

        ////
        //// GET: /Manage/SetPassword
        //public virtual ActionResult UstawHaslo()
        //{
        //    return View();
        //}

        ////
        //// POST: /Manage/SetPassword
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public virtual async Task<ActionResult> UstawHaslo(SetPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result =
        //            await _userManager.AddPasswordAsync(int.Parse(User.Identity.GetUserId()), model.NewPassword);
        //        if (result.Succeeded)
        //        {
        //            var user = await _userManager.FindByIdAsync(int.Parse(User.Identity.GetUserId()));
        //            if (user != null)
        //            {
        //                await SignInAsync(user, isPersistent: false);
        //            }
        //            return RedirectToAction(MVC.Konto.Index());
        //        }
        //        AddErrors(result);
        //    }
        //    return View(model);
        //}

        #region Helpers

        private static string ActivationMailBuilder(string callbackUrl)
        {
            var body = new StringBuilder();
            body.Append("Aktywuj swoje konto klikając <a href=\"" + callbackUrl + "\">tutaj</a>");
            return body.ToString();
        }

        private static string ResetPasswordMailBuilder(string callbackUrl)
        {
            var body = new StringBuilder();
            body.Append("Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
            return body.ToString();
        }

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get { return HttpContext.GetOwinContext().Authentication; }
        //}

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


        private bool HasPassword()
        {
            var user = _userManager.FindById(int.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}