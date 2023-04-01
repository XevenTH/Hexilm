namespace Model;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<MovieCategory> MovieCategory { get; set; } = new List<MovieCategory>();
}
