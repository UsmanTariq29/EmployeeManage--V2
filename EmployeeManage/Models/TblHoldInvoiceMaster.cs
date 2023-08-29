using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblHoldInvoiceMaster
    {
        public int HoldOrderMasterId { get; set; }
        public string OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal TotalAmount { get; set; }
        public string CreatedByUserGuid { get; set; }
        public string BranchGuid { get; set; }
        public string CompanyGuid { get; set; }
    }
}
