using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblTax
    {
        public int TaxId { get; set; }
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public DateTime Date { get; set; }
        public string BranchGuid { get; set; }
        public string CompanyGuid { get; set; }
        public string CreatedByUserGuid { get; set; }
    }
}
