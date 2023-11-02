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
    public class AmenityEntityConfiguration : IEntityTypeConfiguration<AmenityEntity>
    {
        public void Configure(EntityTypeBuilder<AmenityEntity> builder)
        {
            builder.ToTable("Amenity");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsUnicode(true).IsRequired();
            builder.Property(x => x.Description).IsUnicode(true).IsRequired();
        }
    }
}
