using System.ComponentModel.DataAnnotations;

namespace Kimlik.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Soyad")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Şifre Tekrar")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; }
    }
}
