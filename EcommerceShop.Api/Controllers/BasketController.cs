using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;


        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<ActionResult<CustomerBasket>> GetBasketById(string Id)
        {
            var res = await _basketRepository.GetBasketAsync(Id);
            return Ok(res ?? new CustomerBasket(Id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(BasketDto basket)
        {
            var custombasket = _mapper.Map<BasketDto,CustomerBasket>(basket);

            var updateBasket = await _basketRepository.UpdateBasketAsync(custombasket);
            return Ok(basket);
        }

        [HttpDelete]
        public async Task DeleteeBasket(string Id)
        {
            await _basketRepository.DeleteBasketAsync(Id);
        }

    }
}
