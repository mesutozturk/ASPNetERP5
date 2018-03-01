using System.ComponentModel.DataAnnotations;

namespace NorthRestApi.Models.ViewModels
{
    public class ProductViewModel
    {
        public int? CategoryId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }
}