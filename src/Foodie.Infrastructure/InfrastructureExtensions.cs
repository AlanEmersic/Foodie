using Foodie.Application.Abstractions.Query;
using Foodie.Infrastructure.DAL;
using Foodie.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Foodie.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        services.AddSingleton<ExceptionMiddleware>();

        services.AddHandlers();

        services.AddSwaggerGen();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    private static void AddHandlers(this IServiceCollection services)
    {
        Assembly infrastructureAssembly = typeof(InfrastructureExtensions).Assembly;

        services.Scan(s => s
                   .FromAssemblies(infrastructureAssembly)
                   .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                   .AsImplementedInterfaces()
                   .WithScopedLifetime());
    }
}