using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Entities
{
    [Table("ProductImage")]
    public class ProductImage
    {
        [Key]
        public string Location { get; set; }
        public int ProductId { get; set; }
        public string IsActive { get; set; }
        public string IsDelete { get; set; }
        public DateTime? UploadDate { get; set; }
    }
}
