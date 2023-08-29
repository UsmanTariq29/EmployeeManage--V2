using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblAccountSubcode
    {
        public int Id { get; set; }
        public int SubCodeId { get; set; }
        public int DetailCodeId { get; set; }
        public string SubCodeName { get; set; }
        public string SubCodeNameFrench { get; set; }
        public string SubCodeNameArabic { get; set; }
        public int? CurrencyId { get; set; }
        public string PhysicalAccountName { get; set; }
        public string PhysicalAccountNumber { get; set; }
        public int? PhysicalAccountTypeId { get; set; }
        public string PhysicalAccountSwiftCode { get; set; }
        public string PhysicalAccountIbannumber { get; set; }
        public string CryptocurrencyPublicKey { get; set; }
        public string WalletNumber { get; set; }
        public bool IsSystemAccount { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedByUserGuid { get; set; }
        public bool IsSuspended { get; set; }
        public string CompanyGuid { get; set; }
    }
}
