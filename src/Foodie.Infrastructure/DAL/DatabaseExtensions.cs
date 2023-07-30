using Foodie.Core.Repositories;
using Foodie.Infrastructure.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Foodie.Infrastructure.DAL;

internal static class DatabaseExtensions
{
    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        const string sectionName = "Database";
        string connectionString = configuration.GetConnectionString(sectionName)!;

        services.AddDbContext<FoodieDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddHostedService<DatabaseInitializer>();

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}