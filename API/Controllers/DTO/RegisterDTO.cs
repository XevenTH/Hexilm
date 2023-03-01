using System.ComponentModel.DataAnnotations;

namespace Controllers.DTO;

public class RegisterDTO
{
    [Required]
    public string Email { get; set; }    
    [Required]
    public string Username { get; set; }    
    [Required]
    public string Displayname { get; set; }    
    [Required]
    public string Password { get; set; }  
}
