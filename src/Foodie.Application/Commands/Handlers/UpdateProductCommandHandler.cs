using Foodie.Application.Abstractions.Command;
using Foodie.Application.Mappings;
using Foodie.Core.Entities;
using Foodie.Core.Exceptions;
using Foodie.Core.Repositories;

namespace Foodie.Application.Commands.Handlers;

internal sealed class UpdateProductCommandHandler: ICommandHandler<UpdateProductCommand>
{
    private readonly IProductRepository productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task HandleAsync(UpdateProductCommand command)
    {
        Product? product = await productRepository.GetByIdAsync(command.Id);

        if (product is null)
        {
            throw new EntityNotFoundException<Product>(command.Id);
        }

        await productRepository.UpdateAsync(command.AsEntity());
    }
}
