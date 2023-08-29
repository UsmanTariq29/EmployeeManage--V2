
namespace EmployeeManage.ViewModels.Responses
{
    public class EmployeeInfo : DepartmentInfo
    {
        public string EmployeeName { get; set; }
        public int EmployeeID { get; set; }
        public int CountryId { get; set; }
        public string BranchName { get; set; }
        public string EmployeeEmail { get; set; }
        public string PhotoPath { get; set; }
        public string CompanyName { get; set; }
        public string CountryName { get; set; }
        public string CompanyAddress { get; set; }
        public string Nationality { get; set; }
        public string Status { get; set; }
    }
}