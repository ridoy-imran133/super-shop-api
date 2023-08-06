﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Entities
{
    public class TransactionWiseProduct
    {
        public string TrId { get; set; }
        public int ProductCode { get; set; }
        public float PurchaseRate { get; set; }
        public float SellingRate { get; set; }
        public float Quantity { get; set; }
        public string IsDiscountOn { get; set; }
        public float DiscountParcent { get; set; }
        public float DiscountAmount { get; set; }
    }
}
