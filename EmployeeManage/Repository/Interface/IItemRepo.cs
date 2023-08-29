using EmployeeManage.Models;
using EmployeeManage.ViewModels.Request;
using System.Threading.Tasks;
using System.Threading;
using EmployeeManage.ViewModels.Responses;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManage.Repository.Interface
{
    public interface IItemRepo
    {
        Task<TblItem> GetItem(int id, CancellationToken token = default);
        Task<TblItemPrice> GetItemprice(int id, CancellationToken token = default);
        Task AddItemsPrice(AdditemsPriceRequestVM model, string UserGUID, string CompanyGUID, CancellationToken token = default);
        Task AddItem(ItemsRequest model, string usierid, string companyid, CancellationToken token = default);
        Task<IEnumerable<ItemsDataListsResponseVM>> AllItemData(CancellationToken token = default);
        Task UpdateItemData(UpdateItemRequestVm model, CancellationToken token = default);
        public Task<IEnumerable<SelectListItem>> ItemsList(CancellationToken token = default);
    }
}
