using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Models.Security
{
    public class RoleModel
    {
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public string ProjectCode { get; set; }
        public bool? IsChecked { get; set; } = false ;
    }
}
