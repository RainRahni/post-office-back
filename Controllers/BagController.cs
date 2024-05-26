using Microsoft.AspNetCore.Mvc;
using post_office_back.Dtos;
using post_office_back.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public IActionResult CreateBag([FromBody] BagCreationDto bagCreationDto)
        {
            try
            {
                _bagService.CreateBag(bagCreationDto);         
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Failed to create the bag!");
                return BadRequest(ex.Message);
            }
            _logger.LogInformation("Bag created!");
            return Ok();
        }
        // POST api/<BagController>
        [HttpPost("Letters")]
        public IActionResult AddLetters([FromBody] LetterAddingDto letterAddingDto)
        {
            try
            {
                _bagService.AddLetters(letterAddingDto);
            }
            catch(ArgumentException ex)
            {
                _logger.LogError("Failed to add letters!");
                return BadRequest(ex.Message);
            }
            _logger.LogInformation("Letters added!");
            return Ok();
        }
    }
}
