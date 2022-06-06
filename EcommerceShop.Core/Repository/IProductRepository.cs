using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Core.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<IReadOnlyList<ProductType>> GetAllProductType();
        Task<IEnumerable<ProductBrand>> GetAllProductBrand();
        Task<Product> GetById(int id);
    }
}
