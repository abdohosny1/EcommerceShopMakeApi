
using EcommerceShop.Core.Model.identity;
using EcommerceShop.Core.Model.OrderAggragate;
using EcommerceShop.EF.Configure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.EF
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //new OrderConfiguration().Configure(modelBuilder.Entity<Order>());
            //new OrderItemConfiguration().Configure(modelBuilder.Entity<OrderItem>());
            //new DeliveryMethodConfiguration().Configure(modelBuilder.Entity<DeliveryMethod>());
            //new ProductConfiguration().Configure(modelBuilder.Entity<Product>());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.SqlServer")
            {
                foreach (var item in modelBuilder.Model.GetEntityTypes())
                {
                    var propertity = item.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    //var dataTimeProp = item.ClrType.GetProperties()
                    //    .Where(p => p.PropertyType == typeof(DateTimeOffset) 
                    //        || p.PropertyType == typeof(DateTimeOffset?));

                    foreach (var prop in propertity)
                    {
                        modelBuilder.Entity(item.Name).Property(prop.Name)
                            .HasConversion<double>();
                    }

                    //foreach (var prop in dataTimeProp)
                    //{
                    //    modelBuilder.Entity(item.Name).Property(prop.Name)
                    //        .HasConversion(new DateTimeOffsetToBinaryConverter());

                    //    //modelBuilder
                    //    //     .Entity(item.Name)
                    //    //     .Property(item.Name)
                    //    //      .HasConversion(new DateTimeOffsetToBinaryConverter());
                    //}

                }
            }
           base.OnModelCreating(modelBuilder);

        }
        public virtual DbSet<Product> products { get; set; }
        public virtual DbSet<ProductBrand> ProductBrands { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    }
}
