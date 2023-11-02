using BaseSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Database.Configurations
{
    public class RoomDetailConfiguration : IEntityTypeConfiguration<RoomDetailEntity>
    {
        public void Configure(EntityTypeBuilder<RoomDetailEntity> builder)
        {
            builder.ToTable("RoomDetail");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsUnicode(false).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.MaxPeopleStay).IsRequired();
            builder.Property(x => x.Description).IsUnicode(true).IsRequired();
            builder.Property(x => x.RoomSize).IsRequired();
            builder.Property(x => x.Images).HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v));
            builder.HasOne(x => x.Floor).WithMany(x => x.RoomDetails).HasForeignKey(x => x.FloorId);
            builder.HasOne(x => x.RoomType).WithMany(x => x.RoomDetails).HasForeignKey(x => x.RoomTypeId);
        }
    }
}
