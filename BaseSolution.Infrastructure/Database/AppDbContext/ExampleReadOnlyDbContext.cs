using BaseSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseSolution.Infrastructure.Database.AppDbContext
{
    public class ExampleReadOnlyDbContext : DbContext
    {
        public ExampleReadOnlyDbContext()
        {
        }

        public ExampleReadOnlyDbContext(DbContextOptions<ExampleReadOnlyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExampleReadOnlyDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=<SERVER>;Database=<DATABASE>;User Id=<USER>;Password=<PASSWORD>;Trust Server Certificate=true;");
            }
        }

        #region DbSet
        public DbSet<ExampleEntity> Users { get; set; }
        #endregion
    }
}
