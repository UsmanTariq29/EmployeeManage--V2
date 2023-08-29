using EmployeeManage.Models;
using EmployeeManage.ViewModels.Request;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository.Interface
{
    public interface IItemGroupRepo
    {

        Task AddItemGroup(Item_GroupRequest model, string usierid, string companyid, CancellationToken token = default);
        public Task<IEnumerable<SelectListItem>> GroupList(CancellationToken token = default);

    }
}
