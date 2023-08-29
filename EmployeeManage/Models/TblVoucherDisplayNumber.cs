using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblVoucherDisplayNumber
    {
        public int VoucherTypeId { get; set; }
        public string BranchGuid { get; set; }
        public int VoucherDisplayNumber { get; set; }
        public string CompanyGuid { get; set; }
    }
}
