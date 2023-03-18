using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Core.Model.OrderAggragate
{
    public class ProductItemOrdered
    {

        public ProductItemOrdered() { }
        public ProductItemOrdered(int productItemId, string prpductName, string productUrl)
        {
            ProductItemId = productItemId;
            PrpductName = prpductName;
            ProductUrl = productUrl;
        }

        public int ProductItemId { get; set; }
        public string PrpductName { get; set; }
        public string ProductUrl { get; set; }
    }
}
