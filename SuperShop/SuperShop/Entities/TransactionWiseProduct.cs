using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Entities
{
    [Table("TransactionWiseProduct", Schema = "Operation")]
    public class TransactionWiseProduct
    {
        [Key]
        public int TrProductId { get; set; }
        public int TrId { get; set; }
        public int ProductCode { get; set; }
        public double PurchaseRate { get; set; }
        public float SellingRate { get; set; }
        public int Quantity { get; set; }
        public string IsDiscountOn { get; set; }
        public float DiscountParcent { get; set; }
        public float DiscountAmount { get; set; }
    }
}
