using Microsoft.AspNetCore.Identity;
using Model;

namespace Persistence;

public class Seed
{
    public static async Task Seeding(DataContext context, UserManager<UserApp> manager)
    {
        if (!context.User.Any() || !context.Movies.Any())
        {
            List<UserApp> users = new List<UserApp>
            {
                new UserApp {
                    DisplayName = "Joko",
                    UserName = "joko",
                    Email = "joko@gmail.com"
                },

                new UserApp {
                    DisplayName = "Wati",
                    UserName = "wati",
                    Email = "wati@gmail.com"
                },

                new UserApp {
                    DisplayName = "Budi",
                    UserName = "budi",
                    Email = "budi@gmail.com"
                },
            };

            foreach (UserApp user in users)
            {
                await manager.CreateAsync(user, "Pa$$w0rd");
            }

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
        };

    }
}
