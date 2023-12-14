using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Models.DTO
{
    public class StockDTO
    {
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public string OutletCode { get; set; }
        public string CatName { get; set; }
        public decimal StockAvailable { get; set; }
        public decimal StockIn { get; set; }
    }
}
