using System.Linq.Expressions;

using ATech.Repository.Test.Entities;

using Microsoft.EntityFrameworkCore;

namespace ATech.Repository.Test.EntityFrameworkCore;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Measure> Measures { get; set; }
    public DbSet<PhysicalDimension> PhysicalDimensions { get; set; }
    public DbSet<Sensor> Sensors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseInMemoryDatabase("MyInMemoryDatabase");
        }
    }
}
