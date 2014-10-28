using System;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using SukkuShop.Identity;
using SukkuShop.Models;

namespace SukkuShop
{
    public class UnityConfig
    {
        #region Unity Container

        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ApplicationDbContext>();
            container.RegisterType<ApplicationSignInManager>();
            container.RegisterType<ApplicationRoleManager>();
            container.RegisterType<ApplicationUserManager>();

            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterType<IUserStore<ApplicationUser, int>, UserStoreIntPk>(
                new InjectionConstructor(typeof (ApplicationDbContext)));

            container.RegisterType<IRoleStore<RoleIntPk, int>, RoleStoreIntPk>(
                new InjectionConstructor(typeof(ApplicationDbContext)));

        }
    }
}