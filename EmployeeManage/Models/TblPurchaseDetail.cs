using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblPurchaseDetail
    {
        public int PurchaseDetailId { get; set; }
        public int PurchaseMasterId { get; set; }
        public int ItemGroupId { get; set; }
        public int ItemId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? FixedDiscount { get; set; }
        public decimal AmountAfterDiscount { get; set; }
        public int ItemTaxId { get; set; }
        public decimal Percentage { get; set; }
    }
}
