using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManage.ViewModels.Request
{
    public class PromotionRequestVM
    {
        public string PromotionName { get; set; }
        public DateTime PromotionStartDate { get; set; }
        public DateTime PromotionEndDate { get; set; }
        public string Description { get; set; }

        public decimal PromotionPercentage { get; set; }

        public int PromotionTypeId { get; set; }



    }
    public class AddPromotionRequestVM : PromotionRequestVM
    {
        public int ItemGroupId { get; set; }

        public IEnumerable<SelectListItem> PromotionTypeList { get; set; }
        public IEnumerable<SelectListItem> ItemGroupList { get; set; }

    }
}
