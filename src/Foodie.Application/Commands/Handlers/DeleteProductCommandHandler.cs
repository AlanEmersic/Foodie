using Foodie.Application.Abstractions.Command;
using Foodie.Core.Entities;
using Foodie.Core.Exceptions;
using Foodie.Core.Repositories;

namespace Foodie.Application.Commands.Handlers;

internal sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
{
    private readonly IProductRepository productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task HandleAsync(DeleteProductCommand command)
    {
        Product? product = await productRepository.GetByIdAsync(command.Id);

        if (product is null)
        {
            throw new EntityNotFoundException<Product>(command.Id);
        }

        await productRepository.DeleteAsync(product);
    }
}
