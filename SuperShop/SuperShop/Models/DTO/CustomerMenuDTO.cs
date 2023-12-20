using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Models.DTO
{
    public class CustomerMenuDTO
    {
        public string MenuName { get; set; }
        public string URL { get; set; }
        public string Icon { get; set; }
    }
}
