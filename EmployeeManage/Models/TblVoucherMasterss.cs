using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblVoucherMasterss
    {
        public int VoucherMasterId { get; set; }
        public int InvoiceNo { get; set; }
        public string ReferenceVoucherNo { get; set; }
        public int VoucherTypeId { get; set; }
        public string CreatedByUserGuid { get; set; }
        public string BranchGuid { get; set; }
        public string CompanyGuid { get; set; }
    }
}
