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
        public DateTime Date { get; set; }
        public double Quantity { get; set; }
        public string QuantityType { get; set; }
        public double Amount { get; set; }
        public string PaymentType { get; set; }
        public string CustomerId { get; set; }
        public string IsActive { get; set; }
        public string IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
