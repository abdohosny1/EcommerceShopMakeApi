﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Core.Model.OrderAggragate
{
    public class Order :BaseModel
    {
        public Order() { }
        public Order(IReadOnlyList<OrderItem> orderItems, string buyerEmail,
            Address shipToAddress, 
            DeliveryMethod deliveryMethod,
            decimal subTotal,
            string paymentIntentId
           )
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethods = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
           PaymentIntentId= paymentIntentId;
        }

        public string BuyerEmail { get; set; }

        public Address ShipToAddress { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public DeliveryMethod DeliveryMethods { get; set; }


        public IReadOnlyList<OrderItem> OrderItems { get; set; }

        public decimal SubTotal { get; set; }

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public string? PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethods.Price;
        }
    }
}
