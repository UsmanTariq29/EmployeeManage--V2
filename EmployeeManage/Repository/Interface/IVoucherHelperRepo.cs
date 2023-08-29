using System.Threading.Tasks;
using System.Threading;
using EmployeeManage.ViewModels.Responses;
using EmployeeManage.ViewModels.Request;
using System.Data;

namespace EmployeeManage.Repository.Interface
{
    public interface IVoucherHelperRepo
    {
        Task<VoucherDisplayNumberResponseVM> CreateVoucherDisplayNumber(int voucherTypeId,
        string voucherTypeShortName, string branchGUID, string companyGUID, CancellationToken token = default);

        Task CreateVoucherMaster(VoucherRequestVM model, DataTable VoucherDetail, string userGUID, string branchGUID, string companyGUID, CancellationToken token = default);

    }
}
