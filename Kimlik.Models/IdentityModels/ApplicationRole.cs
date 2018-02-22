using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Kimlik.Models.IdentityModels
{
    public class ApplicationRole : IdentityRole
    {
        [StringLength(200)]
        public string Description { get; set; }
    }
}
