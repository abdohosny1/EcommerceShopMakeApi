using EcommerceShop.Core.Model.OrderAggragate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Core.Repository
{
    public interface IOrderService
    {
        Task<Order> CraeteOrderAsunc(string buyerEmail,
            int deliveryMethod, string basketId, Address shappingAddress);

        Task<IReadOnlyList<Order>> GetOrderForUser(string buyerEmail);

        Task<Order> GetOrderById(int id, string buyerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethod();

        void Add(Order entity);
    
        Task<Order> AddAsync(Order entity);
    }
}
