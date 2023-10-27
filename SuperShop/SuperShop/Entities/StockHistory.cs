using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Entities
{
    [Table("StockHistory", Schema = "Operation")]
    public class StockHistory
    {
        [Key]
        public int SHId { get; set; }
        public int ProductCode { get; set; }
        public string OutletCode { get; set; }
        public decimal StockIn { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
