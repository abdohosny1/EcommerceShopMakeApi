using EcommerceShop.Core.Model.OrderAggragate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.EF.Configure
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShipToAddress, a =>
            {
                a.WithOwner();
            });

            builder.Property(e=>e.OrderStatus)
                .HasConversion(
                o=>o.ToString(),
                o=>(OrderStatus) Enum.Parse(typeof(OrderStatus), o));

            builder.HasMany(p => p.OrderItems)
                 .WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }  
}
