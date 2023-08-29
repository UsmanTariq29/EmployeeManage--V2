using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblHoldInvoiceDetail
    {
        public int HoldInvoiceDetailId { get; set; }
        public int HoldInvoiceMasterId { get; set; }
        public int ItemId { get; set; }
        public decimal ItemQuantity { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal Amount { get; set; }
    }
}
