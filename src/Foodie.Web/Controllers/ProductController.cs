using Foodie.Application.Abstractions.Command;
using Foodie.Application.Abstractions.Query;
using Foodie.Application.Commands;
using Foodie.Application.DTO;
using Foodie.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Foodie.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProductController : ControllerBase
{
    private readonly IQueryHandler<GetAllProductsQuery, List<ProductDto>> getAllProductsQueryHandler;
    private readonly IQueryHandler<GetProductQuery, ProductDto> getProductQueryHandler;
    private readonly ICommandHandler<CreateProductCommand> createProductCommandHandler;
    private readonly ICommandHandler<UpdateProductCommand> updateProductCommandHandler;
    private readonly ICommandHandler<DeleteProductCommand> deleteProductCommandHandler;

    public ProductController(
        IQueryHandler<GetAllProductsQuery, List<ProductDto>> getAllProductsQueryHandler,
        IQueryHandler<GetProductQuery, ProductDto> getProductQueryHandler,
        ICommandHandler<CreateProductCommand> createProductCommandHandler,
        ICommandHandler<UpdateProductCommand> updateProductCommandHandler,
        ICommandHandler<DeleteProductCommand> deleteProductCommandHandler)
    {
        this.getAllProductsQueryHandler = getAllProductsQueryHandler;
        this.getProductQueryHandler = getProductQueryHandler;
        this.createProductCommandHandler = createProductCommandHandler;
        this.updateProductCommandHandler = updateProductCommandHandler;
        this.deleteProductCommandHandler = deleteProductCommandHandler;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ProductDto>>> GetProducts([FromQuery] GetAllProductsQuery query)
    {
        List<ProductDto> response = await getAllProductsQueryHandler.HandleAsync(query);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        ProductDto response = await getProductQueryHandler.HandleAsync(new GetProductQuery(id));

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateProduct(CreateProductCommand command)
    {
        await createProductCommandHandler.HandleAsync(command);

        return CreatedAtAction(nameof(CreateProduct), default);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateProduct(UpdateProductCommand command)
    {
        await updateProductCommandHandler.HandleAsync(command);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        await deleteProductCommandHandler.HandleAsync(new DeleteProductCommand(id));

        return NoContent();
    }
}