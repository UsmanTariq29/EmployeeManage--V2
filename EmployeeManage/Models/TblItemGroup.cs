using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblItemGroup
    {
        public int ItemGroupId { get; set; }
        public string ItemGroupName { get; set; }
        public DateTime ItemCreatedDateTime { get; set; }
        public string UserGuId { get; set; }
        public string CompanyGuid { get; set; }
    }
}
