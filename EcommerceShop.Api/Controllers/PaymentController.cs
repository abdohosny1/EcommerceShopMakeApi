using EcommerceShop.Core.Model.OrderAggragate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServices _paymentServices;

        private const string whSecret = "";
        private readonly ILogger<IPaymentServices> _logger;

        public PaymentController(IPaymentServices paymentServices, ILogger<IPaymentServices> logger)
        {
            _paymentServices = paymentServices;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("{basketId}")]

        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket=await _paymentServices.CreateOrUpdatePaymentIntent(basketId);

            if (basket == null) return BadRequest(new ApiResponse(400, "Proplem with your basket"));

            return basket;

        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebHook()
        {
            var json=await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signtaure"], whSecret);

            PaymentIntent intent;
            Order order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeeded", intent.Id);
                    // TODO UPDATE order with new ststus
                    order = await _paymentServices.updateorderPaymentSucceeded(intent.Id);
                    _logger.LogInformation("order updated to payment received", order.Id);

                    break;

                case "payment_intent.payment.failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment failed", intent.Id);
                    // TODO UPDATE order with new ststus
                    order = await _paymentServices.updateorderPaymentFailed(intent.Id);
                    _logger.LogInformation("Payment failed", order.Id);

                    break;
            }
            return new EmptyResult();
        }
    }
}
