using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RE.BLL.Repository;
using RE.Web.MVC.Models;

namespace RE.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GrafikData1()
        {
            var kategoriler = await new CategoryRepo().GetAllAsync();
            var liste = new List<KategoriRaporModel>();
            foreach (var item in kategoriler)
            {
                liste.Add(new KategoriRaporModel()
                {
                    Ad = item.CategoryName,
                    Adet = item.Products.Count
                });
            }

            return Json(liste, JsonRequestBehavior.AllowGet);
        }
    }
}