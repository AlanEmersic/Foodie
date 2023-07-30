using Foodie.Application.Abstractions.Query;
using Foodie.Application.DTO;
using Foodie.Application.Mappings;
using Foodie.Application.Queries;
using Foodie.Core.Entities;
using Foodie.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Foodie.Infrastructure.DAL.Handlers;

internal sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductDto>
{
    private readonly FoodieDbContext context;

    public GetProductQueryHandler(FoodieDbContext context)
    {
        this.context = context;
    }

    public async Task<ProductDto> HandleAsync(GetProductQuery query)
    {
        Product? product = await context.Products.FirstOrDefaultAsync(p => p.Id == query.Id);

        if (product is null)
        {
            throw new EntityNotFoundException<Product>(query.Id);
        }

        return product.AsDto();
    }
}
