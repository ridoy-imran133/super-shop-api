using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Models.DTO
{
    public class StockDTO
    {
        public string SId { get; set; }
        public string ProductCode { get; set; }
        public string OutletCode { get; set; }
        public string Quantity { get; set; }
        public string QtyTypeCode { get; set; }
    }
}
