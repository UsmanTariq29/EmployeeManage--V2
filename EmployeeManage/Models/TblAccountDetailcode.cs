using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblAccountDetailcode
    {
        public int Id { get; set; }
        public int MainCodeId { get; set; }
        public int DetailCodeId { get; set; }
        public string DetailCodeName { get; set; }
        public string DetailCodeNameArabic { get; set; }
        public string DetailCodeNameFrench { get; set; }
        public bool IsSystemAccount { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedByUserGuid { get; set; }
        public bool IsSuspended { get; set; }
        public string CompanyGuid { get; set; }
    }
}
