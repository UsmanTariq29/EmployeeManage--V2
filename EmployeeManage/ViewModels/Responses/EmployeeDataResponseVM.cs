using System;
using System.Collections.Generic;

namespace EmployeeManage.ViewModels.Responses
{
    public class EmployeeBirthdayAndNewHires
    {

        public List<EmployeeDataResponseVM> employeedata { get; set; }
        public List<EmployeeHires> employeeHires { get; set; }
        public List<ExpiredDocuments> employeeExpireDoc { get; set; }
        public List<ActiveEmployee> activeEmployees { get; set; }

    }

    public class EmployeeDataResponseVM
    {
        public string EmolyeeName { get; set; }
        public string DepartmentName { get; set; }
        public string BranchName { get; set; }
        //  public DateTime? BirthDate { get; set; }
        public DateTime JoinDate { get; set; }
        // public bool DocumentWithExpiry { get; set; }
    }

    public class EmployeeHires
    {
        public string EmolyeeName { get; set; }
        public string DepartmentName { get; set; }
        public string BranchName { get; set; }
        //  public DateTime? BirthDate { get; set; }
        public DateTime JoinDate { get; set; }
        // public bool DocumentWithExpiry { get; set; }
    }

    public class ExpiredDocuments
    {
        public string EmolyeeName { get; set; }
        public string DepartmentName { get; set; }
        public string BranchName { get; set; }
        public string DocumentName { get; set; }
        public int DocumentId { get; set; }
        public string ExpireDocument { get; set; }
        public int Expiredays { get; set; }
    }
    public class ActiveEmployee
    {
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string DepartmentName { get; set; }
        public string BranchName { get; set; }
    }
}
