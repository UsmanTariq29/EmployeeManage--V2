using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblBranch
    {
        public int BranchId { get; set; }
        public string BranchGuid { get; set; }
        public string BranchName { get; set; }
        public string BranchShortName { get; set; }
        public string BranchCode { get; set; }
        public int CompanyId { get; set; }
        public int CountryId { get; set; }
        public string CompanyGuid { get; set; }
    }
}
