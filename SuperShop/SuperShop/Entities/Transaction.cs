using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Entities
{
    [Table("Transaction", Schema = "Operation")]
    public class Transaction
    {
        [Key]
        public int TrId { get; set; }
        public DateTime Date { get; set; }
        public float DiscountParcent { get; set; }
        public float FlatDiscount { get; set; }
        public float DiscountAmount { get; set; }
        public float GrandTotal { get; set; }
        public string PaymentType { get; set; }
        public float CollectedAmount { get; set; }
        public float ChangeAmount { get; set; }
        public string CustomerId { get; set; }
        public string UserId { get; set; }
        public string OutletCode { get; set; }
        //public List<TransactionWiseProduct> _products { get; set; }

    }
}
