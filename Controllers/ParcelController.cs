using Microsoft.AspNetCore.Mvc;
using post_office_back.Dtos;
using post_office_back.Services;

namespace post_office_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        private readonly ParcelService _parcelService;
        private readonly ILogger<ParcelController> _logger;

        public ParcelController(ParcelService parcelService, ILogger<ParcelController> logger)
        {
            _parcelService = parcelService;
            _logger = logger;
        }

        // POST api/<ParcelController>
        [HttpPost]
        public IActionResult CreateParcel([FromBody] ParcelCreationDto parcelCreationDto)
        {
            try
            {
                _parcelService.CreateParcel(parcelCreationDto);
            }
            catch (ArgumentException ex) 
            {
                _logger.LogError("Failed to create parcel!");
                return BadRequest(ex.Message);
            }
            _logger.LogInformation("Parcel created!");
            return Ok();
        }
    }
}
