using System;

namespace EmployeeManage.ViewModels.Responses
{
    public class UnholdOrderResponse
    {
        public int HoldInvoiceMasterId { get; set; }
        public int InvoiceMasterId { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string ItemBarcode { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateCreated { get; set; }

        public string HoldOrderId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}