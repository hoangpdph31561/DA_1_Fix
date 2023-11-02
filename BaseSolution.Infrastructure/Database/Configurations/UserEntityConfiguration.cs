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
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.UserName).IsUnicode(false).IsRequired();
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.Property(x => x.Password).IsUnicode(false).IsRequired();
            builder.Property(x => x.Name).IsUnicode(true).IsRequired();
            builder.Property(x => x.Email).IsUnicode(false).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.PhoneNumber).IsUnicode(false).IsRequired().HasMaxLength(20);
            builder.HasOne(x => x.UserRole).WithMany(x => x.Users).HasForeignKey(x => x.UserRoleId);
        }
    }
}
