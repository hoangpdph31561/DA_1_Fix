using BaseSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Database.AppDbContext
{
    public class AppReadWriteDbContext : DbContext
    {
        public AppReadWriteDbContext()
        {

        }
        public AppReadWriteDbContext(DbContextOptions<AppReadWriteDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppReadWriteDbContext).Assembly);
            Seed(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=66.42.55.38;Initial Catalog=HAH;User ID=test;Password=E=lPJeY>-g/9QxzE;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        #region DBSet
        public DbSet<AmenityEntity> Amenities { get; set; }
        public DbSet<AmenityRoomDetailEntity> AmenityRoomDetails { get; set; }
        public DbSet<BillEntity> Bills { get; set; }
        public DbSet<BuildingEntity> Buildings { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<FloorEntity> Floors { get; set; }
        public DbSet<RoomBookingDetailEntity> RoomBookingDetails { get; set; }
        public DbSet<RoomBookingEntity> RoomBookings { get; set; }
        public DbSet<RoomDetailEntity> RoomDetails { get; set; }
        public DbSet<RoomTypeEntity> RoomTypes { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<ServiceOrderDetailEntity> ServiceOrderDetails { get; set; }
        public DbSet<ServiceOrderEntity> ServiceOrders { get; set; }
        public DbSet<ServiceTypeEntity> ServiceTypes { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        #endregion

        #region Data seeding   
        private void Seed(ModelBuilder modelBuilder)
        {
            Guid idBuilding = Guid.NewGuid();
            var building = new BuildingEntity()
            {
                Id = idBuilding,
                Name = "Tòa A",
                CreatedTime = DateTimeOffset.UtcNow,

            };
            modelBuilder.Entity<BuildingEntity>().HasData(building);
            var floor = new FloorEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Tầng 1",
                BuildingId = idBuilding,
                CreatedTime = DateTimeOffset.UtcNow,
                NumberOfRoom = 10
            };
            modelBuilder.Entity<FloorEntity>().HasData(floor);
            var roomType = new RoomTypeEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Phòng đơn",
                CreatedTime = DateTimeOffset.UtcNow,
                Description = "Phòng đơn",
            };
            modelBuilder.Entity<RoomTypeEntity>().HasData(roomType);
            var amenity = new AmenityEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Điều hòa",
                CreatedTime = DateTimeOffset.UtcNow,
                Description = "Điều hòa",
            };
            modelBuilder.Entity<AmenityEntity>().HasData(amenity);
            Guid idServiceType = Guid.NewGuid();
            var serviceType = new ServiceTypeEntity()
            {
                Id = idServiceType,
                Name = "Ăn uống",
                CreatedTime = DateTimeOffset.UtcNow,

            };
            modelBuilder.Entity<ServiceTypeEntity>().HasData(serviceType);
            var service = new ServiceEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Bữa sáng",
                CreatedTime = DateTimeOffset.UtcNow,
                ServiceTypeId = idServiceType,
                Price = 100000,
                Description = "Bữa sáng",
                IsRoomBookingNeed = true,
                Unit = "Suất"
            };
            modelBuilder.Entity<ServiceEntity>().HasData(service);
            var idRoleAdmin = Guid.NewGuid();
            var userRoles = new List<UserRoleEntity>()
            {
                new UserRoleEntity()
                {
                    Id = idRoleAdmin,
                    Name = "Admin",
                    CreatedTime = DateTimeOffset.UtcNow,
                    RoleCode = "Admin"
                },
                new UserRoleEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = "Nhân viên",
                    RoleCode = "Nhân viên",
                    CreatedTime = DateTimeOffset.UtcNow,
                }
            };
            modelBuilder.Entity<UserRoleEntity>().HasData(userRoles);
            var user = new UserEntity()
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                Password = "admin",
                CreatedTime = DateTimeOffset.UtcNow,
                UserRoleId = idRoleAdmin,
                Name = "Admin",
                Email = "admin@gmail.com",
                PhoneNumber = "0123456789",
            };
            modelBuilder.Entity<UserEntity>().HasData(user);

        }
        #endregion
    }
}
