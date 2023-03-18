using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Core.Model
{
    public class CustomerBasket
    {

        public CustomerBasket(string id)
        {
            Id = id;

        }
        public CustomerBasket()
        {


        }
         
        public string Id { get; set; }

        public List<BasketItem> basketItems { get; set; } = new List<BasketItem>();

        public int? DeliveryMethodId { get; set; }
        public string? ClientSecret { get; set; }
        public string? PaymentIntentedId { get; set; }

        public decimal? shippingPrice { get; set; }


    }
}