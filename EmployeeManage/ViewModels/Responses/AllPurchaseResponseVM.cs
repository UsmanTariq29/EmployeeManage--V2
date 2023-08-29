
using System;

namespace EmployeeManage.ViewModels.Responses
{
    public class AllPurchaseResponseVM
    {
        public int ItemGroupId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string DetailDescription { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? FixedDiscount { get; set; }
        public decimal AmountAfterDiscount { get; set; }
        public int ItemTaxId { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string InvoiceNo { get; set; }
        public string MasterDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public int PurchaseMasterId { get; set; }

    }
}