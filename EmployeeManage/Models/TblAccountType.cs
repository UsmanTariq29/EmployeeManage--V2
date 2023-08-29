using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblAccountType
    {
        public int AccountTypeId { get; set; }
        public string AccountName { get; set; }
        public string AccountNameArabic { get; set; }
        public string AccountNameFrench { get; set; }
        public string AccountDescription { get; set; }
        public string AccountDescriptionArabic { get; set; }
        public string AccountDescriptionFrench { get; set; }
    }
}
