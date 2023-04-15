using Model;

namespace Application.Movies.DTO;

public class DirectorDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Photo Photo { get; set; }
}