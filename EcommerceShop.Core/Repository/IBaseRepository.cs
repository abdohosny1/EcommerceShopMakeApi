using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Core.Repository
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<IReadOnlyList<T>> GetAll();
        Task<T> GetById(int id);    
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec);
        Task<int> CountAsunc(ISpecification<T> spec);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
         Task<T> AddAsync(T entity);






    }
}
