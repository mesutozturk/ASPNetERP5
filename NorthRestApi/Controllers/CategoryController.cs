using NorthRestApi.Models;
using NorthRestApi.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace NorthRestApi.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpGet]
        public ResponseData GetAll()
        {
            try
            {
                var db = new NorthwindEntities();
                var kategoriler = db.Categories.Select(x => new
                {
                    x.CategoryID,
                    x.CategoryName,
                    x.Description,
                    ProductsCount = x.Products.Count
                }).ToList();
                return new ResponseData()
                {
                    Success = true,
                    Data = kategoriler
                };
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpGet]
        public ResponseData GetById(int id)
        {
            try
            {
                var db = new NorthwindEntities();
                var category = db.Categories.Find(id);
                if (category == null)
                    return new ResponseData()
                    {
                        Message = "Kategori bulunamadı",
                        Success = false
                    };
                return new ResponseData()
                {
                    Success = true,
                    Data = new
                    {
                        category.CategoryID,
                        category.CategoryName,
                        category.Description,
                        ProductsCount = category.Products.Count
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpPost]
        public async Task<ResponseData> Insert([FromBody]CategoryViewModel value)
        {
            try
            {
                var db = new NorthwindEntities();
                db.Categories.Add(new Category()
                {
                    CategoryName = value.CategoryName,
                    Description = value.Description
                });
                await db.SaveChangesAsync();
                return new ResponseData()
                {
                    Message = $"{value.CategoryName} isimli kategori başarıyla eklenmiştir",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpPost]
        public ResponseData Update(int id, [FromBody] CategoryViewModel value)
        {
            try
            {
                var db = new NorthwindEntities();
                var category = db.Categories.Find(id);
                if (category == null)
                    return new ResponseData()
                    {
                        Message = "Kategori bulunamadı",
                        Success = false
                    };
                category.CategoryName = value.CategoryName;
                category.Description = value.Description;
                db.SaveChanges();
                return new ResponseData()
                {
                    Message = $"{value.CategoryName} kategori güncelleme işlemi başarılı",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpDelete]
        public ResponseData Delete(int id)
        {
            try
            {
                var db = new NorthwindEntities();
                var category = db.Categories.Find(id);
                if (category == null)
                    return new ResponseData()
                    {
                        Message = "Kategori bulunamadı",
                        Success = false
                    };
                if (category.Products.Any())
                    throw new Exception("Kategoriye bağlı ürünler olduğundan silme işlemi başarısız olmuştur");
                db.Categories.Remove(category);
                db.SaveChanges();
                return new ResponseData()
                {
                    Message = $"{category.CategoryName} isimli kategori silinmiştir",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
