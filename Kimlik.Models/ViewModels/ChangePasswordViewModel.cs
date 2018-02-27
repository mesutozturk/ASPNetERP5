using System.ComponentModel.DataAnnotations;

namespace Kimlik.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Eski Şifre")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

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
