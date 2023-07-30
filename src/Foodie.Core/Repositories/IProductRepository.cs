using Foodie.Core.Entities;

namespace Foodie.Core.Repositories;

public interface IProductRepository
{
    public Task<Product?> GetByIdAsync(int id);
    public Task AddAsync(Product product);
    public Task UpdateAsync(Product product);
    public Task DeleteAsync(Product product);
}