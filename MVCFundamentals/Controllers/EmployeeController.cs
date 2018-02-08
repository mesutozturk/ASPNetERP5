using System;
using System.Collections.Generic;
using MVCFundamentals.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCFundamentals.Controllers
{
    public class EmployeeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var db = new NorthwindEntities();
            var model = await db.Employees
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToListAsync();

            return View(model);
        }

        public async Task<ActionResult> Detail(int? id)
        {
            try
            {
                var db = new NorthwindEntities();
                var model = await db.Employees.FindAsync(id);
                if (model == null)
                    return RedirectToAction("Index", "Employee");
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Employee");
            }
        }
        [HttpGet]
        public ActionResult Add()
        {
            var calisanlarList = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Text = "Amiri Yok",
                    Value = "null"
                }
            };
            var db = new NorthwindEntities();
            db.Employees
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList()
                .ForEach(x =>
                {
                    calisanlarList.Add(new SelectListItem
                    {
                        Text = $"{x.FirstName} {x.LastName} - {x.Title}",
                        Value = x.EmployeeID.ToString()
                    });
                });
            ViewBag.calisanlarList = calisanlarList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(Employee model)
        {
            if (model == null)
                return RedirectToAction("Index", "Employee");
            try
            {
                var db = new NorthwindEntities();
                var eklenecek = new Employee()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Title = model.Title,
                    ReportsTo = model.ReportsTo
                };
                db.Employees.Add(eklenecek);
                await db.SaveChangesAsync();
                return RedirectToAction("Detail", "Employee", new { id = eklenecek.EmployeeID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Update(int? id)
        {
            var calisanlarList = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Text = "Amiri Yok",
                    Value = "null"
                }
            };
            var db = new NorthwindEntities();
            var guncellenecek = await db.Employees.FindAsync(id);
            if (guncellenecek == null)
                return RedirectToAction("Index", "Employee");
            db.Employees
                .Where(x => x.EmployeeID != guncellenecek.EmployeeID)
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList()
                .ForEach(x =>
                {
                    calisanlarList.Add(new SelectListItem
                    {
                        Text = $"{x.FirstName} {x.LastName} - {x.Title}",
                        Value = x.EmployeeID.ToString()
                    });
                });
            ViewBag.calisanlarList = calisanlarList;
            return View(guncellenecek);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Employee model)
        {
            if (model == null)
                return RedirectToAction("Index", "Employee");
            try
            {
                var db = new NorthwindEntities();
                var guncellenecek = db.Employees.Find(model.EmployeeID);
                if (guncellenecek == null)
                    return RedirectToAction("Index", "Employee");
                guncellenecek.FirstName = model.FirstName;
                guncellenecek.LastName = model.LastName;
                guncellenecek.ReportsTo = model.ReportsTo;
                guncellenecek.Title = model.Title;
                await db.SaveChangesAsync();
                return RedirectToAction("Detail", "Employee", new { id = guncellenecek.EmployeeID });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Update", "Employee", new { id = model.EmployeeID });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                var db = new NorthwindEntities();
                var silinecek = await db.Employees.FindAsync(id);
                if (silinecek == null)
                    return RedirectToAction("Index", "Employee");
                db.Employees.Remove(silinecek);
                await db.SaveChangesAsync();
                ViewBag.sonuc = $"{silinecek.FirstName} {silinecek.LastName} isimli çalışan silinmiştir";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.sonuc = $"Çalışan silme işleminde bir hata oluştu <br/> {ex.Message}";
                return View();
            }
        }
    }
}