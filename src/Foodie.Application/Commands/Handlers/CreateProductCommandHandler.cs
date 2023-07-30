using Foodie.Application.Abstractions.Command;
using Foodie.Application.Mappings;
using Foodie.Core.Entities;
using Foodie.Core.Repositories;

namespace Foodie.Application.Commands.Handlers;

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
{
    private readonly IProductRepository productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task HandleAsync(CreateProductCommand command)
    {
        Product product = command.AsEntity();

        await productRepository.AddAsync(product);
    }
}
