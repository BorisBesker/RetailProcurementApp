using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

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

            modelBuilder.Entity<Suplier>().Property(e => e.Address).IsRequired();
            modelBuilder.Entity<Suplier>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Suplier>().Property(e => e.Country).IsRequired();

            modelBuilder.Entity<StoreItem>().Property(e => e.Price).IsRequired();
            modelBuilder.Entity<Suplier>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Suplier>().Property(e => e.Address).IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=RetailProcurement;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
