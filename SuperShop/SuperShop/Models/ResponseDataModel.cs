using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Models
{
    public class ResponseDataModel
    {
        public string token { get; set; }
        public string expireIn { get; set; }
        public string userName { get; set; }
    }
}
