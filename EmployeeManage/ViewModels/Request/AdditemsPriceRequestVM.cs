using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EmployeeManage.ViewModels.Request
{
    public class AdditemsPriceRequestVM
    {
        public int PriceId { get; set; }
        public int ItemId { get; set; }

        public decimal ItemsPrice { get; set; }

        public IEnumerable<SelectListItem> ItemsList { get; set; }
    }
}
