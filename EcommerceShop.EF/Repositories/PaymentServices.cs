using EcommerceShop.Core.Model.OrderAggragate;
using EcommerceShop.Core.Specafiation;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = EcommerceShop.Core.Model.Product;

namespace EcommerceShop.EF.Repositories
{
    public class PaymentServices : IPaymentServices
    {

        private readonly IUnutOfWork _unutOfWork;

        private readonly IBasketRepository _basketRepository;


        private readonly IConfiguration _configuration;

        public PaymentServices(IUnutOfWork unutOfWork,IBasketRepository basketRepository, IConfiguration configuration)
        {
            _unutOfWork = unutOfWork;
            _basketRepository = basketRepository;
            _configuration = configuration;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];

            var baske= await _basketRepository.GetBasketAsync(basketId);

            if (baske == null) return null!;
            var sgippingPrice = 0m;
             
            if(baske.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unutOfWork.Repository<DeliveryMethod>().GetById((int)baske.DeliveryMethodId);

                sgippingPrice = deliveryMethod.Price;
            }

            foreach (var item in baske.basketItems)
            {
                var productItem = await _unutOfWork.Repository<Product>().GetById(item.Id);
                
                if(item.Price !=productItem.Price)
                {
                    item.Price=productItem.Price;
                }


            }

            var services = new PaymentIntentService();

            PaymentIntent intent;

            if(string.IsNullOrEmpty(baske.PaymentIntentedId)) 
            {
                var option = new PaymentIntentCreateOptions
                {
                    Amount = (long)baske.basketItems.Sum(i => i.Quentity * (i.Price * 100)) + (long)sgippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes=new List<string> {"card"}
                };
                intent =await services.CreateAsync(option);
                baske.PaymentIntentedId = intent.Id;
                baske.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var option = new PaymentIntentUpdateOptions
                {
                    Amount = (long)baske.basketItems.Sum(i => i.Quentity * (i.Price * 100)) + (long)sgippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                await services.UpdateAsync(baske.PaymentIntentedId, option);

            }

            await _basketRepository.UpdateBasketAsync(baske);

            return baske;

        }

        public async Task<Order> updateorderPaymentFailed(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);

            var order = await _unutOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if (order == null) return null;

            order.OrderStatus = OrderStatus.PaymentFailed;

            await _unutOfWork.Complete();

            return order;
        }

        public async Task<Order> updateorderPaymentSucceeded(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);

            var order = await _unutOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if (order == null) return null;

            order.OrderStatus = OrderStatus.PaymentRecevied;
            _unutOfWork.Repository<Order>().Update(order);

            await _unutOfWork.Complete();

            return null;
        }
    }
}
