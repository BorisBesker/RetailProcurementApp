using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RetailProcurementApp.Controllers;
using RetailProcurementApp.Dto;
using ServiceLayer.Models;
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

        [Fact]
        public void GetStoreItem_ItemNotExists_ReturnsNotFound()
        {
            // Arange 
            int storeItemid = 1;
            A.CallTo(() => _storeItemService.GetSpecificItem(1)).Returns(null);
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);

            // Act
            var result = controller.GetStoreItem(storeItemid);
            var objectResult = result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        public void GetStoreItem_ExceptionRaised_Returns500Error()
        {
            // Arange 
            int storeItemid = 1;
            var storeItems = A.Fake<ICollection<StoreItemDto>>();
            A.CallTo(() => _storeItemService.GetSpecificItem(storeItemid)).Throws<Exception>();
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);

            // Act
            var result = controller.GetStoreItem(storeItemid);
            var objectResult = result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult?.StatusCode);
        }

        [Fact]
        public void GetStoreItem_RecordFound_ReturnsOk()
        {
            // Arange 
            int storeItemid = 1;
            var storeItemModel = A.Fake<StoreItem>();
            var storeItemDto = A.Fake<StoreItemIdDto>();
            A.CallTo(() => _storeItemService.GetSpecificItem(storeItemid)).Returns(storeItemModel);
            A.CallTo(() => _mapper.Map<StoreItemIdDto>(storeItemModel)).Returns(storeItemDto);
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);

            // Act
            var result = controller.GetStoreItem(storeItemid);
            var objectResult = result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void CreateStoreItems_ModelNull_ReturnsBadRequest()
        {
            // Arange 
            StoreItemDto storeItemDto = null;
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);

            // Act
            var result = controller.CreateStoreItem(storeItemDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestResult));
        }

        [Fact]
        public void CreateStoreItem_InvalidModel_ReturnsBadRequest()
        {
            // Arange 
            StoreItemDto storeItemDto = new StoreItemDto();
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);
            controller.ModelState.AddModelError("fakeError", "fakeError");
            // Act
            var result = controller.CreateStoreItem(storeItemDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }

        [Fact]
        public void CreateStoreItem_ExceptionRaised_Returns500Error()
        {
            // Arange 
            StoreItemDto storeItemDto = new StoreItemDto();
            A.CallTo(() => _mapper.Map<StoreItem>(storeItemDto)).Throws<Exception>();
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);

            // Act
            var result = controller.CreateStoreItem(storeItemDto);
            var objectResult = result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult?.StatusCode);
        }

        [Fact]
        public void CreateStoreItem_ExistWithSameName_Returns422Error()
        {
            // Arange 
            var storeItemDto = A.Fake<StoreItemDto>();
            var storeItemDbModel = A.Fake<StoreItem>();
            A.CallTo(() => _mapper.Map<StoreItem>(storeItemDto)).Returns(storeItemDbModel);
            A.CallTo(() => _storeItemService.CreateItem(storeItemDbModel)).Returns(new ServiceResponse<StoreItem> { ExistSameName = true});
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);

            // Act
            var result = controller.CreateStoreItem(storeItemDto);
            var objectResult = result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, objectResult?.StatusCode);
        }

        [Fact]
        public void CreateStoreItem_ValidModel_ReturnsOk()
        {
            // Arange 
            var storeItemDto = A.Fake<StoreItemDto>();
            var storeItemDbModel = A.Fake<StoreItem>();
            var response = A.Fake<ServiceResponse<StoreItem>>().Entity;
            A.CallTo(() => _mapper.Map<StoreItem>(storeItemDto)).Returns(storeItemDbModel);
            A.CallTo(() => _storeItemService.CreateItem(storeItemDbModel)).Returns(new ServiceResponse<StoreItem> { ExistSameName = false });
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);

            // Act
            var result = controller.CreateStoreItem(storeItemDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void UpdateStoreItem_ModelNull_ReturnsBadRequest()
        {
            int storeItemId = 1;
            StoreItemDto storeItemDto = null;
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);

            // Act
            var result = controller.UpdateStoreItem(storeItemId, storeItemDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestResult));
        }

        [Fact]
        public void UpdateStoreItem_InvalidModel_ReturnsBadRequest()
        {
            // Arange 
            int storeItemId = 1;
            StoreItemDto storeItemDto = new StoreItemDto();
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);
            controller.ModelState.AddModelError("fakeError", "fakeError");

            // Act
            var result = controller.UpdateStoreItem(storeItemId, storeItemDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }

        [Fact]
        public void UpdateStoreItem_ExceptionRaised_Returns500Error()
        {
            // Arange 
            int storeItemId = 1;
            StoreItemDto storeItemDto = new StoreItemDto();
            A.CallTo(() => _mapper.Map<StoreItem>(storeItemDto)).Throws<Exception>();
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);

            // Act
            var result = controller.UpdateStoreItem(storeItemId, storeItemDto);
            var objectResult = result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult?.StatusCode);
        }

        [Fact]
        public void UpdateStoreItem_RecordNotExists_ReturnsNotFound()
        {
            // Arange 
            int storeItemId = 1;
            var storeItemDto = A.Fake<StoreItemDto>();
            var storeItemDbModel = A.Fake<StoreItem>();
            A.CallTo(() => _mapper.Map<StoreItem>(storeItemDto)).Returns(storeItemDbModel);
            A.CallTo(() => _storeItemService.UpdateItem(storeItemId, storeItemDbModel)).Returns(new ServiceResponse<StoreItem> { RecordExists = false });
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);

            // Act
            var result = controller.UpdateStoreItem(storeItemId, storeItemDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        public void UpdateStoreItem_ValidModel_ReturnsOkNoContent()
        {
            // Arange 
            int storeItemId = 1;
            var storeItemDto = A.Fake<StoreItemDto>();
            var storeItemDbModel = A.Fake<StoreItem>();
            A.CallTo(() => _mapper.Map<StoreItem>(storeItemDto)).Returns(storeItemDbModel);
            A.CallTo(() => _storeItemService.UpdateItem(storeItemId, storeItemDbModel)).Returns(new ServiceResponse<StoreItem> { RecordExists = true });
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);

            // Act
            var result = controller.UpdateStoreItem(storeItemId, storeItemDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
        }

        [Fact]
        public void DeleteStoreItem_ExceptionRaised_Returns500Error()
        {
            // Arange 
            int storeItemId = 1;
            A.CallTo(() => _storeItemService.DeleteItem(storeItemId)).Throws<Exception>();
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);

            // Act
            var result = controller.DeleteStoreItem(storeItemId);
            var objectResult = result as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult?.StatusCode);
        }

        [Fact]
        public void DeleteStoreItem_RecordNotExists_ReturnsNotFound()
        {
            // Arange 
            int storeItemId = 1;
            A.CallTo(() => _storeItemService.DeleteItem(storeItemId)).Returns(new ServiceResponse<StoreItem> { RecordExists = false });
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);

            // Act
            var result = controller.DeleteStoreItem(storeItemId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        public void DeleteStoreItem_ValidModel_ReturnsOkNoContent()
        {
            // Arange 
            int storeItemId = 1;
            A.CallTo(() => _storeItemService.DeleteItem(storeItemId)).Returns(new ServiceResponse<StoreItem> { RecordExists = true });
            var controller = new StoreItemsController(_logger, _storeItemService, _mapper);

            // Act
            var result = controller.DeleteStoreItem(storeItemId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
        }
    }
}
