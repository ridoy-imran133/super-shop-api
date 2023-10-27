using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Entities
{
    [Table("OutletWiseProduct", Schema = "Operation")]
    public class OutletWiseProduct
    {
        [Key]
        public int Id { get; set; }
        public string OutletCode { get; set; }
        public string CatCode { get; set; }
        public string SubCatCode { get; set; }
        public int ProductCode { get; set; }
        public string IsActive { get; set; }
        public string IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
