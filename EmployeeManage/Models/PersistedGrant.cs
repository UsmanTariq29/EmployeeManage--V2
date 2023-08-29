using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EmployeeManage.Models
{
    [Index(nameof(Expiration), Name = "IX_PersistedGrants_Expiration")]
    [Index(nameof(SubjectId), nameof(ClientId), nameof(Type), Name = "IX_PersistedGrants_SubjectId_ClientId_Type")]
    [Index(nameof(SubjectId), nameof(SessionId), nameof(Type), Name = "IX_PersistedGrants_SubjectId_SessionId_Type")]
    public partial class PersistedGrant
    {
        [Key]
        [StringLength(200)]
        public string Key { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        [StringLength(200)]
        public string SubjectId { get; set; }
        [StringLength(100)]
        public string SessionId { get; set; }
        [Required]
        [StringLength(200)]
        public string ClientId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? Expiration { get; set; }
        public DateTime? ConsumedTime { get; set; }
        [Required]
        public string Data { get; set; }
    }
}
