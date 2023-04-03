using System.ComponentModel.DataAnnotations;

namespace Application.Profile.DTO;

public class UpdateProfileDTO
{
    public string DisplayName { get; set; } = string.Empty;
    [RegularExpression(@"^\S*$", ErrorMessage = "The {0} field should not contain white space.")]
    public string UserName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
}