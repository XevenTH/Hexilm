using Microsoft.AspNetCore.Identity;

namespace Model;

public class UserApp : IdentityUser
{
    public string DisplayName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public ICollection<Photo> Photo { get; set; } = new List<Photo>();
    public ICollection<FavoriteMovies> FavoriteMovies { get; set; } = new List<FavoriteMovies>();
    public ICollection<UserRoom> UserRooms { get; set; } = new List<UserRoom>();

}
