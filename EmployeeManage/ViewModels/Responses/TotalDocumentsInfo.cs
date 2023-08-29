
namespace EmployeeManage.ViewModels.Responses
{
    public class TotalDocumentsInfo
    {
        public int EmployeeId { get; set; }
        //  public int DepartmentId { get; set; }
        public int? DocumentId { get; set; }
        public string EmployeeName { get; set; }
        public string DocumentName { get; set; }
        public int DocumentCount { get; set; }
    }
}