using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Models.Customer
{
    public class CustReigistration
    {
        public string fullname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string phonenumber { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public DateTime dateofbirth { get; set; }
        public string address { get; set; }
        public string additionalInformation { get; set; }
        public string district { get; set; }
        public bool isCreateAccount { get; set; }
    }
}
