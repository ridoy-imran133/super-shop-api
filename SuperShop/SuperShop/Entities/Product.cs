﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public string CatCode { get; set; }
        public string SubCatCode { get; set; }
        public string BrandCode { get; set; }
        //public string ItemCode { get; set; }
        public string QtyTypeCode { get; set; }
        public double PurchaseRate { get; set; }
        public string DiscountType { get; set; } = "N" ;
        public double? DiscountAmount { get; set; }
        public double SellingRate { get; set; }
        public string MenuName { get; set; }
        public string IsActive { get; set; }
        public string IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
