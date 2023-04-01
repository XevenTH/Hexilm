namespace Model;
public class Director
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Photo Photo { get; set; }
    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
