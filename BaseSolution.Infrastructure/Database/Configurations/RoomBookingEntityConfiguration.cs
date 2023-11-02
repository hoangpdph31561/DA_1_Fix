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
    public class RoomBookingEntityConfiguration : IEntityTypeConfiguration<RoomBookingEntity>
    {
        public void Configure(EntityTypeBuilder<RoomBookingEntity> builder)
        {
            builder.ToTable("RoomBooking");
            builder.HasKey(x  => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CodeBooking).IsRequired().IsUnicode(false);
            builder.HasIndex(x => x.CodeBooking).IsUnique();
            builder.Property(x => x.BookingType).IsRequired();
            builder.HasOne(x => x.Customer).WithMany(x => x.RoomBookings).HasForeignKey(x => x.CustomerId);
            builder.HasOne(x => x.User).WithMany(x => x.RoomBookings).HasForeignKey(x => x.UserId).IsRequired(false);
        }
    }
}
