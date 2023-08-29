using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EmployeeManage.ViewModels.Responses
{
    public class ListsResponseVM
    {
        public IEnumerable<SelectListItem> ItemGroupList { get; set; }
        public IEnumerable<SelectListItem> ItemList { get; set; }
        public IEnumerable<SelectListItem> taxList { get; set; }
        public IEnumerable<SelectListItem> SupplierList { get; set; }
    }
}