using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EmployeeManage.ViewModels.Request
{
    public class ItemsRequest
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string TaxName { get; set; }
        public bool IsExempted { get; set; }
        public bool IsActive { get; set; }
        public int ItemReorder { get; set; }
        public int ItemGroupId { get; set; }
        public bool IsBatchItem { get; set; }
        public bool IsReplaceable { get; set; }
        public bool IsExpiryAllowed { get; set; }
        public int UnitInCase { get; set; }
        public int? TaxId { get; set; }

        public IEnumerable<SelectListItem> GroupList { get; set; }
        public IEnumerable<SelectListItem> UnitInCaseList { get; set; }
        public IEnumerable<SelectListItem> TaxList { get; set; }
    }
}
