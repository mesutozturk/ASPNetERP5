using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RE.BLL.Repository;

namespace RE.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //var urunler = new ProductRepo().GetAll();
            //var satistakiUrunler =new ProductRepo().GetAll().Where(x=>x.Discontinued==false).ToList();
            var satistakiUrunler = new ProductRepo().Queryable().Where(x => x.Discontinued == false).ToList();
            return View(satistakiUrunler);
        }
    }
}