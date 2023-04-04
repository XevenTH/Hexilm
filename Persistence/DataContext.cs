using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Model;

namespace Persistence;

public class DataContext : IdentityDbContext<UserApp>
{
    public DataContext(DbContextOptions options) : base(options) { }

    public DbSet<Movie> Movies { get; set; }

    public DbSet<UserApp> User { get; set; }
    public DbSet<Room> Room { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<UserRoom> UserRooms_Join { get; set; }
    public DbSet<FavoriteMovies> FavoriteMovies_Join { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<MovieCategory> MovieCategories_join { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRoom>().HasKey(ur => new { ur.RoomId, ur.UserAppId });

        modelBuilder.Entity<UserRoom>()
            .HasOne(r => r.Room)
            .WithMany(ur => ur.Attendees)
            .HasForeignKey(r => r.RoomId);

        modelBuilder.Entity<UserRoom>()
            .HasOne(ua => ua.User)
            .WithMany(ur => ur.UserRooms)
            .HasForeignKey(ua => ua.UserAppId);

        modelBuilder.Entity<FavoriteMovies>().HasKey(fm => new { fm.UserAppId, fm.MovieId });

        modelBuilder.Entity<FavoriteMovies>()
            .HasOne(u => u.User)
            .WithMany(m => m.FavoriteMovies)
            .HasForeignKey(u => u.UserAppId);

        modelBuilder.Entity<FavoriteMovies>()
            .HasOne(m => m.Movie)
            .WithMany(u => u.UserFavorite)
            .HasForeignKey(m => m.MovieId);

        modelBuilder.Entity<MovieCategory>().HasKey(mc => new { mc.MovieId, mc.CategoryId });

        modelBuilder.Entity<MovieCategory>()
            .HasOne(m => m.Movie)
            .WithMany(c => c.MovieCategory)
            .HasForeignKey(m => m.MovieId);

        modelBuilder.Entity<MovieCategory>()
            .HasOne(m => m.Category)
            .WithMany(c => c.MovieCategory)
            .HasForeignKey(m => m.CategoryId);
    }
}
