using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}