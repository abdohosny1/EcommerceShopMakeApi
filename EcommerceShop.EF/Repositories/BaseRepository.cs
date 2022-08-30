using EcommerceShop.Core.Specafiation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private readonly ApplicationDBContext _dbContext;

        public BaseRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var res = await _dbContext.Set<T>().ToListAsync();

            return res;
        }
        public async Task<T> GetById(int id)
        {
            var res = await _dbContext.Set<T>().FindAsync(id);
            return res;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).ToListAsync();
        }



        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplaySpecification(ISpecification<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }

        public async Task<int> CountAsunc(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).CountAsync();
        }
    }
}