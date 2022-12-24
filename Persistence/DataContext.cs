using Microsoft.EntityFrameworkCore;
using Model;

namespace Persistence;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) { }

    public DbSet<Movie> Movies { get; set; }
}
