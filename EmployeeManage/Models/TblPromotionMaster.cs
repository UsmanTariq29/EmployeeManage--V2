using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblPromotionMaster
    {
        public int PromotionId { get; set; }
        public string PromotionName { get; set; }
        public DateTime PromotionStartDate { get; set; }
        public DateTime PromotionEndDate { get; set; }
        public string PromotionDescription { get; set; }
        public int PromotionType { get; set; }
        public decimal PromotionPercentage { get; set; }
        public string CreatedByUserGuid { get; set; }
        public string BranchGuid { get; set; }
        public string CompanyGuid { get; set; }
    }
}
