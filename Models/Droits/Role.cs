using System.ComponentModel.DataAnnotations;

namespace API.Models.Droits
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public virtual ICollection<RolePermission>? RolePermissions { get; set; }
        [Required]
        public virtual ICollection<UserRole>? UserRoles { get; set; }
    }
}