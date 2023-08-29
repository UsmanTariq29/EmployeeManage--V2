using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblEmployeeDocument
    {
        public int EmployeeDocumentId { get; set; }
        public int DepartmentId { get; set; }
        public int EmployeeId { get; set; }
        public int DocumentCategoryId { get; set; }
        public string DocumentPath { get; set; }
        public string Remarks { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool DocumentWithExpiry { get; set; }
    }
}
