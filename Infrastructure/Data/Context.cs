using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class RetailProcurementContext : DbContext
    {
        DbSet<StoreItem> StoreItems { get; set; }
        DbSet<Suplier> Supliers { get; set; }
        DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Suplier>()
                .HasMany(e => e.StoreItems)
                .WithMany(e => e.Supliers)
                .UsingEntity<SuplierStoreItem>(
                    l => l.HasOne<Suplier>(e => e.Suplier).WithMany(e => e.SuplierStoreItems).HasForeignKey(e => e.SuplierId));

            modelBuilder.Entity<Suplier>()
                .HasMany(e => e.StoreItems)
                .WithMany(e => e.Supliers)
                .UsingEntity<SuplierStoreItem>(
                    r => r.HasOne<StoreItem>(e => e.StoreItem).WithMany(e => e.SuplierStoreItems).HasForeignKey(e => e.StoreItemId));

            modelBuilder.Entity<SuplierStoreItem>().Navigation(e => e.Suplier).AutoInclude();
            modelBuilder.Entity<SuplierStoreItem>().Navigation(e => e.StoreItem).AutoInclude();

            modelBuilder.Entity<Suplier>().Property(e => e.Address).IsRequired();
            modelBuilder.Entity<Suplier>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Suplier>().Property(e => e.Country).IsRequired();

            modelBuilder.Entity<StoreItem>().Property(e => e.ItemName).IsRequired();
            modelBuilder.Entity<StoreItem>().Property(e => e.ItemDescription).IsRequired();

            modelBuilder.Entity<SuplierStoreItem>().Property(e => e.SuplierPrice).IsRequired();

            modelBuilder.Entity<User>().Property(e => e.FirstName).IsRequired();
            modelBuilder.Entity<User>().Property(e => e.LastName).IsRequired();
            modelBuilder.Entity<User>().Property(e => e.UserName).IsRequired();
            modelBuilder.Entity<User>().Property(e => e.Password).IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=RetailProcurement;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
