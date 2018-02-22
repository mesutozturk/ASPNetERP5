using System.Data.Entity;
using Kimlik.Models.Entites;
using Kimlik.Models.IdentityModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Kimlik.DAL
{
    public class MyContext : IdentityDbContext<ApplicationUser>
    {
        public MyContext()
        : base("name=MyCon")
        {
        }

        public virtual DbSet<Message> Messages { get; set; }
    }
}
