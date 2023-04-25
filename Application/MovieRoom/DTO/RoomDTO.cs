using Application.Movies.DTO;

namespace Application.MovieRoom.DTO;

public class RoomDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public MiniMovieDTO Movie { get; set; }
    public ICollection<AttendeesDTO> Attendees { get; set; }
}
