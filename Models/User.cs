using System.ComponentModel.DataAnnotations;
using API.Models.Droits;

namespace API.Models{
    public class User {
        [Key]
        public int UserID {get; set;}
        [Required]
        public string UserName {get; set;}
        [Required]
        public string mail {get; set;}
        [Required]
        public byte[] UserPWD {get; set;}
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required]
        public bool IsAdmin {get; set;}
        [Required]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}