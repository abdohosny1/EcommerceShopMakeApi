using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.Api.Controllers
{
    public class ProdductTypeController : Controller
    {
        private readonly IBaseRepository<ProductType> _typeRepository;

        public ProdductTypeController(IBaseRepository<ProductType> typeRepository)
        {
            _typeRepository = typeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GettAll()
        {
            return Ok(await _typeRepository.GetAll());
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var type= await _typeRepository.GetById(id);

            if (type == null) return NotFound();

            return Ok(type);

        }
    }
}
