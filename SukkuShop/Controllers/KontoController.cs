using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SukkuShop.Models;

namespace SukkuShop.Controllers
{
    [Authorize]
    public class KontoController : Controller
    {
        private ApplicationUserManager _userManager;

        public KontoController()
        {
        }

        public KontoController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
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
        public ActionResult Zaloguj(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Manage");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Zaloguj(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //if (!UserManager.IsEmailConfirmed(UserManager.FindByEmail(model.Email).Id))
            //{
            //    return View("NonActiveAccount", (object) model.Email);
            //}
            var result =
                await
                    SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                        shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }


        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Manage");
            return View();
        }
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {UserName = model.Email, Email = model.Email};
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code }, Request.Url.Scheme);
                    //await UserManager.SendEmailAsync(user.Id, "Confirm your account", ActivationMailBuilder(callbackUrl));
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            return View(model);
        }


        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(int userId, string code)
        {
            if (code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }
                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new {userId = user.Id, code},
                    Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", ResetPasswordMailBuilder(callbackUrl));
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        

        ////////////MANAGE///////////////
        public async Task<ActionResult> EditUserInfo()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<int>());
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
        public async Task<ActionResult> EditUserInfo(ChangeUserInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<int>());
                user.City = model.City;
                user.LastName = model.LastName;
                user.Name = model.Name;
                user.PostalCode = model.PostalCode;
                user.Number = model.Number;
                user.PhoneNumber = model.Phone;
                user.Street = model.Street;
                await UserManager.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            return View(model);
        }


        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
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
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<int>());
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
                    await AuthenticationManager.TwoFactorBrowserRememberedAsync(User.Identity.GetUserId()),
                ChangeUserInfoViewModel = userInfo,
                IsNull = isNull
            };
            return View(model);
        }


        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result =
                await
                    UserManager.ChangePasswordAsync(int.Parse(User.Identity.GetUserId()), model.OldPassword,
                        model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(int.Parse(User.Identity.GetUserId()));
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(int.Parse(User.Identity.GetUserId()), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(int.Parse(User.Identity.GetUserId()));
                    if (user != null)
                    {
                        await SignInAsync(user, isPersistent: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
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

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
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
            return RedirectToAction("Index", "Home");
        }


        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie,
                DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent },
                await user.GenerateUserIdentityAsync(UserManager));
        }



        private bool HasPassword()
        {
            var user = UserManager.FindById(int.Parse(User.Identity.GetUserId()));
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