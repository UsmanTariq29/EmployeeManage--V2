using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class VwEmployeeDatum
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeEmail { get; set; }
        public string PhotoPath { get; set; }
        public string IsActive { get; set; }
    }
}
