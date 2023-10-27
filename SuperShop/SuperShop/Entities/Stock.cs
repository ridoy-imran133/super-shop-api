using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Entities
{
    [Table("RealStock", Schema = "Operation")]
    public class Stock
    {
        [Key]
        public int SId { get; set; }
        public int ProductCode { get; set; }
        public string OutletCode { get; set; }
        public decimal StockAvailable { get; set; }
    }
}
