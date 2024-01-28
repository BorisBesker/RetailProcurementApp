using AutoMapper;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using RetailProcurementApp.Dto;
using ServiceLayer.Services;

namespace RetailProcurementApp.Controllers
{
    [ApiController]
    [Route("/api/store-items")]
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<StoreItemDto>))]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                var storeItesms = _mapper.Map<List<StoreItemDto>>(_storeItemService.GetStores());

                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }

                return Ok(storeItesms);
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
        public IActionResult GetStoreItem(int id)
        {
            try
            {
                var storeItemModel = _storeItemService.GetSpecificStore(id);

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
        [ProducesResponseType(200, Type = typeof(StoreItemDto))]
        [ProducesResponseType(400)]
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
                    ModelState.AddModelError("", "Name already exists");
                    return StatusCode(422, ModelState);
                }

                var responseModel = _mapper.Map<StoreItemIdDto>(response.StoreItem);

                return Ok(responseModel);
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(StoreItemDto))]
        [ProducesResponseType(400)]
        public IActionResult UpdateStoreItem([FromRoute] int id, [FromBody] StoreItemDto storeItem)
        {
            //if(storeItem == null)
            //{
            //    return BadRequest(modelState):
            //}

            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            return Ok(storeItem);
        }
    }
}
