using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Models
{
    public class DBResponseMessage
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public Object ResponseData { get; set; }
    }
}
