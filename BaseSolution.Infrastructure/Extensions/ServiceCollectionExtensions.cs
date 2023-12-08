using BaseSolution.Application.Interfaces.Login;
using BaseSolution.Application.Interfaces.Repositories.ReadOnly;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Infrastructure.Database.AppDbContext;
using BaseSolution.Infrastructure.Implements.Login;
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
            services.AddTransient<IFloorReadOnlyRespository, FloorReadOnlyRespository>();
            services.AddTransient<IFloorReadWriteRespository, FloorReadWriteRespository>();
            services.AddTransient<IBillReadOnlyRespository, BillReadOnlyRespository>();
            services.AddTransient<IBillReadWriteRespository, BillReadWriteRespository>();
            services.AddTransient<IServiceOrderReadOnlyRespository, ServiceOrderReadOnlyRespository>();
            services.AddTransient<IServiceOrderReadWriteRespository, ServiceOrderReadWriteRespository>();
            services.AddTransient<IServiceOrderDetailReadOnlyRespository, ServiceOrderDetailReadOnlyRespository>();
            services.AddTransient<IServiceOrderDetailReadWriteRespository, ServiceOrderDetailReadWriteRespository>();
            services.AddTransient<ICustomerReadOnlyRepository, CustomerReadOnlyRepository>();
            services.AddTransient<ICustomerReadWriteRepository, CustomerReadWriteRepository>();
            services.AddTransient<IRoomDetailReadOnlyRepository, RoomDetailReadOnlyRepository>();
            services.AddTransient<IRoomDetailReadWriteRepository, RoomDetailReadWriteRepository>();
            services.AddTransient<IServiceReadOnlyRepository, ServiceReadOnlyRepository>();
            services.AddTransient<IServicesReadWriteRepository, ServiceReadWriteRepository>();
            services.AddTransient<IServiceTypeReadOnlyRepository, ServiceTypeReadOnlyRepository>();
            services.AddTransient<IServiceTypeReadWriteRepository, ServiceTypeReadWriteRepository>();
            services.AddTransient<IAmenityReadOnlyRepository, AmenityReadOnlyRepository>();
            services.AddTransient<IAmenityReadWriteRepository, AmenityReadWriteRepository>();
            services.AddTransient<IAmenityRoomDetailReadOnlyRepository, AmenityRoomDetailReadOnlyRepository>();
            services.AddTransient<IAmenityRoomDetailReadWriteRepository, AmenityRoomDetailReadWriteRepository>();
            services.AddTransient<IRoomTypeReadOnlyRepository, RoomTypeReadOnlyRepository>();
            services.AddTransient<IRoomTypeReadWriteRepository, RoomTypeReadWriteRepository>();
            services.AddTransient<IRoomBookingDetailReadOnlyRepository, RoomBookingDetailReadOnlyRepository>();
            services.AddTransient<IRoomBookingDetailReadWriteRepository, RoomBookingDetailReadWriteRepository>();
            services.AddTransient<IRoombookingReadOnlyRepository, RoombookingReadOnlyRepository>();
            services.AddTransient<IRoombookingReadWriteRepository, RoombookingReadWriteRepository>();
            services.AddTransient<IRoleReadOnlyRepository, RoleReadOnlyRepository>();
            services.AddTransient<IRoleReadWriteRepository, RoleReadWriteRepository>();
            services.AddTransient<IUserReadOnlyRepository, UserReadOnlyRepository>();
            services.AddTransient<IUserReadWriteRepository, UserReadWriteRepository>();
            services.AddTransient<IRoombookingStatisticReadOnlyRepository, RoombookingStatisticReadOnlyRepository>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IBillStatisticReadOnlyRespository, BillStatisticReadOnlyRespository>();
            services.AddTransient<IServiceOrderStatisticReadOnlyRespository, ServiceOrderStatisticReadOnlyRespository>();
            return services;
        }
    }
}