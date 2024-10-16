using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class LoginDTO
{
    [Required]
    [EmailAddress]
    public string Mail { get; set; }
    
    [Required]
    public string Password { get; set; }
}
}