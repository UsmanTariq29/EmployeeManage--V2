using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblAcccountMaincode
    {
        public int Id { get; set; }
        public int MainCodeId { get; set; }
        public string MainCodeName { get; set; }
        public string MainCodeNameArabic { get; set; }
        public string MainCodeNameFrench { get; set; }
        public int AccountTypeId { get; set; }
        public int? AccountTypeGroupId { get; set; }
        public bool IsSystemAccount { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedByUserGuid { get; set; }
        public bool IsSuspended { get; set; }
        public string CompanyGuid { get; set; }
    }
}
