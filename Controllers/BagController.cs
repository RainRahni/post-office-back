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
        public BagController(BagService bagService)
        {
            _bagService = bagService;
        }
        // GET: api/<BagController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BagController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BagController>
        [HttpPost]
        public HttpResponseMessage CreateBag([FromBody] BagCreationDto bagCreationDto)
        {
            return _bagService.CreateBag(bagCreationDto);
        }
        // POST api/<BagController>
        [HttpPost("Letters")]
        public HttpResponseMessage AddLetters([FromBody] LetterAddingDto letterAddingDto)
        {
            return _bagService.AddLetters(letterAddingDto);
        }

        // PUT api/<BagController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BagController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
