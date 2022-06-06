

namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository  _ibaseRepository;

        public ProductController(IProductRepository ibaseRepository)
        {
            _ibaseRepository = ibaseRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetProducts()
        {
            var res=await _ibaseRepository.GetAll();
            return Ok(res);
            
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetProduct(int id)
        {
            var book = await _ibaseRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _ibaseRepository.GetAllProductBrand());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
        {
            return Ok(await _ibaseRepository.GetAllProductType());
        }


    }
}
