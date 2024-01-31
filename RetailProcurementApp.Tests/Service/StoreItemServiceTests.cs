using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Models;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Models;
using ServiceLayer.ServicesImplementation;

namespace RetailProcurementApp.Tests.Service
{
    public class StoreItemServiceTests
    {
        private readonly IUnitOfWork _database;

        public StoreItemServiceTests()
        {
            _database = GetDatabaseContext();
        }

        [Fact]
        public void GetStores_StoresExist_ReturnsCollection()
        {
            // Arange 
            var service = new StoreItemService(_database);

            // Act 
            var result = service.GetStores();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<StoreItem>>();
            result.Should().HaveCount(10);
        }

        [Fact]
        public void GetSpecificItem_StoreExist_ReturnsStoreItem()
        {
            // Arange 
            var service = new StoreItemService(_database);

            // Act 
            var result = service.GetSpecificItem(1);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<StoreItem>();
        }

        [Fact]
        public void GetSpecificItem_ItemNotExist_ReturnsNull()
        {
            // Arange 
            var service = new StoreItemService(_database);

            // Act 
            var result = service.GetSpecificItem(11);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void CreateItem_ExistsWithSameName_ReturnsSucessFalse()
        {
            // Arange             
            var service = new StoreItemService(_database);
            var itemModel = new StoreItem { ItemName = "item1", ItemDescription = "CreateItemDsc", Price = 100 };

            //Act
            var result = service.CreateItem(itemModel);

            // Assert
            result.Should().BeOfType<ServiceResponse<StoreItem>>();
            result.Success.Should().Be(false);
            result.ExistSameName.Should().Be(true);
            result.Entity.Should().BeNull();
        }

        [Fact]
        public void CreateItem_NotExistsWithSameName_ReturnsSucess()
        {
            // Arange             
            var service = new StoreItemService(_database);
            var itemModel = new StoreItem { ItemName = "testItem1", ItemDescription = "CreateItemDsc", Price = 100 };

            //Act
            var result = service.CreateItem(itemModel);
            var createdItem = _database.StoreItems.Get(result?.Entity?.Id);

            // Assert
            result.Should().BeOfType<ServiceResponse<StoreItem>>();
            result.Success.Should().Be(true);
            result.ExistSameName.Should().Be(false);
            result.Entity.Should().NotBeNull();
            result.Entity.ItemName.Should().Be("testItem1");
            result.Entity.ItemDescription.Should().Be("CreateItemDsc");
            result.Entity.Price.Should().Be(100);
            createdItem.Should().NotBeNull();
        }

        [Fact]
        public void UpdateItem_ItemNotExist_ReturnsSucessFalse()
        {
            // Arange             
            var service = new StoreItemService(_database);
            int itemId = 11;
            var itemModel = new StoreItem { ItemName = "UpdateItem", ItemDescription = "UpdateItemDsc", Price = 100 };

            // Act 
            var result = service.UpdateItem(itemId, itemModel);
            var updatedItem = _database.StoreItems.Get(itemId);

            // Assert
            result.Should().BeOfType<ServiceResponse<StoreItem>>();
            result.Success.Should().Be(false);
            result.RecordExists.Should().Be(false);
            updatedItem.Should().BeNull();
        }

        [Fact]
        public void UpdateItem_ItemExist_ReturnsSucess()
        {
            // Arange             
            var service = new StoreItemService(_database);
            int itemId = 1;
            var itemModel = new StoreItem { ItemName = "UpdateItem", ItemDescription = "UpdateItemDsc", Price = 100 };

            // Act 
            var result = service.UpdateItem(itemId, itemModel);
            var updatedItem = _database.StoreItems.Get(itemId);

            // Assert
            result.Should().BeOfType<ServiceResponse<StoreItem>>();
            result.Success.Should().Be(true);
            result.RecordExists.Should().Be(true);
            result.Entity.Should().NotBeNull();
            result.Entity.ItemName.Should().Be("UpdateItem");
            result.Entity.ItemDescription.Should().Be("UpdateItemDsc");
            result.Entity.Price.Should().Be(100);
            updatedItem.Should().NotBeNull();
        }

        [Fact]
        public void DeleteItem_ItemNotExist_ReturnsSucessFalse()
        {
            // Arange             
            var service = new StoreItemService(_database);
            int itemId = 11;

            // Act 
            var result = service.DeleteItem(itemId);

            // Assert
            result.Should().BeOfType<ServiceResponse<StoreItem>>();
            result.Success.Should().Be(false);
            result.RecordExists.Should().Be(false);
        }

        [Fact]
        public void DeleteItem_ItemExist_ReturnsSucess()
        {
            // Arange             
            var service = new StoreItemService(_database);
            int itemId = 10;

            // Act 
            var result = service.DeleteItem(itemId);
            var deletedItem = _database.StoreItems.Get(itemId);

            // Assert
            result.Should().BeOfType<ServiceResponse<StoreItem>>();
            result.Success.Should().Be(true);
            result.RecordExists.Should().Be(true);
            deletedItem.Should().BeNull();
        }

        private UnitOfwork GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<RetailProcurementContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new RetailProcurementContext(options);
            UnitOfwork  database  = new UnitOfwork(databaseContext);
            databaseContext.Database.EnsureCreated();

            // Already filled with data from inital migration, ideal it with be to insert here
            // indepented data => if the data from migration change not to affect the tests

            return database;
        }
    }
}
