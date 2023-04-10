namespace Application.MovieRoom.DTO;

public class AttendeesDTO
{
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string Photo { get; set; } = string.Empty;
    public bool IsHost { get; set; }
}
