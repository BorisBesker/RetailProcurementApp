using AutoMapper;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using RetailProcurementApp.Dto;
using ServiceLayer.Services;

namespace RetailProcurementApp.Controllers
{
    [ApiController]
    [Route("/api/supplier-store-items")]
    public class SuplierItemController : Controller
    {
        private readonly ILogger<StoreItemsController> _logger;
        private readonly ISuplerItemsService _suplierItemsService;
        private readonly IMapper _mapper;

        public SuplierItemController(ILogger<StoreItemsController> logger, ISuplerItemsService storeItemService, IMapper mapper)
        {
            _logger = logger;
            _suplierItemsService = storeItemService;
            _mapper = mapper;
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SuplierItemDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult GetRelationships()
        {
            try
            {
                var storeItemRelationships = _mapper.Map<List<SuplierItemDto>>(_suplierItemsService.GetSuplierItemRelationships());

                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }

                return Ok(storeItemRelationships);
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateRelationship([FromBody] SuplierItemIdDto suplierItem)
        {
            try
            {
                if (suplierItem == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }

                var suplierItemDbModel = _mapper.Map<SuplierStoreItem>(suplierItem);

                var response = _suplierItemsService.CreateSuplierItemRelationship(suplierItemDbModel);

                if (response.ExistSameName)
                {
                    ModelState.AddModelError("", "Relationship already exists");
                    return StatusCode(422, ModelState);
                }

                if (response.RelationShipEntityMissing)
                {
                    ModelState.AddModelError("", $"No suplier or storeItem records exiist with Id's: {suplierItem.SuplierId} - {suplierItem.StoreItemId}");
                    return StatusCode(422, ModelState);
                }

                return Ok();
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{supplierId}/{storeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStoreItem([FromRoute] int id)
        {
            try
            {
                //var response = _suplierService.DeleteSuplier(id);

                //if (!response.RecordExists)
                //{
                //    return NotFound();
                //}

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
