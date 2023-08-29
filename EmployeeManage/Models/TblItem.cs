using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblItem
    {
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public bool IsExempted { get; set; }
        public bool IsActive { get; set; }
        public DateTime ItemDateTime { get; set; }
        public string UserGuId { get; set; }
        public string CompanyGuid { get; set; }
        public int ItemReOrder { get; set; }
        public int ItemGroupId { get; set; }
        public bool IsBatchItem { get; set; }
        public bool IsRepalaceable { get; set; }
        public bool IsExpiryAllowed { get; set; }
        public int UnitInCase { get; set; }
        public int? TaxId { get; set; }
        public decimal? Discount { get; set; }
    }
}
