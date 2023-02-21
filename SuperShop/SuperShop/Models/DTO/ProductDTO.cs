﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Models.DTO
{
    public class ProductDTO
    {
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public string CatCode { get; set; }
        public string SubCatCode { get; set; }
        public string BrandCode { get; set; }
        public string ItemCode { get; set; }
        public string QtyTypeCode { get; set; }
    }
}
