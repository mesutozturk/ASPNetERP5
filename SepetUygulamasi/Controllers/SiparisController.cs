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
        private static int mevcutSayfa = 1;

        private static int sayfadaGosterilecekAdet = 20;

        // GET: Siparis
        public ActionResult Index()
        {
            ViewBag.toplam = Math.Ceiling(new NorthwindEntities().Products.Count() / (sayfadaGosterilecekAdet * 1d));
            return View();
        }
        [HttpGet]
        public JsonResult Urunler(int sayfa = 1)
        {
            mevcutSayfa = sayfa;
            try
            {
                int a = 5;
#if DEBUG
                a = 10;
#else
a=20;
#endif
                var db = new NorthwindEntities();
                var urunler = db.Products.Select(x => new
                {
                    x.ProductID,
                    x.ProductName,
                    x.Categories.CategoryName,
                    x.UnitPrice,
                    x.UnitsInStock
                }).ToList()
                    .Skip((mevcutSayfa - 1) * sayfadaGosterilecekAdet)
                    .Take(sayfadaGosterilecekAdet).ToList();
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
        public JsonResult Urunler(List<SepetModel> model)
        {
            var db = new NorthwindEntities();

            using (var tran = db.Database.BeginTransaction())
            {
                try
                {
                    var siparis = new Orders()
                    {
                        EmployeeID = db.Employees.First().EmployeeID,
                        CustomerID = db.Customers.First().CustomerID,
                        Freight = 20,
                        OrderDate = DateTime.Now,
                        RequiredDate = DateTime.Now.AddDays(7),
                        ShipCity = "Istanbul",
                        ShipVia = db.Shippers.First().ShipperID
                    };
                    db.Orders.Add(siparis);
                    db.SaveChanges();
                    foreach (var item in model)
                    {
                        db.Order_Details.Add(new Order_Details()
                        {
                            OrderID = siparis.OrderID,
                            ProductID = item.ProductID,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                            Discount = 0
                        });
                    }
                    db.SaveChanges();
                    tran.Commit();
                    return Json(new ResponseModel()
                    {
                        message = $"{siparis.OrderID} nolu siparişiniz onaylanmıştır",
                        success = true
                    }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return Json(new ResponseModel()
                    {
                        success = false,
                        message = ex.Message
                    }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}