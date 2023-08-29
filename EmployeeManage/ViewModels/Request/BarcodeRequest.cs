using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace EmployeeManage.ViewModels.Request
{
    public class BarcodeRequest
    {

        public int ItemId { get; set; }
        public string GeneratedBarcodeId { get; set; }
        public IEnumerable<SelectListItem> ItemsList { get; set; }
    }
}