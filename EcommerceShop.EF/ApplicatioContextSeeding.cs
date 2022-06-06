using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EcommerceShop.EF
{
    public class ApplicatioContextSeeding
    {
        public static async Task SeedAsync(ApplicationDBContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (context.ProductBrands.Any())
                {
                    var brandData = File.ReadAllText("../EcommerceShop.EF/Seedding/brands.json");
                    var brand = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                    foreach (var item in brand)
                    {
                        context.ProductBrands.Add(item);

                    }
                    await context.SaveChangesAsync();
                }

                if (context.ProductTypes.Any())
                {
                    var typeData = File.ReadAllText("../EcommerceShop.EF/Seedding/types.json");
                    var type = JsonSerializer.Deserialize<List<ProductType>>(typeData);

                    foreach (var item in type)
                    {
                        context.ProductTypes.Add(item);

                    }
                    await context.SaveChangesAsync();
                }

                if (context.products.Any())
                {
                    var productData = File.ReadAllText("../EcommerceShop.EF/Seedding/products.json");
                    var product = JsonSerializer.Deserialize<List<Product>>(productData);

                    foreach (var item in product)
                    {
                        context.products.Add(item);

                    }
                    await context.SaveChangesAsync();
                }



            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ApplicatioContextSeeding>();
                logger.LogError(ex.Message);
            }
        }
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDBContext>();

                context.Database.EnsureCreated();

                try
                {
                    if (!context.ProductBrands.Any())
                    {
                        context.ProductBrands.AddRange(new List<ProductBrand>()
                            {
                                new ProductBrand()
                            {
                                Name = "Angular",
                            },
                                    new ProductBrand()
                            {
                                Name = "NetCore",
                            },
                                        new ProductBrand()
                            {
                                Name = "VS Code",
                            },
                                            new ProductBrand()
                            {
                                Name = "React",
                            },
                                                new ProductBrand()
                            {
                                Name = "Typescript",
                            },
                                                    new ProductBrand()
                            {
                                Name = "MVC",
                            },

                            });



                        context.SaveChanges();
                    }

                    if (!context.ProductTypes.Any())
                    {
                        context.ProductTypes.AddRange(new List<ProductType>()
                            {
                                new ProductType()
                            {
                                Name = "Boards",
                            },
                                    new ProductType()
                            {
                                Name = "Hats",
                            },
                                        new ProductType()
                            {
                                Name = "Gloves",
                            },
                            });



                        context.SaveChanges();
                    }

                    if (!context.products.Any())
                    {
                        context.products.AddRange(new List<Product>()
                            {
                               new Product()
                            {
                              Name= "Angular Speedster Board 2000",
                              Description= "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. ",
                              Price= 200,
                              PictureUrl= "images/products/sb-ang1.png",
                              ProductTypeId= 1,
                              ProductBrandId= 1,
                           },
                               new Product()
                            {
                              Name= "Green Angular Board 3000",
                              Description= "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.",
                              Price= 150,
                              PictureUrl= "images/products/sb-ang2.png",
                              ProductTypeId= 1,
                              ProductBrandId= 1,
                                },
                             new Product()
                            {
                               Name= "Core Board Speed Rush 3",
                               Description= "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis,",
                               Price= 180,
                                PictureUrl= "images/products/sb-core1.png",
                                ProductTypeId= 1,
                               ProductBrandId= 2,
                                },
                             new Product()
                             {
                                Name= "Net Core Super Board",
                                Description= "Pellentesque habitant morbi tristique senectus et netus et es ac",
                                Price= 300,
                                PictureUrl= "images/products/sb-core2.png",
                                ProductTypeId= 1,
                                ProductBrandId= 2,
                                },
                             new Product()
                             {
                               Name= "React Board Super Whizzy Fast",
        Description= "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna se",
        Price= 250,
        PictureUrl= "images/products/sb-react1.png",
        ProductTypeId= 1,
        ProductBrandId= 4,
                                },
                                              new Product()
                             {
               Name = "Typescript Entry Board",
        Description= "Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.",
        Price= 120,
        PictureUrl= "images/products/sb-ts1.png",
        ProductTypeId= 1,
        ProductBrandId= 5
                                },
                                      new Product()
                             {
              Name= "Core Blue Hat",
        Description= "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero,is urna.",
        Price= 10,
        PictureUrl= "images/products/hat-core1.png",
        ProductTypeId= 2,
        ProductBrandId= 2,

                                },


                                      new Product()
                             {
            Name= "Purple React Woolen Hat",
    Description="Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, ",
    Price= 15,
    PictureUrl= "images/products/hat-react2.png",
    ProductTypeId= 2,
    ProductBrandId= 4,
                                },
                                  new Product()
                             {
        Name= "Blue Code Gloves",
    Description= "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.",
    Price= 18,
    PictureUrl= "images/products/glove-code1.png",
    ProductTypeId= 3,
    ProductBrandId= 3,
                                },
                                  new Product()
                             {
          Name= "Green Code Gloves",
    Description= "Pellentesque habitant morbi tristique senectus et netus et malesuada ",
    Price= 15,
    PictureUrl= "images/products/glove-code2.png",
    ProductTypeId= 3,
    ProductBrandId= 3,
                                },
                                           new Product()
                             {
     Name= "Purple React Gloves",
    Description= "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue",
    Price= 16,
    PictureUrl= "images/products/glove-react1.png",
    ProductTypeId= 3,
    ProductBrandId= 4,
                                },
                                 new Product()
                             {
    Name= "Green React Gloves",
    Description= "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ",
    Price= 14,
    PictureUrl= "images/products/glove-react2.png",
    ProductTypeId= 3,
    ProductBrandId= 4,
                                },
                                        new Product()
                             {
    Name= "Redis Red Boots",
    Description= "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattisend. Ut nonummy.",
    Price= 250,
    PictureUrl= "images/products/boot-redis1.png",
    ProductTypeId= 3,
    ProductBrandId= 6,
                                },
                                      new Product()
                             {
    Name= "Core Red Boots",
    Description= "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa.",
    Price= 189,
    PictureUrl= "images/products/boot-core2.png",
    ProductTypeId=3,
    ProductBrandId= 2
                                },
                                         new Product()
                             {
    Name= "Core Purple Boots",
    Description= "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin ",
    Price= 199,
    PictureUrl= "images/products/boot-core1.png",
    ProductTypeId= 3,
    ProductBrandId= 2,
                                },
                                            new Product()
                             {
    Name= "Angular Purple Boots",
    Description= "Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.",
    Price= 150,
    PictureUrl= "images/products/boot-ang2.png",
    ProductTypeId= 3,
    ProductBrandId= 1,
                                },
                                                  new Product()
                             {
    Name= "Angular Blue Boots",
    Description= "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc.t nonummy.",
    Price= 180,
    PictureUrl= "images/products/boot-ang1.png",
    ProductTypeId= 3,
    ProductBrandId= 1,
                                },








                            });



                        context.SaveChanges();
                    }


                }
                catch (Exception ex)
                {
                    //var logger = loggerFactory.CreateLogger<ApplicatioContextSeeding>();
                    //logger.LogError(ex.Message);
                }
            }

        }

        //public static void Seed(IApplicationBuilder applicationBuilder)
        //{
        //    using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        //    {
        //        var context = serviceScope.ServiceProvider.GetService<ApplicationDBContext>();

        //        context.Database.EnsureCreated();

        //        try
        //        {
        //            if (!context.ProductBrands.Any())
        //            {
        //                var filePath = "~/Seedding/types.json";
        //                using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        //                using StreamReader streamReader = new(stream);
        //                using JsonTextReader reader = new(streamReader);

        //                while (reader.Read())
        //                {
        //                    if (reader.TokenType != JsonToken.PropertyName)
        //                        continue;

        //                    var key = reader.Value as string;
        //                    reader.Read();
        //                    JsonSerializer _serializer = new();
        //                    var value = _serializer.Deserialize<string>(reader);
        //                }
        //                context.SaveChanges();
        //            }

        //            if (!context.ProductTypes.Any())
        //            {
        //                var typeData = File.ReadAllText("~/Seedding/types.json");
        //                var type = JsonSerializer.Deserialize<List<ProductType>>(typeData);

        //                foreach (var item in type)
        //                {
        //                    context.ProductTypes.Add(item);

        //                }
        //                context.SaveChanges();
        //            }

        //            if (!context.products.Any())
        //            {
        //                var productData = File.ReadAllText("../EcommerceShop.EF/Seedding/products.json");
        //                var product = JsonSerializer.Deserialize<List<Product>>(productData);

        //                foreach (var item in product)
        //                {
        //                    context.products.Add(item);

        //                }
        //                context.SaveChanges();
        //            }



        //        }
        //        catch (Exception ex)
        //        {
        //            var logger = loggerFactory.CreateLogger<ApplicatioContextSeeding>();
        //            logger.LogError(ex.Message);
        //        }
        //    }

        //}

    }
}

