using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Models.Report
{
    public class SalesReport
    {
        public int ProductCode { get; set; }
        public String ProductName { get; set; }
        public double Quantity { get; set; }
        public String SaleDate { get; set; }

    }
}
