

using EcommerceShop.Api.Dto;


namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<ProductBrand> _brandRepository;
        private readonly IBaseRepository<ProductType> _typeRepository;
        private readonly IMapper _mapper;

        public ProductController
            (IBaseRepository<Product> productRepository, 
            IBaseRepository<ProductBrand> brandRepository,
            IBaseRepository<ProductType> typeRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetProducts()
        {
            var spec = new ProductWithTypeAndBrand();
            var res=await _productRepository.GetAllAsync(spec);
            //var products = res.Select(product => new ProductDto
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Description = product.Description,
            //    Price = product.Price,
            //    PictureUrl = product.PictureUrl,
            //    ProductBrand = product.ProductBrand.Name,
            //    ProductType = product.ProductType.Name
            //}).ToList();
            var products = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(res);
            return Ok(products);
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetProduct(int id)
        {
            var spec = new ProductWithTypeAndBrand(id);

            var product = await _productRepository.GetEntityWithSpec(spec);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            //var newProduct = new ProductDto
            //{
            //    Id=product.Id,
            //    Name=product.Name,
            //    Description=product.Description,
            //    Price=product.Price,
            //    PictureUrl=product.PictureUrl,
            //    ProductBrand=product.ProductBrand.Name,
            //    ProductType=product.ProductType.Name
            //};
            var newProduct = _mapper.Map<Product, ProductDto>(product);
            return Ok(newProduct);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _brandRepository.GetAll());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _typeRepository.GetAll());
        }


    }
}
