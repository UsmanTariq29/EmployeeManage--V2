using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblCustomer
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Ntnnumber { get; set; }
        public string Cnicnumber { get; set; }
        public string CreatedByUserGuid { get; set; }
        public string BranchGuid { get; set; }
        public string CompanyGuid { get; set; }
    }
}
