using EmployeeManage.Models;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository.Interface
{
    public interface IDepartmentRepo
    {
        Task<TblDepartment> GetDeptartment(int id, CancellationToken token = default);
        Task<IEnumerable<SelectListItem>> GetDeptartmentList(CancellationToken token = default);
        Task<IEnumerable<SelectListItem>> GetBranchList(CancellationToken token = default);
        Task<IEnumerable<SelectListItem>> GetNationalityList(CancellationToken token = default);
        Task<IEnumerable<EmployeeNetSalaryInfo>> GetNetSalaryOfEmoployee(CancellationToken token = default);
        Task<IEnumerable<BranchEmployeeInfo>> GETBranchNameEmployee(int id, CancellationToken token = default);
    }
}
