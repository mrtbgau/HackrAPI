using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UserRole
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]
        public virtual Role Role { get; set; }
    }
}