using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblPromotionDetail
    {
        public int PromotionDetailId { get; set; }
        public int PromotionMasterId { get; set; }
        public int ItemId { get; set; }
        public decimal PromotionPercentage { get; set; }
    }
}
