using EcommerceShop.Core.Model.OrderAggragate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Core.Repository
{
    public interface IPaymentServices
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);

        Task<Order> updateorderPaymentSucceeded(string paymentIntentId);
        Task<Order> updateorderPaymentFailed(string paymentIntentId);
    }
}
