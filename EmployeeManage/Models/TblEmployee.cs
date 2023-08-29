using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblEmployee
    {
        public TblEmployee()
        {
            TblEmployeeAllowances = new HashSet<TblEmployeeAllowance>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public decimal GrossSalary { get; set; }
        public int DepartmentId { get; set; }
        public int NationalityId { get; set; }
        public int BranchId { get; set; }
        public string PhotoPath { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime JoinDate { get; set; }

        public virtual ICollection<TblEmployeeAllowance> TblEmployeeAllowances { get; set; }
    }
}
