using EcommerceShop.Core.Model.OrderAggragate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.EF.Configure
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(i => i.ItemOrdered, o => { o.WithOwner(); });

            builder.Property(u => u.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
