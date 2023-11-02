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
    public class FloorEntityConfiguration : IEntityTypeConfiguration<FloorEntity>
    {
        public void Configure(EntityTypeBuilder<FloorEntity> builder)
        {
            builder.ToTable("Floor");
            builder.HasKey(x  => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsUnicode(true).IsRequired();
            builder.Property(x => x.NumberOfRoom).IsRequired();
            builder.HasOne(x => x.Building).WithMany(x => x.Floors).HasForeignKey(x => x.BuildingId);
        }
    }
}
