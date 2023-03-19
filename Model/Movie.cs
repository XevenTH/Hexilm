namespace Model;

public class Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Picture { get; set; } = string.Empty;
    public ICollection<FavoriteMovies> UserFavorite { get; set; } = new List<FavoriteMovies>();
}
