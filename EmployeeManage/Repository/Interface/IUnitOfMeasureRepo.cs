using EmployeeManage.ViewModels.Request;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EmployeeManage.Repository.Interface
{
    public interface IUnitOfMeasureRepo
    {
        Task AddUnitOfMeasure(UnitOfMeasureRequest model, CancellationToken token = default);
        public Task<IEnumerable<SelectListItem>> UnitInCaseList(CancellationToken token = default);
    }
}