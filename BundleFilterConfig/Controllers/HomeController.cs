using BundleFilterConfig.Services;
using System.Web.Mvc;

namespace BundleFilterConfig.Controllers
{
    [HandleError(View = "Error")]
    [ExceptionHandlerFilter]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}