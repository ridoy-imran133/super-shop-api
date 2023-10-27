using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Models.DTO
{
    public class TransactionWiseProductDTO
    {
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal StockAvailable { get; set; }
        public float Quantity { get; set; }
        public double Amount { get; set; }
        public double SellingRate { get; set; }
        public float PurchaseRate { get; set; }
        public string IsDiscountOn { get; set; }
        public float DiscountParcent { get; set; }
        public float DiscountAmount { get; set; }
    }
}
