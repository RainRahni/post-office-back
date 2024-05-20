using Microsoft.AspNetCore.Mvc;
using post_office_back.Dtos;
using post_office_back.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace post_office_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly ShipmentService _shipmentService;


        public ShipmentController(ShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/
        [HttpPost]
        public HttpResponseMessage CreateShipment([FromBody] ShipmentDto shipmentDto)
        {
            return _shipmentService.CreateShipment(shipmentDto);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{shipmentNumber}")]
        public void DeleteShipment(String shipmentNumber)
        {

        }
    }
}
