using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendSMSController : ControllerBase
    {
        private readonly ISMSService _sMSService;


        public SendSMSController(ISMSService sMSService)
        {
            _sMSService = sMSService;
        }

        [HttpPost("send")]

        public IActionResult send(SendSMSDto dto)
        {
            var result = _sMSService.Send(dto.MobileNumber, dto.Body);

            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return BadRequest(result.ErrorMessage);

            return Ok(result);

        }
    }
}
