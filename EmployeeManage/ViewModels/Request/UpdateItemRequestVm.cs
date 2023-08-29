

namespace EmployeeManage.ViewModels.Request
{
    public class UpdateItemRequestVm
    {
        public int ItemId { get; set; }
        public int ItemGroupId { get; set; }
        public decimal ItemsPrice { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public bool IsExempted { get; set; }
        public bool IsActive { get; set; }
        public string UserGuId { get; set; }
        public string CompanyGuid { get; set; }
        public int ItemReOrder { get; set; }
        public bool IsBatchItem { get; set; }
        public bool IsRepalaceable { get; set; }
        public bool IsExpiryAllowed { get; set; }
        public int UnitInCase { get; set; }
        public int? TaxId { get; set; }
    }
}
