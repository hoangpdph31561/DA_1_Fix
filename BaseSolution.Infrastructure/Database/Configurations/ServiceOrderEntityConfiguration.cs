using BaseSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Database.Configurations
{
    public class ServiceOrderEntityConfiguration : IEntityTypeConfiguration<ServiceOrderEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceOrderEntity> builder)
        {
            builder.ToTable("ServiceOrder");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Customer).WithMany(x => x.ServiceOrders).HasForeignKey(x => x.CustomerId);
            builder.HasOne(x => x.RoomBookingDetail).WithMany(x => x.ServiceOrders).HasForeignKey(x => x.RoomBookingDetailId).IsRequired(false);
        }
    }
}
