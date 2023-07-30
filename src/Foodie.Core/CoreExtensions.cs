using Microsoft.Extensions.DependencyInjection;

namespace Foodie.Core;

public static class CoreExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services;
    }
}