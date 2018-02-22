using Kimlik.DAL;
using Kimlik.Models.IdentityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Kimlik.BLL.Account
{
    public static class MembershipTools
    {
        public static UserStore<ApplicationUser> NewUserStore()
        {
            return new UserStore<ApplicationUser>(new MyContext());
        }

        public static UserManager<ApplicationUser> NewUserManager()
        {
            return new UserManager<ApplicationUser>(NewUserStore());
        }

        public static RoleStore<ApplicationRole> NewRoleStore()
        {
            return new RoleStore<ApplicationRole>(new MyContext());
        }

        public static RoleManager<ApplicationRole> NewRoleManager()
        {
            return new RoleManager<ApplicationRole>(NewRoleStore());
        }
    }
}
