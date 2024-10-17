using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class RolePermission
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public int PermissionId { get; set; }
        [Required]
        public virtual Role Role { get; set; }
        [Required]
        public virtual Permission Permission { get; set; }
    }
}