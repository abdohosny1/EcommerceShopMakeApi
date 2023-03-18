using EcommerceShop.Core.Model.OrderAggragate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Core.Specafiation
{
    public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
    {
        public OrderByPaymentIntentIdSpecification(string paymentIntentId) 
            : base(o=>o.PaymentIntentId ==paymentIntentId)
        {
        }
    }
}
