using Microsoft.EntityFrameworkCore;
using sdlt.Entities.Models;
using sdlt.Repository.Configuration;

namespace sdlt.Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options)
    : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }

    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Product { get; set; }
}

