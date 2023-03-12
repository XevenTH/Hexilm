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
                    Displayname = "Joko",
                    UserName = "joko",
                    Email = "joko@gmail.com"
                },

                new UserApp {
                    Displayname = "Wati",
                    UserName = "wati",
                    Email = "wati@gmail.com"
                },

                new UserApp {
                    Displayname = "Budi",
                    UserName = "budi",
                    Email = "budi@gmail.com"
                },
            };

            foreach (UserApp user in users)
            {
                await manager.CreateAsync(user, "Passw0rd");
            }

            if(context.Movies.Any()) return;

            List<Movie> movies = new List<Movie>
            {
                new Movie {
                    Title = "Marvel",
                    Description = "This is Marvel Movie Description",
                    Picture = ""
                },
                new Movie {
                    Title = "DC",
                    Description = "This is DC Movie Description",
                    Picture = ""
                },
                new Movie {
                    Title = "The Conjuring",
                    Description = "This is The Conjuring Movie Description",
                    Picture = ""
                },
                new Movie {
                    Title = "Suzume no Tojimari",
                    Description = "This is Suzume no Tojimari Movie Description",
                    Picture = ""
                },
                new Movie {
                    Title = "Kimi No Nawa",
                    Description = "This is Kimi No Nawa Movie Description",
                    Picture = ""
                },
                new Movie {
                    Title = "Spider-Man: Homecoming",
                    Description = "This is Spider-Man: Homecoming Movie Description",
                    Picture = ""
                }
            };

            if(context.Room.Any()) return;

            List<Room> rooms = new List<Room>
            {
                new Room {
                    Title = "Yook Penggemar Marvel",
                    Movie = movies[0],
                    Attendees = new List<UserRoom>
                    {
                        new UserRoom {
                            User = users[0]
                        },
                    }
                },
                new Room {
                    Title = "New Movie!! Suzume No Tojimari",
                    Movie = movies[3],
                    Attendees = new List<UserRoom>
                    {
                        new UserRoom {
                            User = users[0]
                        },
                        new UserRoom {
                            User = users[1]
                        },
                        new UserRoom {
                            User = users[2]
                        },
                    }
                },
                new Room {
                    Title = "On Stream DC Movie!!!",
                    Movie = movies[1],
                    Attendees = new List<UserRoom>
                    {
                        new UserRoom {
                            User = users[1]
                        },
                    }
                },
                new Room {
                    Title = "Mari Kita Kaget Bareng",
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
