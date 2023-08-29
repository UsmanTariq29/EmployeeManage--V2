using EmployeeManage.ViewModels.Request;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository.Interface
{
    public interface IPromotionRepo
    {
        Task CreatePromotion(PromotionRequestVM model, DataTable promotionItems, string userGUID, string branchGUID, string companyGUID,
            CancellationToken token = default);
        Task<IEnumerable<PromotionItemsResponse>> GetItems(int id, CancellationToken token = default);
        Task<IEnumerable<SelectListItem>> PromotionTypeList(CancellationToken token = default);

    }
}
