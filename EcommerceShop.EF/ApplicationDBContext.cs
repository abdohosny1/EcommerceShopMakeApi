
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.EF
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.SqlServer")
            {
                foreach (var item in modelBuilder.Model.GetEntityTypes())
                {
                    var propertity = item.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                    foreach (var prop in propertity)
                    {
                        modelBuilder.Entity(item.Name).Property(prop.Name)
                            .HasConversion<double>();

                    }

                }
            }
        }
        public virtual DbSet<Product> products { get; set; }
        public virtual DbSet<ProductBrand> ProductBrands { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
    }
}
