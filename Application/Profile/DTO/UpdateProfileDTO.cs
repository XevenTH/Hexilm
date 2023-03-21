namespace Application.Profile.DTO;

public class UpdateProfileDTO
{
    public string DisplayName { get; set; }
    public string UserName { get; set; }
    public string Bio { get; set; } = string.Empty;
}