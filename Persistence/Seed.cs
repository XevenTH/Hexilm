using Microsoft.AspNetCore.Identity;
using Model;

namespace Persistence;

public class Seed
{
    public static async Task Seeding(DataContext context, UserManager<UserApp> manager)
    {
        if (!context.User.Any() || !context.Movies.Any() || !context.Room.Any() || !context.Directors.Any() || !context.Actors.Any())
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
                await manager.CreateAsync(user, "Passw0rd");
            }

            List<Director> Directors = new List<Director>
            {
                new Director{
                    Name = "Anthony Russo",
                },
                new Director{
                    Name = "James Gunn",
                },
                new Director{
                    Name = "Michael Chaves",
                },
                new Director{
                    Name = "Makoto Shinkai",
                },
                new Director{
                    Name = "Jon Watts",
                },
            };

            List<Actor> Actors = new List<Actor>
            {
                // Captain America: Civil War 
                new Actor{
                    Name = "Chris Evans"
                },
                new Actor{
                    Name = "Robert Downey Jr."
                },
                // The Suicide Squad (2021)
                new Actor{
                    Name = "Margot Robbie"
                },
                new Actor{
                    Name = "Jared Leto"
                },
                // The Conjuring: The Devil Made Me Do It
                new Actor{
                    Name = "Vera Farmiga"
                },
                new Actor{
                    Name = "Patrick Wilson"
                },
                // Suzume no Tojimari
                new Actor{
                    Name = "Nanoka Hara"
                },
                new Actor{
                    Name = "Hokuto Matsumura"
                },
                // Kimi No Nawa
                new Actor{
                    Name = "Ryunosuke Kamiki"
                },
                new Actor{
                    Name = "Mone Kamishiraishi"
                },
                // Spider-Man: Far from Home
                new Actor{
                    Name = "Tom Holland"
                },
                new Actor{
                    Name = "Zendaya"
                },
            };

            if (context.Movies.Any()) return;

            List<Movie> movies = new List<Movie>
            {
                new Movie {
                    Title = "Captain America: Civil War",
                    Description = "This is Captain America: Civil War Movie Description",
                    Director = Directors[0],
                    Actors = new List<Actor>{Actors[0],Actors[1]}
                },
                new Movie {
                    Title = "The Suicide Squad (2021)",
                    Description = "This is The Suicide Squad (2021) Movie Description",
                    Director = Directors[1],
                    Actors = new List<Actor>{Actors[2],Actors[3]}
                },
                new Movie {
                    Title = "The Conjuring: The Devil Made Me Do It",
                    Description = "This is The Conjuring: The Devil Made Me Do It Movie Description",
                    Director = Directors[2],
                    Actors = new List<Actor>{Actors[4],Actors[5]}
                },
                new Movie {
                    Title = "Suzume no Tojimari",
                    Description = "This is Suzume no Tojimari Movie Description",
                    Director = Directors[3],
                    Actors = new List<Actor>{Actors[6],Actors[7]}
                },
                new Movie {
                    Title = "Kimi No Nawa",
                    Description = "This is Kimi No Nawa Movie Description",
                    Director = Directors[3],
                    Actors = new List<Actor>{Actors[8],Actors[9]}
                },
                new Movie {
                    Title = "Spider-Man: Far from Home",
                    Description = "This is Spider-Man: Far from Home Movie Description",
                    Director = Directors[4],
                    Actors = new List<Actor>{Actors[10],Actors[11]}
                }
            };

            if (context.Room.Any()) return;

            List<Room> rooms = new List<Room>
            {
                new Room {
                    Title = "Yook Penggemar Marvel",
                    Movie = movies[0],
                    Attendees = new List<UserRoom>
                    {
                        new UserRoom {
                            User = users[0],
                            IsHost = true
                        },
                    }
                },
                new Room {
                    Title = "New Movie!! Suzume No Tojimari",
                    Movie = movies[3],
                    Attendees = new List<UserRoom>
                    {
                        new UserRoom {
                            User = users[0],
                            IsHost = true
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
                            User = users[1],
                            IsHost = true
                        },
                    }
                },
                new Room {
                    Title = "Mari Kita Kaget Bareng",
                    Movie = movies[2],
                    Attendees = new List<UserRoom>
                    {
                        new UserRoom {
                            User = users[2],
                            IsHost = true
                        },
                        new UserRoom {
                            User = users[1]
                        }
                    }
                }
            };

            await context.Directors.AddRangeAsync(Directors);
            await context.Actors.AddRangeAsync(Actors);
            await context.Movies.AddRangeAsync(movies);
            await context.Room.AddRangeAsync(rooms);
            await context.SaveChangesAsync();
        };

    }
}
