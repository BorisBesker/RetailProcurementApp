using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RetailProcurementApp.Controllers;
using RetailProcurementApp.Dto;
using ServiceLayer.Services;

namespace RetailProcurementApp.Tests.Controller
{
    public class StoreItemsControllerTests
    {
        private readonly IStoreItemService _storeItemService;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreItemsController> _logger;

        public StoreItemsControllerTests()
        {
            _storeItemService = A.Fake<IStoreItemService>();
            _mapper = A.Fake<IMapper>();
            _logger = A.Fake<ILogger<StoreItemsController>>();
        }

        [Fact]
        public void GetStoreItems_NoException_ReturnsOk() 
        {
            // Arange 
            var storeItems = A.Fake<ICollection<StoreItemDto>>();
            var storeItemlist = A.Fake<List<StoreItemIdDto>>();
            A.CallTo(()=>_mapper.Map<List<StoreItemIdDto>>(storeItems)).Returns(storeItemlist);
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);

            // Act
            var result = controller.GetStoreItems();
            var objectResult = result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void GetStoreItems_ExceptionRaised_Returns500Error()
        {
            // Arange 
            var storeItems = A.Fake<ICollection<StoreItemDto>>();
            A.CallTo(() => _mapper.Map<List<StoreItemIdDto>>(storeItems)).Throws<Exception>();
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);

            // Act
            var result = controller.GetStoreItems();
            var objectResult = result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult?.StatusCode);
        }
    }
}
