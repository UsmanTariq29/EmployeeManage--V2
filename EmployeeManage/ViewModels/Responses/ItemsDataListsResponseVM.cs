using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EmployeeManage.ViewModels.Responses
{
    public class ItemsDataListsResponseVM
    {
        public int PriceId { get; set; }
        public int ItemId { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public bool IsExempted { get; set; }
        public bool IsActive { get; set; }
        public int ItemReOrder { get; set; }
        public bool IsBatchItem { get; set; }
        public bool IsRepalaceable { get; set; }
        public bool IsExpiryAllowed { get; set; }
        public int UnitInCase { get; set; }
        public int? TaxId { get; set; }
        public int ItemGroupId { get; set; }
        public decimal ItemsPrice { get; set; }
        public string unitOfMeasure { get; set; }
        public int unitOfMeasureId { get; set; }
        public string UnitOfMeasureDescription { get; set; }
        public IEnumerable<SelectListItem> ItemsList { get; set; }
        public IEnumerable<SelectListItem> UOMList { get; set; }
        public IEnumerable<SelectListItem> taxList { get; set; }
        public IEnumerable<SelectListItem> GroupList { get; set; }
    }
}