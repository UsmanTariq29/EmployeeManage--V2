using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblVoucherStatus
    {
        public byte VoucherStatusId { get; set; }
        public string VoucherStatusDescription { get; set; }
        public string VoucherStatusDescriptionFrench { get; set; }
        public string VoucherStatusDescriptionArabic { get; set; }
    }
}
