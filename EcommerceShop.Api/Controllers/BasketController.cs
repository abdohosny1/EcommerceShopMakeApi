using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;

        }

        [HttpGet]

        public async Task<ActionResult<CustomerBasket>> GetBasketById(string Id)
        {
            var res = await _basketRepository.GetBasketAsync(Id);
            return Ok(res ?? new CustomerBasket(Id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket customerBasket)
        {
            var basket = await _basketRepository.UpdateBasketAsync(customerBasket);
            return Ok(basket);
        }

        [HttpDelete]
        public async Task DeleteeBasket(string Id)
        {
            await _basketRepository.DeleteBasketAsync(Id);
        }

    }
}
