using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Entities
{
    [Table("CustomerMenu", Schema = "Customer")]
    public class CustomerMenu
    {
        [Key]
        public string MenuCode { get; set; }
        public string MenuName { get; set; }
        public string URL { get; set; }
        public string Icon { get; set; }
        public string Sequence { get; set; }
        public string IsActive { get; set; }
        public string IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
