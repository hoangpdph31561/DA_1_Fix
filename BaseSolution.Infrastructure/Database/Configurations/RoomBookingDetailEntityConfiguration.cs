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
    public class RoomBookingDetailEntityConfiguration : IEntityTypeConfiguration<RoomBookingDetailEntity>
    {
        public void Configure(EntityTypeBuilder<RoomBookingDetailEntity> builder)
        {
            builder.ToTable("RoomBookingDetail");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.CheckInBooking).IsRequired();
            builder.Property(x => x.CheckOutBooking).IsRequired();
            builder.Property(x => x.CheckInReality).IsRequired();
            builder.Property(x => x.CheckOutReality).IsRequired();
            builder.Property(x => x.PrePaid).HasDefaultValue(0);
            builder.HasOne(x => x.RoomDetail).WithMany(x => x.RoomBookingDetails).HasForeignKey(x => x.RoomDetailId);
            builder.HasOne(x => x.RoomBooking).WithMany(x => x.RoomBookingDetails).HasForeignKey(x => x.RoomBookingId);
        }
    }
}
