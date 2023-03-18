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

        public async Task<IReadOnlyList<T>> GetAll()
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

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
          //  _dbContext.SaveChanges();
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State= EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();

        }
    }
}