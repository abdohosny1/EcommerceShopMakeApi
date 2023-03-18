using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Core.Model.OrderAggragate
{
    public class OrderItem :BaseModel
    { 

        public OrderItem() { }
        public OrderItem(ProductItemOrdered itemOrdered, decimal price, int quentity)
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quentity = quentity;
        }

        public ProductItemOrdered ItemOrdered { get; set; }

        public decimal Price { get; set; }

        public int Quentity { get; set; }


         
    }
}
