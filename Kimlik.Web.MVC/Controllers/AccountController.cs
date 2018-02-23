using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kimlik.BLL.Account;
using Kimlik.Models.IdentityModels;
using Kimlik.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace Kimlik.Web.MVC.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            var userManager = MembershipTools.NewUserManager();
            var checkUser = userManager.FindByName(model.UserName);
            if (checkUser != null)
            {
                ModelState.AddModelError("UserName", "Bu kullanıcı adı daha önceden alınmıştır.");
                return View(model);
            }
            checkUser = userManager.FindByEmail(model.Email);
            if (checkUser != null)
            {
                ModelState.AddModelError("Email", "Bu e-posta adresi daha önceden alınmıştır.");
                return View(model);
            }

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.UserName,
                Name = model.Name,
                Surname = model.Surname
            };
            var result = userManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id, userManager.Users.Count() == 1 ? "Admin" : "User");
                return RedirectToAction("Login", "Account");
            }
            ModelState.AddModelError("", "Kullanıcı kayıt işleminde bir hata oluştu");
            return View(model);
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}