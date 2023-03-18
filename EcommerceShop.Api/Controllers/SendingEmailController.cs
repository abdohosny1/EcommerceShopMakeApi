using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendingEmailController : ControllerBase
    {
        private readonly IMailSendService _mailService;

        public SendingEmailController(IMailSendService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost("send")]

        public async Task<IActionResult> sendEmail( MailRequestDTO mailRequestDTO)
        {
            await _mailService.SendingEmail(mailRequestDTO.tOEmail, mailRequestDTO.Subject, mailRequestDTO.Body);//, mailRequestDTO.Attachment);
            return Ok();
        }

        [HttpPost("SendTemp")]
        public async Task<IActionResult> SendWelcomeEmail( SendTempDto dto)
        {
            var filePath = $"{Directory.GetCurrentDirectory()}\\Templates\\EmailTemplate.html";
            var str = new StreamReader(filePath);

            var mailText = str.ReadToEnd();
            str.Close();

            mailText = mailText.Replace("[username]", dto.UserName).Replace("[number]", dto.OrderNum.ToString())
                .Replace("[date]", dto.OrderDate).Replace("[Total]", dto.OrderTotal.ToString())
                .Replace("[SubTotal]", dto.OrderTotal.ToString()).Replace("[shippingPrice]", dto.shippingPrice.ToString());
               // .Replace("[Address]",dto.Address);

            await _mailService.SendingEmail(dto.Email, "Welcome to ESHOPE", mailText);
            return Ok();
        }
    }
}
