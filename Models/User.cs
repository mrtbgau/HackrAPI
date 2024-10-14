using System.ComponentModel.DataAnnotations;

namespace API.Models{
    public class User {
        [Key]
        public int UserID {get; set;}
        [Required]
        public required string UserName {get; set;}
        [Required]
        public required byte[] UserPWD {get; set;}
    }
}