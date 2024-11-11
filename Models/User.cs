using API.Models.Droits;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models{
    public class User {
        [Key]
        public int UserID {get; set;}
        [Required]
        public string? UserName {get; set;}
        [Required]
        public string? mail {get; set;}
        [Required]
        public byte[]? UserPWD {get; set;}
        [Required]
        public byte[]? PasswordSalt { get; set; }
        [Required]
        public bool IsAdmin {get; set;}
        [Required]
        public int RoleId { get; set; }
        [Required]
        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }
    }
}