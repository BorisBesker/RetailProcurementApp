using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Infrastructure.Data
{
    public class RetailProcurementContext : DbContext
    {
        DbSet<StoreItem> StoreItems { get; set; }
        DbSet<Suplier> Supliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Suplier>()
                .HasMany(e => e.StoreItems)
                .WithMany(e => e.Supliers)
                .UsingEntity<SuplierItem>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=RetailProcurement;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
