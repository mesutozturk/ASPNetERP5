using System.Web.Mvc;
using System.Web.Routing;
using Kimlik.BLL.Account;
using Kimlik.Models.IdentityModels;
using Microsoft.AspNet.Identity;

namespace Kimlik.Web.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var roleManager = MembershipTools.NewRoleManager();
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new ApplicationRole()
                {
                    Name = "Admin",
                    Description = "Sistem Yöneticisi"
                });
            }
            if (!roleManager.RoleExists("User"))
            {
                roleManager.Create(new ApplicationRole()
                {
                    Name = "User",
                    Description = "Site Kullanıcısı"
                });
            }
            if (!roleManager.RoleExists("Passive"))
            {
                roleManager.Create(new ApplicationRole()
                {
                    Name = "Passive",
                    Description = "Aktivasyo Yapmamış Kullanıcı Rolu"
                });
            }
        }
    }
}
