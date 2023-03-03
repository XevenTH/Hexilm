using Microsoft.AspNetCore.Identity;
using Model;

namespace Persistence;

public class Seed
{
    public static async Task Seeding(DataContext context, UserManager<UserApp> manager)
    {
        if (!context.User.Any() || !context.Movies.Any() || !context.Room.Any())
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

            List<Movie> movies = new List<Movie>
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

            if(context.Room.Any()) return;

            List<Room> rooms = new List<Room>
            {
                new Room {
                    Movie = movies[0],
                    Attendees = new List<UserRoom>
                    {
                        new UserRoom {
                            User = users[0]
                        },
                    }
                },
                new Room {
                    Movie = movies[1],
                    Attendees = new List<UserRoom>
                    {
                        new UserRoom {
                            User = users[1]
                        },
                    }
                },
                new Room {
                    Movie = movies[2],
                    Attendees = new List<UserRoom>
                    {
                        new UserRoom {
                            User = users[2]
                        },
                        new UserRoom {
                            User = users[1]
                        }
                    }
                }
            };

            await context.Movies.AddRangeAsync(movies);
            await context.Room.AddRangeAsync(rooms);
            await context.SaveChangesAsync();
        };

    }
}
