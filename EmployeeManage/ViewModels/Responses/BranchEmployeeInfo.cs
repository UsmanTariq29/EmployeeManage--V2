

namespace EmployeeManage.ViewModels.Responses
{
    public class BranchEmployeeInfo
    {
        public int EmployeeId { get; set; }
        public int BranchId { get; set; }
        public string EmployeeName { get; set; }
        //public string CompanyName { get; set; }
        public int CompanyID{ get; set; }
        public string BranchName { get; set; }
        public int Presents { get; set; }
        public int Absents { get; set; }
        //public string CompanyAddress { get; set; }
        //public DateTime? AttandanceDate { get; set; }
    }
}
