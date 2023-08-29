using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblPurchaseMaster
    {
        public int PurchaseMasterId { get; set; }
        public int SupplierId { get; set; }
        public string InvoiceNo { get; set; }
        public int? StatusId { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal NetAmount { get; set; }
        public string CreatedByUserGuid { get; set; }
        public string BranchGuid { get; set; }
        public string CompanyGuid { get; set; }
        public string VoucherDisplayNo { get; set; }
    }
}
