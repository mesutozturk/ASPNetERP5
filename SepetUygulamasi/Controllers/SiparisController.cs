using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SepetUygulamasi.Models;

namespace SepetUygulamasi.Controllers
{
    public class SiparisController : Controller
    {
        // GET: Siparis
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Urunler()
        {
            try
            {
                var db = new NorthwindEntities();
                var urunler = db.Products.Select(x => new
                {
                    x.ProductID,
                    x.ProductName,
                    x.Categories.CategoryName,
                    x.UnitPrice,
                    x.UnitsInStock
                }).ToList();
                return Json(new ResponseModel()
                {
                    data = urunler,
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResponseModel()
                {
                    success = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Urunler()
        {

        }
    }
}