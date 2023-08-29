using EmployeeManage.ViewModels.Request;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository.Interface
{
    public interface ISupplierRepo
    {
        Task CreateSupplier(AddSupplierRequest model, string userGUID, string branchGUID, string companyGUID,
            CancellationToken token = default);
        Task<IEnumerable<SelectListItem>> SuppliersList(CancellationToken token = default);
    }
}