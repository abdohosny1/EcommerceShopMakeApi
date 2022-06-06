

namespace EcommerceShop.EF.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public ProductRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            var res = await _dbContext.products
                    .Include(e=>e.ProductBrand)
                    .Include(e=>e.ProductType)
                    .ToListAsync();

            return res;
        }

        public async Task<IEnumerable<ProductBrand>> GetAllProductBrand()
        {
            return await _dbContext.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetAllProductType()
        {
            return await _dbContext.ProductTypes.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            var res= await _dbContext.products
                 .Include(e => e.ProductBrand)
                 .Include(e => e.ProductType)
                .FirstOrDefaultAsync(e=>e.Id==id);
            return res;
        }
    }
}
