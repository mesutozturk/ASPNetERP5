using NorthRestApi.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using NorthRestApi.Models.ViewModels;

namespace NorthRestApi.Controllers
{
    [RoutePrefix("api/urun")]
    public class ProductController : ApiController
    {
        [HttpGet]
        [Route("")]
        public ResponseData GetAll()
        {
            try
            {
                var db = new NorthwindEntities();
                var urunler = db.Products.Select(x => new
                {
                    x.CategoryID,
                    x.Category.CategoryName,
                    x.ProductID,
                    x.ProductName,
                    x.UnitPrice,
                    TotalOrder = x.Order_Details.Count
                }).ToList();
                return new ResponseData()
                {
                    Success = true,
                    Data = urunler
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
        [Route("getir/{id:int:min(1)}")]
        public ResponseData GetById(int id)
        {
            try
            {
                var db = new NorthwindEntities();
                var urun = db.Products.Find(id);
                if (urun == null)
                    return new ResponseData()
                    {
                        Message = "Ürün bulunamadı",
                        Success = false
                    };
                return new ResponseData()
                {
                    Success = true,
                    Data = new
                    {
                        urun.CategoryID,
                        urun.Category.CategoryName,
                        urun.ProductID,
                        urun.ProductName,
                        urun.UnitPrice,
                        TotalOrder = urun.Order_Details.Count
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

        [Route("{cid:int:min(0)}")]
        public ResponseData GetAllByCatId(int cid)
        {
            try
            {
                var db = new NorthwindEntities();
                var urunler = db.Products
                    .Where(x => x.CategoryID == cid)
                    .Select(x => new
                    {
                        x.CategoryID,
                        x.Category.CategoryName,
                        x.ProductID,
                        x.ProductName,
                        x.UnitPrice,
                        TotalOrder = x.Order_Details.Count
                    }).ToList();
                return new ResponseData()
                {
                    Success = true,
                    Data = urunler
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
        [Route("ekle")]
        public async Task<ResponseData> Insert([FromBody] ProductViewModel value)
        {
            try
            {
                var db = new NorthwindEntities();
                db.Products.Add(new Product()
                {
                    CategoryID = value.CategoryId,
                    ProductName = value.ProductName,
                    UnitPrice = value.UnitPrice
                });
                await db.SaveChangesAsync();
                return new ResponseData()
                {
                    Success = true,
                    Message = $"{value.ProductName} isimli ürün ekleme işlemi başarılı"
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
