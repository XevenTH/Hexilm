using Application.Movies.DTO;
using Model;

namespace Application.Profile.DTO;

public class ProfileDTO
{
    public string DisplayName { get; set; } = string.Empty;
    public string UserName { get; set; }
    public string Bio { get; set; } = string.Empty;
    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    public ICollection<MovieDTO> FavoriteMovies { get; set; }
}
