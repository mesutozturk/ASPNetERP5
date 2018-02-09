using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LayoutKullanimi.Models;

namespace LayoutKullanimi.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.mesaj = "Hello Layout";
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public PartialViewResult HeaderPartialResult()
        {
            var urunler = new NorthwindEntities().Products.ToList();
            return PartialView("_HeaderPartial", urunler);
        }
    }
}