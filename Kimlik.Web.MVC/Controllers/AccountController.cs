using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kimlik.BLL.Account;
using Kimlik.BLL.Settings;
using Kimlik.Models.IdentityModels;
using Kimlik.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using static Kimlik.BLL.Account.MembershipTools;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userManager = NewUserManager();
            var user = await userManager.FindAsync(model.UserName, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                return View();
            }

            var authManager = HttpContext.GetOwinContext().Authentication;
            var userIdentity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            authManager.SignIn(new AuthenticationProperties()
            {
                IsPersistent = model.RememberMe
            }, userIdentity);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<ActionResult> Profile()
        {
            var userManager = NewUserManager();
            var user = await userManager.FindByIdAsync(HttpContext.User.Identity.GetUserId());
            var model = new ProfilePasswordMViewModel();
            var model1 = new ProfileViewModel()
            {
                Email = user.Email,
                Name = user.Name,
                RegisterDate = user.RegisterDate,
                Surname = user.Surname,
                UserName = user.UserName
            };
            model.ProfileViewModel = model1;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Profile(ProfilePasswordMViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                var userStore = NewUserStore();
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = await userManager.FindByIdAsync(HttpContext.User.Identity.GetUserId());
                user.Name = model.ProfileViewModel.Name;
                user.Surname = model.ProfileViewModel.Surname;
                user.Email = model.ProfileViewModel.Email;

                await userStore.UpdateAsync(user);
                await userStore.Context.SaveChangesAsync();
                return RedirectToAction("Profile", "Account");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Güncelleme işleminde bir hata oluştu {ex.Message}");
                return View(model);
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ProfilePasswordMViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Profile");
            try
            {
                var userStore = NewUserStore();
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = await userManager.FindByIdAsync(HttpContext.User.Identity.GetUserId());

                user = await userManager.FindAsync(user.UserName, model.ChangePasswordViewModel.OldPassword);
                if (user == null)
                {
                    ModelState.AddModelError("OldPassword", "Mevcut şifreniz yanlış girildi");
                    return View("Profile");
                }

                await userStore.SetPasswordHashAsync(user,
                    userManager.PasswordHasher.HashPassword(model.ChangePasswordViewModel.Password));
                await userStore.UpdateAsync(user);
                await userStore.Context.SaveChangesAsync();
                HttpContext.GetOwinContext().Authentication.SignOut();
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Güncelleme işleminde bir hata oluştu {ex.Message}");
                return View("Profile");
            }
        }

        public ActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var userStore = NewUserStore();
            var userManager = new UserManager<ApplicationUser>(userStore);

            var user1 = await userManager.FindByEmailAsync(model.Email);
            if (user1 == null)
            {
                ModelState.AddModelError("", "Lütfen geçerli bir email adresi giriniz");
                return View(model);
            }

            var user2 = await userManager.FindByNameAsync(model.UserName);
            if (user2 == null)
            {
                ModelState.AddModelError("", "Lütfen geçerli bir kullanıcı adı giriniz");
                return View(model);
            }

            if (user1.Id != user2.Id)
            {
                ModelState.AddModelError("", "Girdiğiniz bilgiler geçerli değil");
                return View(model);
            }

            var randomPass = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
            await userStore.SetPasswordHashAsync(user1, userManager.PasswordHasher.HashPassword(randomPass));
            await userStore.Context.SaveChangesAsync();

            await SiteSettings.SendMail(new MailModel()
            {
                To = user1.Email,
                Subject = "Şifreniz Değişti",
                Message = $"Merhaba {user1.Name} <br>Yeni Şifreniz: <b>{randomPass}</b>"
            });
            return RedirectToAction("Login", "Account");
        }
    }
}