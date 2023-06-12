using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Entities
{
    public class TransactionWiseProduct
    {
        public string TransactionId { get; set; }
        public string ProductCode { get; set; }
        public string Quantity { get; set; }
        public string QtyTypeCode { get; set; }
        public double ProductAmount { get; set; }
        public double Amount { get; set; }
        public string Vat { get; set; }
        public string Tax { get; set; }
    }
}
