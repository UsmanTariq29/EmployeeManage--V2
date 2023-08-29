using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblAccountTypeGroup
    {
        public int AccountTypeGroupId { get; set; }
        public int AccountTypeId { get; set; }
        public string AccountTypeGroup { get; set; }
        public string AccountTypeGroupFrench { get; set; }
        public string AccountTypeGroupArabic { get; set; }
    }
}
