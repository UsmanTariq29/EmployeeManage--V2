using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblCompany
    {
        public int CompanyId { get; set; }
        public string CompanyGuid { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
    }
}
