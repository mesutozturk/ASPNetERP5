using RE.BLL.Repository;
using System.Linq;
using System.Web.Mvc;

namespace RE.Web.MVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var urunler = new ProductRepo().Queryable().Where(x => x.Discontinued == false).OrderBy(x => x.ProductName).ToList();
            return View(urunler);
        }
    }
}