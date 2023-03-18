using EcommerceShop.Api.Extensision;
using EcommerceShop.Core.Model.OrderAggragate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;


        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }



        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {

            var email = HttpContext.User.RetrivewEmailFromPrincipal();

            var address = _mapper.Map<AddressDto, Core.Model.OrderAggragate.Address>(orderDto.ShippingAddress);

            var order = await _orderService.CraeteOrderAsunc(email, orderDto.DeliveryMethodId,
                  orderDto.baskeyId, address);

            if (order == null) return BadRequest(new ApiResponse(400, "proplem Creating order"));

            return Ok(order);

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>>GetOrderForUser()
        {
              var email = HttpContext.User.RetrivewEmailFromPrincipal();
              var order = await _orderService.GetOrderForUser(email);
            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDTO>>(order)) ;

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderByIdForUser(int id)
        {
            var email = HttpContext.User.RetrivewEmailFromPrincipal();
            var order = await _orderService.GetOrderById(id, email);
            if (order == null) return NotFound(new ApiResponse(404));
           // return Ok(order);
            return _mapper.Map<Order,OrderToReturnDTO>(order);
        }

        [HttpGet("deliveryMethod")]
       // [Route("deliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetDeliveryMethod()
        {

            return Ok(await _orderService.GetDeliveryMethod());
        }





    }
}

