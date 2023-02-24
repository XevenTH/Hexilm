using Microsoft.AspNetCore.Identity;

namespace Model;

public class UserApp : IdentityUser
{
    public string DisplayName { get; set; }
    public string Bio { get; set; }
}
