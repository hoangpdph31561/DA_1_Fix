using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.Database.AppDbContext;
using BaseSolution.Infrastructure.Implements.Repositories.ReadOnly;
using BaseSolution.Infrastructure.Implements.Repositories.ReadWrite;
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
            #region App DBContext
            services.AddDbContextPool<AppReadOnlyDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            services.AddDbContextPool<AppReadWriteDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });
            #endregion
            #region Example DbContext
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
            #endregion

            services.AddTransient<ILocalizationService, LocalizationService>();
            services.AddTransient<IBuildingReadOnlyRespository, BuildingReadOnlyRespository>();
            services.AddTransient<IBuildingReadWriteRespository, BuildingReadWriteRespository>();
            services.AddTransient<IUserReadOnlyRepository, UserReadOnlyRepository>();
            services.AddTransient<IUserReadWriteRepository, UserReadWriteRepository>();
            services.AddTransient<IRoombookingReadOnlyRepository, RoombookingReadOnlyRepository>();
            services.AddTransient<IRoombookingReadWriteRepository, RoombookingReadWriteRepository>();
            services.AddTransient<IRoomBookingDetailReadOnlyRepository, RoomBookingDetailReadOnlyRepository>();
            services.AddTransient<IRoomBookingDetailReadWriteRepository, RoomBookingDetailReadWriteRepository>();
            services.AddTransient<IRoleReadOnlyRepository, RoleReadOnlyRepository>();
            services.AddTransient<IRoleReadWriteRepository, RoleReadWriteRepository>();
            services.AddTransient<IAmenityReadOnlyRepository, AmenityReadOnlyRepository>();
            services.AddTransient<IAmenityReadWriteRepository, AmenityReadWriteRepository>();
            services.AddTransient<IAmenityRoomDetailReadOnlyRepository, AmenityRoomDetailReadOnlyRepository>();
            services.AddTransient<IAmenityRoomDetailReadWriteRepository, AmenityRoomDetailReadWriteRepository>();
            services.AddTransient<IRoomTypeReadOnlyRepository, RoomTypeReadOnlyRepository>();
            services.AddTransient<IRoomTypeReadWriteRepository, RoomTypeReadWriteRepository>();
            return services;
        }
    }
}