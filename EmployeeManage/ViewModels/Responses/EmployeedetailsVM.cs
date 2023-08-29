
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;

namespace EmployeeManage.ViewModels.Responses
{
    public class EmployeedetailsVM
    {
        public string EmployeeName { get; set; }
        public int EmployeeID { get; set; }
        public int DepartmentId { get; set; }
        public string EmployeeEmail { get; set; }
        public decimal grossSalary { get; set; }
        public int NationalityId { get; set; }
        public int branchId { get; set; }
        public string photoPath { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        public IEnumerable<SelectListItem> NationalityList { get; set; }
        public IEnumerable<SelectListItem> BranchList { get; set; }

    }
}