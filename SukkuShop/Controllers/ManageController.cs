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
    public class ManageController : Controller
    {
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }


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
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                BrowserRemembered =
                    await AuthenticationManager.TwoFactorBrowserRememberedAsync(User.Identity.GetUserId()),
                    ChangeUserInfoViewModel = userInfo
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
                return RedirectToAction("Index", new {Message = ManageMessageId.ChangePasswordSuccess});
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
                    return RedirectToAction("Index", new {Message = ManageMessageId.SetPasswordSuccess});
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #region Helpers

        // Used for XSRF protection when adding external logins

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie,
                DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = isPersistent},
                await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
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