using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EmployeeManage.Models
{
    [Keyless]
    [Table("TBL_LOG")]
    [Index(nameof(BranchGuid), Name = "IX_TBL_LOG_BRANCH")]
    [Index(nameof(CompanyGuid), Name = "IX_TBL_LOG_COMPANY")]
    [Index(nameof(TimeStamp), Name = "IX_TBL_LOG_TIME_STAMP")]
    [Index(nameof(Username), Name = "IX_TBL_LOG_USERNAME")]
    [Index(nameof(UserGuid), Name = "IX_TBL_LOG_USER_GUID")]
    public partial class TblLog
    {
        [Column(TypeName = "datetime")]
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        [StringLength(12)]
        public string Level { get; set; }
        public string Exception { get; set; }
        public string LogEvent { get; set; }
        [StringLength(1000)]
        public string Source { get; set; }
        [Column("RequestIP")]
        [StringLength(20)]
        public string RequestIp { get; set; }
        [StringLength(1000)]
        public string RequestUserAgent { get; set; }
        [Column("UserGUID")]
        [StringLength(50)]
        public string UserGuid { get; set; }
        [StringLength(50)]
        public string Username { get; set; }
        [Column("BranchGUID")]
        [StringLength(50)]
        public string BranchGuid { get; set; }
        [Column("CompanyGUID")]
        [StringLength(50)]
        public string CompanyGuid { get; set; }
    }
}
