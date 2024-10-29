using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Droits
{
    public class RolePermission
    {
        [Key]
        public int RoleId { get; set; }
        [Key]
        public int PermissionId { get; set; }
        [Required]
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        [Required]
        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; }
    }
}