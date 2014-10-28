using Microsoft.AspNet.Identity;
using SukkuShop.Models;

namespace SukkuShop.Identity
{
    public class ApplicationRoleManager : RoleManager<RoleIntPk, int>
    {
        public ApplicationRoleManager(IRoleStore<RoleIntPk, int> roleStore)
            : base(roleStore)
        {
        }
    }
}