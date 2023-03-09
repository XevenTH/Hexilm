namespace Model;

public class Room
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Movie Movie { get; set; }
    public ICollection<UserRoom> Attendees { get; set; } = new List<UserRoom>();
}
