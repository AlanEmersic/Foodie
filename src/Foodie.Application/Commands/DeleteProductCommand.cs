using Foodie.Application.Abstractions.Command;

namespace Foodie.Application.Commands;

public sealed record DeleteProductCommand(int Id) : ICommand;