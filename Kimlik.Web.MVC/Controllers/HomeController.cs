using System.Web.Mvc;

namespace Kimlik.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //[Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}