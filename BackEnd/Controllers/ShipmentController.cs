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
        public void CreateShipment([FromBody] ShipmentCreationDto shipmentCreationDto)
        {

                _shipmentService.CreateShipment(shipmentCreationDto);
                _logger.LogInformation("Shipment created!");
            
        }

        // POST api/<ShipmentController/Final
        [HttpPost("Final")]
        public void FinalizeShipment([FromQuery] string shipmentNumber)
        {
             _shipmentService.FinalizeShipment(shipmentNumber);
            _logger.LogInformation("Shipment finalized!");
        }
    }
}
