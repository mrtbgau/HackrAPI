using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Droits
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        [Required]
        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }
    }
}