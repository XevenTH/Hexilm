namespace Model;
public class MovieCategory
{
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public Guid MovieId { get; set; }
    public Movie Movie { get; set; }
}
