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
    public class ServiceEntityConfiguration : IEntityTypeConfiguration<ServiceEntity>
    {
        public void Configure(EntityTypeBuilder<ServiceEntity> builder)
        {
            builder.ToTable("Service");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsUnicode(true).IsRequired();
            builder.Property(x => x.Description).IsUnicode(true).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Unit).IsRequired().IsUnicode(true);
            builder.Property(x => x.IsRoomBookingNeed).IsRequired();
            builder.HasOne(x => x.ServiceType).WithMany(x => x.Services).HasForeignKey(x => x.ServiceTypeId);
        }
    }
}
