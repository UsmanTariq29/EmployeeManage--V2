
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace EmployeeManage.ViewModels
{
    public class EmployeeCreateVM
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Name Cannot be Exceeded from 50")]
        public String EmployeeName { get; set; }
        [Required]
        public String EmployeeEmail { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public IFormFile photo { get; set; }

        public decimal grossSalary { get; set; }
        public int NationalityId { get; set; }
        public int branchId { get; set; }
        public string photoPath { get; set; }

        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        public IEnumerable<SelectListItem> NationalityList { get; set; }
        public IEnumerable<SelectListItem> BranchList { get; set; }

    }

    public class EmployeeUpdateVM : EmployeeCreateVM
    {
        public int EmployeeID { get; set; }
    }

    public class EmployeeDocument
    {
        public int DepartmentId { get; set; }
        public int EmployeeID { get; set; }
        public IEnumerable<SelectListItem> EmployeeActiveList { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        public IEnumerable<SelectListItem> DocumentList { get; set; }
        public int DocumentId { get; set; }
        public IFormFile DocumentPath { get; set; }
        public string Remarks { get; set; }
    }
}
