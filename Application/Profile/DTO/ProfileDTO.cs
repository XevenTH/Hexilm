using Model;

namespace Application.Profile.DTO;

public class ProfileDTO
{
    public string Id { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string UserName { get; set; }
    public string Bio { get; set; } = string.Empty;
    public ICollection<FavoriteMovies> FavoriteMovies { get; set; }
}
