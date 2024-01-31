using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class RetailProcurementContext : DbContext
    {
        public RetailProcurementContext(DbContextOptions options) : base(options)
        {

        }

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


            // Insert dummy data on inital migration

            modelBuilder.Entity<StoreItem>().HasData(new StoreItem { Id = 1, ItemName = "item1", ItemDescription = "desc1"});
            modelBuilder.Entity<StoreItem>().HasData(new StoreItem { Id = 2, ItemName = "item2", ItemDescription = "desc2" });
            modelBuilder.Entity<StoreItem>().HasData(new StoreItem { Id = 3, ItemName = "item3", ItemDescription = "desc3" });
            modelBuilder.Entity<StoreItem>().HasData(new StoreItem { Id = 4, ItemName = "item4", ItemDescription = "desc4" });
            modelBuilder.Entity<StoreItem>().HasData(new StoreItem { Id = 5, ItemName = "item5", ItemDescription = "desc5" });
            modelBuilder.Entity<StoreItem>().HasData(new StoreItem { Id = 6, ItemName = "item6", ItemDescription = "desc6" });
            modelBuilder.Entity<StoreItem>().HasData(new StoreItem { Id = 7, ItemName = "item7", ItemDescription = "desc7" });
            modelBuilder.Entity<StoreItem>().HasData(new StoreItem { Id = 8, ItemName = "item8", ItemDescription = "desc8" });
            modelBuilder.Entity<StoreItem>().HasData(new StoreItem { Id = 9, ItemName = "item9", ItemDescription = "desc9" });
            modelBuilder.Entity<StoreItem>().HasData(new StoreItem { Id = 10, ItemName = "item10", ItemDescription = "desc10" });

            modelBuilder.Entity<Suplier>().HasData(new Suplier { Id = 1, Name = "L-SHOP-TEAM GmbH", Address = "desc1", Country = "Germany" });
            modelBuilder.Entity<Suplier>().HasData(new Suplier { Id = 2, Name = "Cotton Classic Handels GmbH", Address = "desc2", Country = "Netherlands" });
            modelBuilder.Entity<Suplier>().HasData(new Suplier { Id = 3, Name = "UTT Europe Kft.", Address = "desc3", Country = "Poland" });
            modelBuilder.Entity<Suplier>().HasData(new Suplier { Id = 4, Name = "SIPEC SpA", Address = "desc4", Country = "Czech Republic" });
            modelBuilder.Entity<Suplier>().HasData(new Suplier { Id = 5, Name = "MAXIM Ceramics Sp. z o. o. Sp. k.", Address = "desc5", Country = "Greece" });
            modelBuilder.Entity<Suplier>().HasData(new Suplier { Id = 6, Name = "TROIKA Germany GmbH", Address = "desc6", Country = "Spain" });
            modelBuilder.Entity<Suplier>().HasData(new Suplier { Id = 7, Name = "Halfar System GmbH", Address = "desc7", Country = "Spain" });
            modelBuilder.Entity<Suplier>().HasData(new Suplier { Id = 8, Name = "Araco International B.V.", Address = "desc8", Country = "Poland" });
            modelBuilder.Entity<Suplier>().HasData(new Suplier { Id = 9, Name = "Clipper B.V.", Address = "desc9", Country = "Germany" });
            modelBuilder.Entity<Suplier>().HasData(new Suplier { Id = 10, Name = "Toppoint B.V.", Address = "desc10", Country = "Poland" });

            modelBuilder.Entity<SuplierStoreItem>().HasData(new SuplierStoreItem { StoreItemId = 1, SuplierId = 1, SuplierPrice = 10});
            modelBuilder.Entity<SuplierStoreItem>().HasData(new SuplierStoreItem { StoreItemId = 1, SuplierId = 2, SuplierPrice = 20 });
            modelBuilder.Entity<SuplierStoreItem>().HasData(new SuplierStoreItem { StoreItemId = 2, SuplierId = 1, SuplierPrice = 30 });
            modelBuilder.Entity<SuplierStoreItem>().HasData(new SuplierStoreItem { StoreItemId = 2, SuplierId = 4, SuplierPrice = 40 });
            modelBuilder.Entity<SuplierStoreItem>().HasData(new SuplierStoreItem { StoreItemId = 2, SuplierId = 5, SuplierPrice = 55 });
            modelBuilder.Entity<SuplierStoreItem>().HasData(new SuplierStoreItem { StoreItemId = 6, SuplierId = 1, SuplierPrice = 70 });
            modelBuilder.Entity<SuplierStoreItem>().HasData(new SuplierStoreItem { StoreItemId = 7, SuplierId = 4, SuplierPrice = 100 });
            modelBuilder.Entity<SuplierStoreItem>().HasData(new SuplierStoreItem { StoreItemId = 4, SuplierId = 6, SuplierPrice = 22 });
            modelBuilder.Entity<SuplierStoreItem>().HasData(new SuplierStoreItem { StoreItemId = 5, SuplierId = 7, SuplierPrice = 22 });
        }
    }
}
