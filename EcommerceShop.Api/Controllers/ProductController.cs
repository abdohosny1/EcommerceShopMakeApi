using EcommerceShop.Core.Model;
using EcommerceShop.Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IBaseRepository<Product>  _ibaseRepository;

        public ProductController(IBaseRepository<Product> ibaseRepository)
        {
            _ibaseRepository = ibaseRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetProducts()
        {
            var res=await _ibaseRepository.GetAll();
            return Ok(res);
            
        }

        [HttpGet("id")]

        public async Task<IActionResult> GetProduct(int id)
        {
            var book = await _ibaseRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
    }
}
