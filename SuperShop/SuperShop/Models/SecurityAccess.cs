using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Models
{
    public class SecurityAccess
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public List<Menu> responseData { get; set; }
    }
}
