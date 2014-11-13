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

        //[AllowAnonymous]
        //public async Task<ActionResult> ResendActivationEmail(string email)
        //{
        //    var userid = UserManager.FindByEmail(email).Id;
        //    var code = await UserManager.GenerateEmailConfirmationTokenAsync(userid);
        //    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userid, code }, Request.Url.Scheme);
        //    await UserManager.SendEmailAsync(userid, "Confirm your account", ActivationMailBuilder(callbackUrl));
        //    return RedirectToAction("Login");
        //}


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
            //if (!UserManager.IsEmailConfirmed(UserManager.FindByEmail(model.Email).Id))
            //{
            //    return View("NonActiveAccount", (object) model.Email);
            //}
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
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
                    Name = model.Name,
                    LastName = model.LastName,
                    PhoneNumber = model.Phone
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code }, Request.Url.Scheme);
                    //await UserManager.SendEmailAsync(user.Id, "Confirm your account", ActivationMailBuilder(callbackUrl));
                    await _signInManager.SignInAsync(user, false, false);
                    Session["username"] = user.Name;
                    return RedirectToAction(MVC.Home.Index());
                }
                AddErrors(result);
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
                return View(MVC.Shared.Views.Error);
            }
            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "PotwierdzonyMail" : "Blad");
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
            return code == null ? View(MVC.Shared.Views.Error) : View();
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


        ////////////MANAGE///////////////
        public virtual async Task<ActionResult> EdytujDane()
        {
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
            var userInfo = new ChangeUserInfoViewModel
            {
                Name = user.Name,
                City = user.City,
                LastName = user.LastName,
                Number = user.Number,
                Phone = user.PhoneNumber,
                PostalCode = user.PostalCode,
                Street = user.Street
            };
            return View(userInfo);
        }

        [HttpPost]
        public virtual async Task<ActionResult> EdytujDane(ChangeUserInfoViewModel model)
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
                await _userManager.UpdateAsync(user);
                return RedirectToAction(MVC.Konto.Index());
            }
            return View(model);
        }


        //
        // GET: /Manage/Index
        public virtual async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess
                    ? "Your password has been changed."
                    : message == ManageMessageId.SetPasswordSuccess
                        ? "Your password has been set."
                        : message == ManageMessageId.SetTwoFactorSuccess
                            ? "Your two-factor authentication provider has been set."
                            : message == ManageMessageId.Error
                                ? "An error has occurred."
                                : message == ManageMessageId.AddPhoneSuccess
                                    ? "Your phone number was added."
                                    : message == ManageMessageId.RemovePhoneSuccess
                                        ? "Your phone number was removed."
                                        : "";
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
            ;
            var isNull = false;

            var userInfo = new ChangeUserInfoViewModel
            {
                Name = user.Name,
                City = user.City,
                LastName = user.LastName,
                Number = user.Number,
                Phone = user.PhoneNumber,
                PostalCode = user.PostalCode,
                Street = user.Street
            };

            foreach (var prop in userInfo.GetType().GetProperties())
            {
                var propertyValue = prop.GetValue(userInfo);
                if (propertyValue == null)
                    isNull = true;
            }
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                BrowserRemembered =
                    await _authenticationManager.TwoFactorBrowserRememberedAsync(User.Identity.GetUserId()),
                ChangeUserInfoViewModel = userInfo,
                IsNull = isNull
            };
            return View(model);
        }


        //
        // GET: /Manage/ChangePassword
        public virtual ActionResult ZmienHaslo()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ZmienHaslo(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
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
                return RedirectToAction(MVC.Konto.Index(ManageMessageId.ChangePasswordSuccess));
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public virtual ActionResult UstawHaslo()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> UstawHaslo(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _userManager.AddPasswordAsync(int.Parse(User.Identity.GetUserId()), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByIdAsync(int.Parse(User.Identity.GetUserId()));
                    if (user != null)
                    {
                        await SignInAsync(user, isPersistent: false);
                    }
                    return RedirectToAction(MVC.Konto.Index(ManageMessageId.SetPasswordSuccess));
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #region Helpers

        private static string ActivationMailBuilder(string callbackUrl)
        {
            var body = new StringBuilder();
            body.Append("Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
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