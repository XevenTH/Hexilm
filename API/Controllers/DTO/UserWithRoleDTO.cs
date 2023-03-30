namespace API.Controllers.DTO;

public class UserWithRoleDTO
{
    public string Id { get; set; }
    public string DisplayName { get; set; }    
    public string Username { get; set; }
    public string Photo { get; set; } = string.Empty;
    public string Role { get; set; }
    public string Token { get; set; }
}
