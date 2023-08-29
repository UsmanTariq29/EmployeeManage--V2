using EmployeeManage.Model;
using EmployeeManage.Models;
using EmployeeManage.ViewModels;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Model
{
    public interface IEmployeeRepo
    {
        Task<TblEmployee> ValidateRecord(int Id, CancellationToken token = default);
        Task<TblEmployee> GetEmployee(int Id, CancellationToken token = default);
        public Task<EmployeeInfo> GetEmployeeDetails(int Id, CancellationToken token = default);
        Task<IEnumerable<EmployeeInfo>> SelectEmployeeInfo(CancellationToken token = default);
        //  public Task<IEnumerable<VwEmployeeDatum>> GetEmployeeData(CancellationToken token = default);
        public Task<IEnumerable<EmployeesDocumentInfo>> GetRemarkPath(int id, CancellationToken token = default);
        void Create(EmployeeCreateVM model);
        IEnumerable<SelectListItem> GetEmployeeList();
        public IEnumerable<SelectListItem> GetEmployeeListAct(int id);
        public Task updateAsync(EmployeeUpdateVM employeeChanges, CancellationToken token = default);
        public Task<IEnumerable<CompanyInfo>> GetEmployeeCompnyInfo(CancellationToken token = default);
        public Task DeleteAsync(int id, CancellationToken token = default);
    }
}
