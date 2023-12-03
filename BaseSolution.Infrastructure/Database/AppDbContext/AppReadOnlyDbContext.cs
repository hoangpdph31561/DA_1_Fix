using BaseSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Database.AppDbContext
{
    public class AppReadOnlyDbContext : DbContext
    {
        public AppReadOnlyDbContext()
        {

        }
        public AppReadOnlyDbContext(DbContextOptions<AppReadOnlyDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppReadOnlyDbContext).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DA1_HotelBooking;Integrated Security=True;TrustServerCertificate=true");
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
    }
}