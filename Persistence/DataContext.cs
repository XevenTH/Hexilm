using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Model;

namespace Persistence;

public class DataContext : IdentityDbContext<UserApp>
{
    public DataContext(DbContextOptions options) : base(options) { }

    public DbSet<Movie> Movies { get; set; }

    public DbSet<UserApp> User { get; set; }
}
