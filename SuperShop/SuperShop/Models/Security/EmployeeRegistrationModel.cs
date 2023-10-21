using SuperShop.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Models
{
    public class EmployeeRegistrationModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ECName { get; set; }
        public string ECPhoneNumber { get; set; }
        public string MaritalStatus { get; set; }
        public bool HasSystemAccess { get; set; }
        public string Token { get; set; }
        public string RoleCode { get; set; }
        public string ProjectCode { get; set; }
    }
}
