using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SukkuShop.Models;

namespace SukkuShop.Identity
{
    public class ApplicationRoleManager : RoleManager<RoleIntPk, int>
    {
        public ApplicationRoleManager(IRoleStore<RoleIntPk, int> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStoreIntPk(context.Get<ApplicationDbContext>()));
        }
    }
}