using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCFundamentals.Models;

namespace MVCFundamentals.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var db = new NorthwindEntities();
                var model = db.Categories.OrderBy(x => x.CategoryName).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                return View(new List<Category>());
            }
        }
        [HttpGet]
        public ActionResult Detail(int id = 0)
        {
            try
            {
                var db = new NorthwindEntities();
                var model = db.Categories.Find(id);
                if (model == null)
                    return RedirectToAction("Index", "Category");
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Category");
            }
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            try
            {
                var db = new NorthwindEntities();
                db.Categories.Add(new Category()
                {
                    CategoryName = model.CategoryName,
                    Description = model.Description
                });
                db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }

        }
        [HttpGet]
        public ActionResult Update(int? id)
        {
            try
            {
                var db = new NorthwindEntities();
                var model = db.Categories.Find(id);
                if (model == null)
                    return RedirectToAction("Index", "Category");
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Category");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Category model)
        {
            try
            {
                var db = new NorthwindEntities();
                var category = db.Categories.Find(model.CategoryID);
                if (category == null)
                    return RedirectToAction("Index", "Category");
                category.CategoryName = model.CategoryName;
                category.Description = model.Description;
                db.SaveChanges();
                return RedirectToAction("Update", "Category", new { id = category.CategoryID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index", "Category");
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                var db = new NorthwindEntities();
                var silinecek = db.Categories.Find(id);
                if (silinecek == null)
                    return RedirectToAction("Index", "Category");
                db.Categories.Remove(silinecek);
                db.SaveChanges();
                ViewBag.sonuc = $"{silinecek.CategoryName} isimli kategori başarıyla silinmiştir";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.sonuc = $"Kategori silme işleminde bir hata oluştu <br>{ex.Message}";
                return View();
            }
        }
    }
}