using Model;

namespace Application.Movies.DTO;

public class MovieDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Picture { get; set; } = string.Empty;
    public ICollection<Actor> Actors { get; set; } = new List<Actor>();
    public DirectorDTO Director { get; set; }
}