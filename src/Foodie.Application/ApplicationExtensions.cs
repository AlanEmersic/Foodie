using Foodie.Application.Abstractions.Command;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Foodie.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly applicationAssembly = typeof(ICommandHandler<>).Assembly;
        services.Scan(s => s
            .FromAssemblies(applicationAssembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}