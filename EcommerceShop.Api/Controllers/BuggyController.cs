using EcommerceShop.Api.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public BuggyController(ApplicationDBContext context)
        {
            _context=context;

        }
        [HttpGet("notFound")]
        public IActionResult GetNotFoundRequest()
        {
            var thing = _context.products.Find(42);
            if(thing == null)
            {
                return NotFound(new ApiResponse(404) );
            }
            
            return Ok();
        }
        [HttpGet("TestAuth")]
        [Authorize]
        public ActionResult<String> GetScretText()
        {
            return "secret stuff";
        }
       

        [HttpGet("servererror")]
        public IActionResult GetServerErrort()
        {
            var thing = _context.products.Find(42);
            var thingToReturn=thing.ToString();

            return Ok();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}
