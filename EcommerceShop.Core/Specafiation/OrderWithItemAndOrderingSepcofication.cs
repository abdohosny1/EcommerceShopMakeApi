using EcommerceShop.Core.Model.OrderAggragate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Core.Specafiation
{
    public class OrderWithItemAndOrderingSepcofication : BaseSpecification<Order>
    {
        public OrderWithItemAndOrderingSepcofication(string email):base(o=>o.BuyerEmail==email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethods);
           // AddInclude(o => o.OrderDate);
        }

        public OrderWithItemAndOrderingSepcofication(int id,string email) : base(o=>o.Id==id && o.BuyerEmail ==email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethods);
        }
    }
}
