using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SukkuShop.Models;

namespace SukkuShop.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser, int>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store)
            : base(store)
        {
            //var manager = new ApplicationUserManager(new UserStoreIntPk(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            UserValidator = new UserValidator<ApplicationUser, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                //RequireNonLetterOrDigit = true,
                //RequireDigit = true,
                //RequireLowercase = true,
                //RequireUppercase = true,
            };

            // Configure user lockout defaults
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;
            EmailService = new EmailService();

            var dataProtectionProvider = Startup.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser, int>(
                        dataProtectionProvider.Create("ASP.NET Identity"));
            }
        }

        //public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
        //    IOwinContext context)
        //{
            //var manager = new ApplicationUserManager(new UserStoreIntPk(context.Get<ApplicationDbContext>()));
            //// Configure validation logic for usernames
            //manager.UserValidator = new UserValidator<ApplicationUser, int>(manager)
            //{
            //    AllowOnlyAlphanumericUserNames = false,
            //    RequireUniqueEmail = true
            //};

            //// Configure validation logic for passwords
            //manager.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 6,
            //    //RequireNonLetterOrDigit = true,
            //    //RequireDigit = true,
            //    //RequireLowercase = true,
            //    //RequireUppercase = true,
            //};

            //// Configure user lockout defaults
            //manager.UserLockoutEnabledByDefault = true;
            //manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            //manager.EmailService = new EmailService();
            //var dataProtectionProvider = options.DataProtectionProvider;
            //if (dataProtectionProvider != null)
            //{
            //    manager.UserTokenProvider =
            //        new DataProtectorTokenProvider<ApplicationUser, int>(dataProtectionProvider.Create());
            //}
            //return manager;
        //}
    }
}