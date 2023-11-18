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
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsUnicode(true).IsRequired();
            builder.Property(x => x.IdentificationNumber).IsUnicode(false).IsRequired();
            builder.Property(x => x.Email).IsUnicode(false).IsRequired();
            builder.Property(x => x.PhoneNumber).IsUnicode(false).IsRequired().HasMaxLength(20);
            builder.Property(x => x.CustomerType).IsRequired();
            builder.Property(x => x.ApprovedCode).IsRequired(false).IsUnicode(false);
            builder.Property(x => x.ApprovedCodeExpiredTime).IsRequired(false);
        }
    }
}
