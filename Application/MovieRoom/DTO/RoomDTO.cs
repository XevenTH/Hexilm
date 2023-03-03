using Model;

namespace Application.MovieRoom.DTO;

public class RoomDTO
{
    public Guid Id { get; set; }
    public Movie Movie { get; set; }
    public ICollection<AttendeesDTO> Attendees { get; set; }
}
