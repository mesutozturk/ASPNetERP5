using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCFundamentals.Models;

namespace MVCFundamentals.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new NorthwindEntities();
            var model = db.Categories.OrderBy(x => x.CategoryName).ToList();
            ViewBag.durum = "Aktif";
            return View(model);
        }

        public ActionResult Detail(int? catid)
        {
            if (catid == null)
                return RedirectToAction("Index", "Home");
            var db = new NorthwindEntities();
            var category = db.Categories.Find(catid.Value);
            if (category == null)
                return RedirectToAction("Index", "Home");
            return View(category);
        }

        public ActionResult ProductDetail(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            var db = new NorthwindEntities();
            var urun = db.Products.Find(id.Value);
            if (urun == null)
                return RedirectToAction("Index", "Home");
            return View(urun);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}