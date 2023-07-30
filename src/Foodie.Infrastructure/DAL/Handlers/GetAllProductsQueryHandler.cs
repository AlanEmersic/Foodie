using Foodie.Application.Abstractions.Query;
using Foodie.Application.DTO;
using Foodie.Application.Mappings;
using Foodie.Application.Queries;
using Microsoft.EntityFrameworkCore;

namespace Foodie.Infrastructure.DAL.Handlers;

internal sealed class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly FoodieDbContext context;

    public GetAllProductsQueryHandler(FoodieDbContext context)
    {
        this.context = context;
    }

    public async Task<List<ProductDto>> HandleAsync(GetAllProductsQuery query)
    {
        List<ProductDto> products = await context.Products.AsNoTracking().Select(x => x.AsDto()).ToListAsync();

        return products;
    }
}
