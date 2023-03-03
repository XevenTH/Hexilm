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
    public DbSet<UserRoom> UserRooms { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRoom>().HasKey(Ur => new { Ur.RoomId, Ur.UserAppId });

        modelBuilder.Entity<UserRoom>()
            .HasOne<Room>(r => r.Room)
            .WithMany(ur => ur.Attendees)
            .HasForeignKey(r => r.RoomId);

        modelBuilder.Entity<UserRoom>()
            .HasOne<UserApp>(ua => ua.User)
            .WithMany(ur => ur.UserRooms)
            .HasForeignKey(ua => ua.UserAppId);
    }
}
