using System.ComponentModel.DataAnnotations;

namespace API.Models.Droits
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]
        public virtual Role Role { get; set; }
    }
}