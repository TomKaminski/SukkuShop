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
            container.RegisterType<ApplicationDbContext>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationSignInManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationRoleManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());

            container.RegisterType<IAuthenticationManager>(new PerRequestLifetimeManager(),
                new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterType<IUserStore<ApplicationUser, int>, UserStoreIntPk>(new PerRequestLifetimeManager(),
                new InjectionConstructor(typeof (ApplicationDbContext)));

            container.RegisterType<IRoleStore<RoleIntPk, int>, RoleStoreIntPk>(new PerRequestLifetimeManager(),
                new InjectionConstructor(typeof(ApplicationDbContext)));

        }
    }
}