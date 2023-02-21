using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Entities
{
    [Table("QtyType")]
    public class QtyType
    {
        [Key]
        public string QtyTypeCode { get; set; }
        public string QtyTypeName { get; set; }
        public string IsActive { get; set; }
        public string IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
