﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.EF.Configure
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
           builder.Property(p=>p.Id).IsRequired(); 
           builder.Property(p=>p.Name).IsRequired().HasMaxLength(100); 
           builder.Property(p=>p.Description).IsRequired().HasMaxLength(200); 
           builder.Property(p=>p.Price).IsRequired().HasColumnType("decimal(18,2)");
           builder.Property(p => p.PictureUrl).IsRequired();
           builder.HasOne(e => e.ProductBrand).WithMany()
                .HasForeignKey(e => e.ProductBrandId);
           builder.HasOne(e => e.ProductType).WithMany()
               .HasForeignKey(e => e.ProductTypeId);
        }
    }
}
