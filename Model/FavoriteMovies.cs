namespace Model;

public class FavoriteMovies
{
    public int Id { get; set; }
    public string UserAppId { get; set; }
    public UserApp User { get; set; }
    public Guid MovieId { get; set; }
    public Movie Movie { get; set; }
}