using Foodie.Application.Abstractions.Command;

namespace Foodie.Application.Commands;

public sealed record CreateProductCommand(string Name, string Description, decimal Price, string ImageUrl) : ICommand;