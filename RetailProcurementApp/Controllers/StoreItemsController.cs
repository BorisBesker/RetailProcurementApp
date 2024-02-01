using AutoMapper;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailProcurementApp.Dto;
using ServiceLayer.Services;

namespace RetailProcurementApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/store-items")]
    //[Authorize]
    public class StoreItemsController : ControllerBase
    {
        private readonly ILogger<StoreItemsController> _logger;
        private readonly IStoreItemService _storeItemService;
        private readonly IMapper _mapper;

        public StoreItemsController(ILogger<StoreItemsController> logger, IStoreItemService storeItemService, IMapper mapper)
        {
            _logger = logger;
            _storeItemService = storeItemService;
            _mapper = mapper;
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StoreItemIdDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult GetStoreItems()
        {
            try
            {
                var storeItems = _mapper.Map<List<StoreItemIdDto>>(_storeItemService.GetStores());

                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }

                return Ok(storeItems);
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(StoreItemIdDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult GetStoreItem(int id)
        {
            try
            {
                var storeItemModel = _storeItemService.GetSpecificItem(id);

                if(storeItemModel == null)
                {
                    return NotFound();
                }

                var storeItem = _mapper.Map<StoreItemIdDto>(storeItemModel);

                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }

                return Ok(storeItem);
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost()]
        [ProducesResponseType(200, Type = typeof(StoreItemIdDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateStoreItem([FromBody] StoreItemDto storeItem)
        {
            try
            {
                if (storeItem == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }

                var storeItemDbModel = _mapper.Map<StoreItem>(storeItem);

                var response = _storeItemService.CreateItem(storeItemDbModel);

                if (response.ExistSameName)
                {
                    ModelState.AddModelError("", "Itesm with same Name already exists");
                    return StatusCode(422, ModelState);
                }

                var responseModel = _mapper.Map<StoreItemIdDto>(response.Entity);

                return Ok(responseModel);
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateStoreItem([FromRoute] int id, [FromBody] StoreItemDto storeItem)
        {
            try
            {
                if (storeItem == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }

                var storeItemDbModel = _mapper.Map<StoreItem>(storeItem);

                var response = _storeItemService.UpdateItem(id, storeItemDbModel);

                if (!response.RecordExists)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteStoreItem([FromRoute] int id)
        {
            try
            {
                var response = _storeItemService.DeleteItem(id);

                if (!response.RecordExists)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
