﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Core.Model
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public ProductBrand ProductBrand { get; set; }

        public int ProductBrandId { get; set; }


        public ProductType ProductType { get; set; }

        public int ProductTypeId { get; set; }

    }
}
