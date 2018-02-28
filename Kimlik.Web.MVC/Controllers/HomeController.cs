using System.Web.Mvc;

namespace Kimlik.Web.MVC.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class HomeController : Controller
    {
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}