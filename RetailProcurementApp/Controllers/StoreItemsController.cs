using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using RetailProcurementApp.Dto;

namespace RetailProcurementApp.Controllers
{
    [ApiController]
    [Route("/api/store-items")]
    public class StoreItemsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<StoreItemsController> _logger;
        private readonly IUnitOfWork _database;


        public StoreItemsController(ILogger<StoreItemsController> logger)
        {
            _logger = logger;
            _database = new UnitOfwork();
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StoreItem>))]
        [ProducesResponseType(400)]
        public IEnumerable<StoreItem> Get()
        {
            return new List<StoreItem> { new StoreItem { ItemName = "boris" } };
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(StoreItem))]
        [ProducesResponseType(400)]
        public IActionResult GetStoreItem(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            return Ok(new StoreItem { ItemName = "boris"});
        }

        [HttpPost()]
        [ProducesResponseType(200, Type = typeof(StoreItem))]
        [ProducesResponseType(400)]
        public IActionResult CreateStoreItem(StoreItem storeItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            _database.StoreItems.Add(new Infrastructure.Models.StoreItem { ItemName = "dd", Price = 32, ItemDescription = "desc" });
            _database.Save();

            return Ok(storeItem);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(StoreItem))]
        [ProducesResponseType(400)]
        public IActionResult UpdateStoreItem([FromRoute] int id, [FromBody] StoreItem storeItem)
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
