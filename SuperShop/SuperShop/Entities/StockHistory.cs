using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Entities
{
    [Table("StockHistory")]
    public class StockHistory
    {
        public DateTime Date { get; set; }
        public string SId { get; set; }
        public double DailyStock { get; set; }
        public double StockIn { get; set; }
        public string QuantityType { get; set; }
        public string IsActive { get; set; }
        public string IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
