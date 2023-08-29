

namespace EmployeeManage.ViewModels.Request
{
    public class purchaseRequest
    {
        public int PurchaseMasterId { get; set; }
        public int SupplierId { get; set; }
        public string InvoiceNo { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal NetAmount { get; set; }
        public int Tax { get; set; }
        public int StatusId { get; set; }
    }
    public class PurchaseDetailVM : purchaseRequest

    //detail
    { 
        public int PurchaseDetailId { get; set; }
        public int ItemGroupId { get; set; }
        public int ItemId { get; set; }
        public string ItemDetailDescription { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal ItemAmount { get; set; }
        public decimal? ItemDiscountPercentage { get; set; }
        public decimal? ItemFixedDiscount { get; set; }
        public decimal ItemAmountAfterDiscount { get; set; }
        public int ItemTaxId { get; set; }
        public decimal ItemPercentage { get; set; }

    }

}
