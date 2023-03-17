using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Controllers.DTO;

public class RegisterDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string DisplayName { get; set; }

    [JsonPropertyName("Password")]
    [Required(ErrorMessage = "Password harus diisi.")]
    [CustomValidation(typeof(RegisterDTO), "ValidatePassword")]
    public string password { get; set; }

    public static ValidationResult ValidatePassword(string password, ValidationContext context)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            return new ValidationResult("Password Cant Be Empty.");
        }

        if (password.Length < 8)
        {
            return new ValidationResult("Password Have Atleast 8 Character.");
        }

        if (password.Length > 16)
        {
            return new ValidationResult("Password Can't be more than 16 Character.");
        }

        if (!password.Any(char.IsDigit))
        {
            return new ValidationResult("Password Have Atleast 1 Number.");
        }

        if (!password.Any(char.IsUpper))
        {
            return new ValidationResult("Password Have Atleast 1 UpperCase Character.");
        }

        return ValidationResult.Success;
    }
}
