using Model;

namespace Application.Movies.DTO;

public class MovieDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    public ICollection<ActorDTO> Actors { get; set; } = new List<ActorDTO>();
    public DirectorDTO Director { get; set; }
}