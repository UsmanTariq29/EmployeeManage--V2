
namespace EmployeeManage.ViewModels.Request
{
    public class AddSupplierRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string CNIC { get; set; }
        public bool IsActive { get; set; }
        public string CreatedByUserGuid { get; set; }
        public string BranchGuid { get; set; }
        public string CompanyGuid { get; set; }
        public int? DetailCodeId { get; set; }
        public int? SubCodeId { get; set; }

    }
}
