using Factoring.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Factoring.Infrastructure
{
    public class FactoringDbContext(DbContextOptions<FactoringDbContext> options) : DbContext(options)
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<FactoringContract> FactoringContracts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FactoringContract>()
                .Property(c => c.Status)
                .HasConversion<string>();

            modelBuilder.Entity<FactoringContract>()
                .Property(c => c.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.Amount)
                .HasPrecision(18, 2);
        }
    }
}
