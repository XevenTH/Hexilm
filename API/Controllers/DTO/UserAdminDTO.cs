namespace API.Controllers.DTO;

public class UserAdminDTO
{
    public string Displayname { get; set; }    
    public string Username { get; set; }
    public bool IsAdmin { get; set; }
    public string Token { get; set; }
}
