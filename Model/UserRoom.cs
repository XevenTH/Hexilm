namespace Model;

public class UserRoom
{
    public string UserAppId { get; set; }
    public UserApp User { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public bool IsHost { get; set; }
}
