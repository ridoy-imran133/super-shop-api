using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Models.DTO
{
    public class TransactionDTO
    {
        public string TrId { get; set; }
        public string StockId { get; set; }
        public DateTime Date { get; set; }
        public double Quantity { get; set; }
        public string QuantityType { get; set; }
        public double Amount { get; set; }
        public string PaymentType { get; set; }
        public string CustomerId { get; set; }
    }
}
