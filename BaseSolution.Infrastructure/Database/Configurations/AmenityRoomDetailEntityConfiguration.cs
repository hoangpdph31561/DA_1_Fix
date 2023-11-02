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
    public class AmenityRoomDetailEntityConfiguration : IEntityTypeConfiguration<AmenityRoomDetailEntity>
    {
        public void Configure(EntityTypeBuilder<AmenityRoomDetailEntity> builder)
        {
            builder.ToTable("AmenityRoomDetail");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Amount).IsRequired();
            builder.HasOne(x => x.Amenity).WithMany(x => x.AmenityRoomDetails).HasForeignKey(x => x.AmenityId);
            builder.HasOne(x => x.RoomType).WithMany(x => x.AmenityRoomDetails).HasForeignKey(x => x.RoomTypeId);
        }
    }
}
