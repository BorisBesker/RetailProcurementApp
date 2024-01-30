using AutoMapper;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailProcurementApp.Dto;
using ServiceLayer.Services;

namespace RetailProcurementApp.Controllers
{
    [ApiController]
    [Route("/api/supliers")]
    //[Authorize]
    public class SupliersController : ControllerBase
    {
        private readonly ILogger<SupliersController> _logger;
        private readonly ISuplierService _suplierService;
        private readonly IMapper _mapper;

        public SupliersController(ILogger<SupliersController> logger, ISuplierService storeItemService, IMapper mapper)
        {
            _logger = logger;
            _suplierService = storeItemService;
            _mapper = mapper;
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SuplierIdDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetSupliers()
        {
            try
            {
                var suplierItesms = _mapper.Map<List<SuplierIdDto>>(_suplierService.GetSupliers());

                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }

                return Ok(suplierItesms);
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(SuplierIdDto))]
        [ProducesResponseType(400)]
        public IActionResult GetSuplier(int id)
        {
            try
            {
                var suplierModel = _suplierService.GetSpecificSuplier(id);

                if (suplierModel == null)
                {
                    return NotFound();
                }

                var suplier = _mapper.Map<SuplierIdDto>(suplierModel);

                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }

                return Ok(suplier);
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost()]
        [ProducesResponseType(200, Type = typeof(SuplierIdDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public IActionResult CreateSuplier([FromBody] SuplierDto suplier)
        {
            try
            {
                if (suplier == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }

                var suplierModel = _mapper.Map<Suplier>(suplier);

                var response = _suplierService.CreateSuplier(suplierModel);

                if (response.ExistSameName)
                {
                    ModelState.AddModelError("", "Name already exists");
                    return StatusCode(422, ModelState);
                }

                var responseModel = _mapper.Map<SuplierIdDto>(response.Entity);

                return Ok(responseModel);
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStoreItem([FromRoute] int id, [FromBody] SuplierDto storeItem)
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

                var storeItemDbModel = _mapper.Map<Suplier>(storeItem);

                var response = _suplierService.UpdateSuplier(id, storeItemDbModel);

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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStoreItem([FromRoute] int id)
        {
            try
            {
                var response = _suplierService.DeleteSuplier(id);

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
