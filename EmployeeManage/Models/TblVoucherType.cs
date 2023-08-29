using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblVoucherType
    {
        public int VoucherTypeId { get; set; }
        public string VoucherType { get; set; }
        public string VoucherDescription { get; set; }
    }
}
