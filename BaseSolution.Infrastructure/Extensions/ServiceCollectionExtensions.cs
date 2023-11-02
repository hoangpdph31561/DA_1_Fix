using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.Database.AppDbContext;
using BaseSolution.Infrastructure.Implements.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BaseSolution.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<ExampleReadOnlyDbContext>(options =>
            {
                // Configure your DbContext options here
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            services.AddDbContextPool<ExampleReadWriteDbContext>(options =>
            {
                // Configure your DbContext options here
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            services.AddTransient<ILocalizationService, LocalizationService>();

            return services;
        }
    }
}