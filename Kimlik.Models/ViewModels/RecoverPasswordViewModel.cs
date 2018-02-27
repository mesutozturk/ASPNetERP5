using System.ComponentModel.DataAnnotations;

namespace Kimlik.Models.ViewModels
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
    }
}
