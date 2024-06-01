using Microsoft.AspNetCore.Mvc;
using post_office_back.Dtos;
using post_office_back.Models;
using post_office_back.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace post_office_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly ShipmentService _shipmentService;
        private readonly ILogger<ShipmentController> _logger;

        public ShipmentController(ShipmentService shipmentService, ILogger<ShipmentController> logger)
        {
            _shipmentService = shipmentService;
            _logger = logger;
        }
        // GET: api/<ShipmentController>
        [HttpGet]
        public List<ShipmentRequestDto> ReadAllShipments()
        {
            return _shipmentService.ReadAllShipments();
        }

        // POST api/<ShipmentController/Initial
        [HttpPost("Initial")]
        public IActionResult CreateShipment([FromBody] ShipmentCreationDto shipmentCreationDto)
        {
            try
            {
                _shipmentService.CreateShipment(shipmentCreationDto);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Failed to create the shipment!");
                return BadRequest(ex.Message);
            }
            _logger.LogInformation("Shipment created!");
            return Ok();
        }

        // POST api/<ShipmentController/Final
        [HttpPost("Final")]
        public IActionResult FinalizeShipment([FromQuery] string shipmentNumber)
        {
            try
            {
                _shipmentService.FinalizeShipment(shipmentNumber);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Failed to finalize the shipment due to invalid input!");
                return BadRequest(ex.Message);
            }
            _logger.LogInformation("Shipment finalized!");
            return Ok();
        }
    }
}
