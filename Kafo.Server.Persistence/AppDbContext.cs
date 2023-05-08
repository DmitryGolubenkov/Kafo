using Microsoft.EntityFrameworkCore;

namespace Kafo.Server.Persistence;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

