using Foodie.Core.Entities;
using Foodie.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Foodie.Infrastructure.DAL.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly FoodieDbContext dbContext;

    public ProductRepository(FoodieDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        await dbContext.Products.AddAsync(product);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
    }
}