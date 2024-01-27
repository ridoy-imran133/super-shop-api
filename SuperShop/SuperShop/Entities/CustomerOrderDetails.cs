using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Entities
{
    [Table("OrderDetails", Schema = "Customer")]
    public class CustomerOrderDetails
    {
        [Key]
        public int Id { get; set; }
        public int CustTransactionCode { get; set; }
        public int ProductCode { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public string HaveDiscount { get; set; }
        public string DiscountType { get; set; }
        public double DiscountAmount { get; set; }
    }
}
