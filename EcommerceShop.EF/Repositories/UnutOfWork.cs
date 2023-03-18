using EcommerceShop.Core.Model.OrderAggragate;
using EcommerceShop.Core.Specafiation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.EF.Repositories
{
    public class UnutOfWork : IUnutOfWork
    {
        private  Hashtable _repositories;
        private  readonly ApplicationDBContext _dbContext;
      //  public IBaseRepository<Product> Products { get; private set; }
     //   public IBaseRepository<DeliveryMethod> DeliveryMethods { get; private set; }
    //    public IBaseRepository<Order> Orders { get; private set; }

    

        public UnutOfWork(ApplicationDBContext context)
        {
            _dbContext = context;
           // Orders = new BaseRepository<Order>(_dbContext);
          //  DeliveryMethods = new BaseRepository<DeliveryMethod>(_dbContext);
          //  Products = new BaseRepository<Product>(_dbContext);

        }
        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IBaseRepository<T> Repository<T>() where T : BaseModel
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var reposutoryType = typeof(BaseRepository<>);
                var repositoryInstance = Activator.CreateInstance(reposutoryType.MakeGenericType
                    (typeof(T)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IBaseRepository<T>)_repositories[type]!;
        }
    }
}
