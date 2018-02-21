using System;
using RE.BLL.Repository;
using System.Threading.Tasks;
using System.Web.Mvc;
using RE.Models.Entities;

namespace RE.Web.MVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public async Task<ActionResult> Index()
        {
            var kategoriler = await new CategoryRepo().GetAllAsync();
            return View(kategoriler);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Category");

            var kategori = await new CategoryRepo().GetByIdAsync(id.Value);
            if (kategori == null)
                return RedirectToAction("Index", "Category");

            return View(kategori);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Category model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Güncelleme işleminde bir hata oluştu");
                return View(model);
            }
            try
            {
                var kategori = await new CategoryRepo().GetByIdAsync(model.CategoryID);
                if (kategori == null)
                {
                    ModelState.AddModelError("", "Kategori bulunamadı");
                    return View(model);
                }
                kategori.CategoryName = model.CategoryName;
                kategori.Description = model.Description;
                await new CategoryRepo().UpdateAsync();
                return RedirectToAction("Edit", "Category", new {kategori.CategoryID});
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}