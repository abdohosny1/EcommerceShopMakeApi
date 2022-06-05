﻿using EcommerceShop.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
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
            var res= await _dbContext.Set<T>().FindAsync(id);
            return res;
        }
    }
}
