using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kimlik.Models.IdentityModels;

namespace Kimlik.Models.Entites
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public DateTime MessageDate { get; set; } = DateTime.Now;
        [Required]
        public string Content { get; set; }

        public string SenderId { get; set; }

        [ForeignKey("SenderId")]
        public virtual ApplicationUser Sender { get; set; }
    }
}
