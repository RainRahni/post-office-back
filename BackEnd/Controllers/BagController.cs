using Microsoft.AspNetCore.Mvc;
using post_office_back.Dtos;
using post_office_back.Services;

namespace post_office_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BagController : ControllerBase
    {
        private readonly BagService _bagService;
        private readonly ILogger<BagController> _logger;
        public BagController(BagService bagService, ILogger<BagController> logger)
        {
            _bagService = bagService;
            _logger = logger;
        }

        // POST api/<BagController>
        [HttpPost]
        public void CreateBag([FromBody] BagCreationDto bagCreationDto)
        {
            _bagService.CreateBag(bagCreationDto);         
            _logger.LogInformation("Bag created!");
        }
        // POST api/<BagController>/Letters
        [HttpPost("Letters")]
        public void AddLetters([FromBody] LetterAddingDto letterAddingDto)
        {
             _bagService.AddLetters(letterAddingDto);
            _logger.LogInformation("Letters added!");
        }
    }
}
