using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Entities
{
    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        public string TrId { get; set; }
        public string StockId { get; set; }
        public string OutletId { get; set; }
        public DateTime Date { get; set; }
        public double Quantity { get; set; }
        public string QuantityType { get; set; }
        public double TotalAmount { get; set; }
        public double TotalTax { get; set; }
        public double TotalVat { get; set; }
        public int ProductCount { get; set; }
        public string PaymentType { get; set; }
        public string CustomerId { get; set; }
        public string UserId { get; set; }
        public double PayAmount { get; set; }
        public double ReturnAmount { get; set; }
        public double PaidAmount { get; set; }
        public List<TransactionWiseProduct> products { get; set; }
    }
}
