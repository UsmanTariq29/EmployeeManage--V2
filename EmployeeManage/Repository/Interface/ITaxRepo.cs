using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using EmployeeManage.ViewModels.Request;
using EmployeeManage.ViewModels.Responses;

namespace EmployeeManage.Repository.Interface
{
    public interface ITaxRepo
    {
        Task CreateTaxAsync(AddTaxRequestVM model, string userGUID, string companyGUID,
            string branchGUID, CancellationToken token = default);
        Task<IEnumerable<SelectListItem>> GetTaxListAsync(CancellationToken token = default);

        Task<IEnumerable<TaxResponseVM>> GetItemsTaxListAsync(int id, CancellationToken token);
        Task<TaxResponseVM> GetTaxByIdAsync(int id, CancellationToken token);
    }
}