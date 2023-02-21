using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Models.DTO
{
    public class StockHistoryDTO
    {
        public DateTime Date { get; set; }
        public string SId { get; set; }
        public double DailyStock { get; set; }
        public double StockIn { get; set; }
        public string QuantityType { get; set; }
    }
}
