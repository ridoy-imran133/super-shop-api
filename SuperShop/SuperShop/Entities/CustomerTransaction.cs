using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Entities
{
    [Table("CustomerTransaction", Schema = "Customer")]
    public class CustomerTransaction
    {
        [Key]
        public int CustTransactionCode { get; set; }
        public int CustCode { get; set; }
        public string PaymentMethod { get; set; }
        public string IsTransactionComplete { get; set; }
        public double TotalAmount { get; set; }
        public double PayAmount { get; set; }
        public double ShipmentCharge { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
    }
}
