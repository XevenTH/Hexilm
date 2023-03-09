using Microsoft.AspNetCore.Identity;

namespace Model;

public class UserApp : IdentityUser
{
    public string Displayname { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public ICollection<UserRoom> UserRooms { get; set; } = new List<UserRoom>();
}
