using System;
using System.IO;
using RE.BLL.Repository;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using RE.BLL.Settings;
using RE.Models.Entities;

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

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Product");

            var urun = await new ProductRepo().GetByIdAsync(id.Value);

            if (urun == null) return RedirectToAction("Index", "Product");

            return View(urun);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Güncelleme işleminde bir hata oluştu");
                return View(model);
            }

            try
            {
                var urun = await new ProductRepo().GetByIdAsync(model.ProductID);
                if (urun == null)
                {
                    ModelState.AddModelError("", "Ürün bulunamadı");
                    return View(model);
                }

                urun.ProductName = model.ProductName;
                urun.UnitPrice = model.UnitPrice;
                if (model.Foto != null && model.Foto.ContentLength > 0)
                {
                    var dosya = model.Foto;
                    string fileName = Path.GetFileNameWithoutExtension(dosya.FileName);
                    string extName = Path.GetExtension(dosya.FileName);
                    fileName = SiteSettings.UrlFormatConverter(fileName);
                    fileName += Guid.NewGuid().ToString().Replace("-", "");
                    var directoryPath = Server.MapPath("~/Uploads/Products");
                    var filePath = Server.MapPath($"~/Uploads/Products/{fileName}{extName}");
                    if (!Directory.Exists(directoryPath))
                        Directory.CreateDirectory(directoryPath);
                    dosya.SaveAs(filePath);
                    WebImage img = new WebImage(filePath);
                    img.Resize(800, 800, false);
                    img.AddTextWatermark("Kuzey Ticaret", fontColor: "Tomato", fontSize: 18, fontFamily: "Verdana");
                    img.Save(filePath);
                    if (!string.IsNullOrEmpty(urun.FotoUrl))
                        System.IO.File.Delete(Server.MapPath(urun.FotoUrl));
                    urun.FotoUrl = $@"/Uploads/Products/{fileName}{extName}";
                }
                await new ProductRepo().UpdateAsync();
                return RedirectToAction("Edit", "Product", new { id = model.ProductID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}