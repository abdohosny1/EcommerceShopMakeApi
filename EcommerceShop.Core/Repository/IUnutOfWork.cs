using EcommerceShop.Core.Model.OrderAggragate;
using EcommerceShop.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Core.Repository
{
    public interface IUnutOfWork :IDisposable
    {
         IBaseRepository<T> Repository<T>()where T : BaseModel;
      //  IBaseRepository<Product> Products { get; }
       // IBaseRepository<DeliveryMethod> DeliveryMethods { get; }
      //  IBaseRepository<Order> Orders { get; }
       Task<int> Complete();
    }
}
