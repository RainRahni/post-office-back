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
        public void CreateParcel([FromBody] ParcelCreationDto parcelCreationDto)
        {
            _parcelService.CreateParcel(parcelCreationDto);
            _logger.LogInformation("Parcel created!");
        }
    }
}
