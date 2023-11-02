using BaseSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseSolution.Infrastructure.Database.Configurations
{
    public partial class ExampleEntityConfigurations : IEntityTypeConfiguration<ExampleEntity>
    {
        public void Configure(EntityTypeBuilder<ExampleEntity> builder)
        {
            builder.ToTable("Example");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
        }
    }
}