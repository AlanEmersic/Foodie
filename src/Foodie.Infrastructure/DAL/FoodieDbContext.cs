using Foodie.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Foodie.Infrastructure.DAL;

internal sealed class FoodieDbContext : DbContext
{
    public DbSet<Product> Products { get; set; } = default!;

    public FoodieDbContext(DbContextOptions<FoodieDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoodieDbContext).Assembly);
    }
}