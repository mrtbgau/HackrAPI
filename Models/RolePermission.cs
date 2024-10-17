using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        [Required]
        public virtual Role Role { get; set; }
        [Required]
        public virtual Permission Permission { get; set; }
    }
}