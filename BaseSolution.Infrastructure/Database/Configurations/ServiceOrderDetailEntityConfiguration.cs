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
    public class ServiceOrderDetailEntityConfiguration : IEntityTypeConfiguration<ServiceOrderDetailEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceOrderDetailEntity> builder)
        {
            builder.ToTable("ServiceOrderDetail");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.HasOne(x => x.Service).WithMany(x => x.ServiceOrderDetails).HasForeignKey(x => x.ServiceId);
            builder.HasOne(x => x.ServiceOrder).WithMany(x => x.ServiceOrderDetails).HasForeignKey(x => x.ServiceOrderId);
        }
    }
}
