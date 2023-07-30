using Foodie.Application.Commands;
using Foodie.Application.DTO;
using Foodie.Core.Entities;

namespace Foodie.Application.Mappings;

public static class ProductMappingExtensions
{
    public static ProductDto AsDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl
        };
    }

    public static Product AsEntity(this CreateProductCommand command)
    {
        return new Product
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            ImageUrl = command.ImageUrl
        };
    }

    public static Product AsEntity(this UpdateProductCommand command)
    {
        return new Product
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            ImageUrl = command.ImageUrl
        };
    }
}
