using System.ComponentModel.DataAnnotations;

namespace API.Controllers.DTO;

public class AuthDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public string RoleName { get; set; }
}
