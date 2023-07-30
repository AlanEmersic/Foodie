using Foodie.Application.Abstractions.Query;
using Foodie.Application.DTO;

namespace Foodie.Application.Queries;

public sealed record GetAllProductsQuery() : IQuery<List<ProductDto>>;
