using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Models.DTO
{
    public class OutletWiseProductDTO
    {
        public string OutletCode { get; set; }
        public string CatCode { get; set; }
        public string CatName { get; set; }
        public string SubCatCode { get; set; }
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public bool IsAvailable { get; set; }
    }
}
