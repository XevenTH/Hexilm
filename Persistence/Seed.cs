using Model;

namespace Persistence;

public class Seed
{
    public static async Task Seeding(DataContext context)
    {
        if(context.Movies.Any()) return;

        List<Movie> entity = new List<Movie> 
        {
            new Movie {
                Title = "Marvel",
                picture = null
            },
            new Movie {
                Title = "DC",
                picture = null
            },
            new Movie {
                Title = "The Conjuring",
                picture = null
            }
        };

        await context.Movies.AddRangeAsync(entity);
        await context.SaveChangesAsync();
    }
}
