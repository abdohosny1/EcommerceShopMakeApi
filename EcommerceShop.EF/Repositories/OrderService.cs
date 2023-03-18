using EcommerceShop.Core.Model.OrderAggragate;
using EcommerceShop.Core.Specafiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.EF.Repositories
{
    public class OrderService : IOrderService
    {
        private readonly IUnutOfWork _unutOfWork;
        private readonly IBasketRepository _basletrepo;
        private readonly IPaymentServices _paymentServices;

        public OrderService(
            IUnutOfWork unutOfWork,
            IBasketRepository basletrepo,
            IPaymentServices paymentServices)
        {
             _unutOfWork=unutOfWork;
            _basletrepo = basletrepo;
            _paymentServices=paymentServices;
        }

        public void Add(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<Order> AddAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> CraeteOrderAsunc(string buyerEmail, int deliveryMethodId, string basketId, Address shappingAddress)
        {
            //get basket from repo
            var basket = await _basletrepo.GetBasketAsync(basketId);

            //get items from product repo
            var items = new List<OrderItem>();

            foreach (var item in basket.basketItems)
            {
                var productItem = await _unutOfWork.Repository<Product>().GetById(item.Id);
                var itemOrder = new ProductItemOrdered(productItem.Id,productItem.Name,productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrder,productItem.Price,item.Quentity);

                items.Add(orderItem);
            }

            //get deliveryMetod from repo
            var deliveryMethod = await _unutOfWork.Repository<DeliveryMethod>().GetById(deliveryMethodId);

            //calac suptotal

            var subTotal = items.Sum(s => s.Price * s.Quentity);
            ///asd

           // check to see the order exists
            var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentedId);
            var existingOrder = await _unutOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if (existingOrder != null)
            {
                _unutOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentServices.CreateOrUpdatePaymentIntent(basket.PaymentIntentedId);
            }

            //create order
            var order = new Order(items, buyerEmail, shappingAddress, deliveryMethod, subTotal,basket.PaymentIntentedId);
            //save to db

              _unutOfWork.Repository<Order>().Add(order);
                    var result =await    _unutOfWork.Complete();
                 if (result <= 0) return null;

         //   delete basket async
           await _basletrepo.DeleteBasketAsync(basketId);
            //rerturn order
            return order;
        }

        public async Task<Order> GetOrderById(int id, string buyerEmail)
        {
            
                 var spec = new OrderWithItemAndOrderingSepcofication(id,buyerEmail);
            return await _unutOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethod()
        {
            return await _unutOfWork.Repository<DeliveryMethod>().GetAll();

        }

        public async Task<IReadOnlyList<Order>> GetOrderForUser(string buyerEmail)
        {

            var spec = new OrderWithItemAndOrderingSepcofication(buyerEmail);
            return await _unutOfWork.Repository<Order>().GetAllAsync(spec);
        }
    }
}
