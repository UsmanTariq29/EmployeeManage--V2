using EmployeeManage.Models;
using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository
{
    public class DepartmentRepo : IDepartmentRepo 
    {
        private readonly EmployeesDBContext _Db;

        public DepartmentRepo(EmployeesDBContext context)
        {
            this._Db = context;
        }
        public async Task<TblDepartment> GetDeptartment(int id, CancellationToken token = default)
        {
            return await _Db.TblDepartments
                .Where(a=>a.DepartmentId.Equals(id)).FirstOrDefaultAsync(token);
        }
        public async Task<IEnumerable<SelectListItem>> GetDeptartmentList(CancellationToken token = default)
        {
            var data = await _Db.TblDepartments.AsNoTracking().ToListAsync(token);

            List<SelectListItem> departments = data
                .OrderBy(n => n.DepartmentName)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.DepartmentId.ToString(),
                        Text = n.DepartmentName
                    }).ToList();
            var deparmenttip = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Department"
            };
            departments.Insert(0, deparmenttip);
            return  new SelectList(departments, "Value", "Text");

        }
        public async Task<IEnumerable<SelectListItem>> GetBranchList(CancellationToken token = default)
        {
            var data = await _Db.TblBranches.AsNoTracking().ToListAsync(token);
            List<SelectListItem> branchs = data
                .OrderBy(n => n.BranchName)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.BranchId.ToString(),
                        Text = n.BranchName
                    }).ToList();
            var branchip = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Branch"
            };
            branchs.Insert(0, branchip);
            return new SelectList(branchs, "Value", "Text");

        }
    public async Task<IEnumerable<SelectListItem>> GetNationalityList(CancellationToken token = default)
        {
            var data = await _Db.TblCountries.AsNoTracking().ToListAsync(token);
            List<SelectListItem> Nation = data
                .OrderBy(n => n.CountryName)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.CountryId.ToString(),
                        Text = n.CountryName
                    }).ToList();
            var nationip = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Nationality"
            };
            Nation.Insert(0, nationip);
            return new SelectList(Nation, "Value", "Text");

        }
        public async Task< IEnumerable<EmployeeNetSalaryInfo>> GetNetSalaryOfEmoployee(CancellationToken token = default)
        {
            var query = _Db.TblEmployees
                 .GroupJoin(
                    _Db.TblEmployeeAllowances,
                     Emp => Emp.EmployeeId,
                     EmpAL => EmpAL.EmployeeId,
                     (Emp, EmpAL) => new { Emp, EmpAL }
                    )
                 .SelectMany(
                    obj => obj.EmpAL.DefaultIfEmpty(),
                    (data, EmAL) => new
                    {
                        EmployeeId = data.Emp.EmployeeId,
                        EmployeeName = data.Emp.EmployeeName,
                        EmployeeGrossSalary = data.Emp.GrossSalary,
                        Amount = EmAL.Amount,
                    });
            var result = query
                .GroupBy(r => new
                {
                    r.EmployeeName,
                    r.EmployeeGrossSalary,
                    r.EmployeeId
                })
                .Select(n => new EmployeeNetSalaryInfo
                {
                    EmployeeId = n.Key.EmployeeId,
                    EmployeeName = n.Key.EmployeeName,
                    EmployeeGrossSalary = n.Key.EmployeeGrossSalary + n.Sum(a => a.Amount),
                    Amount = n.Sum(a => a.Amount)
                }).ToListAsync(token);
            return await result;
        }
        public async Task<IEnumerable<BranchEmployeeInfo>> GETBranchNameEmployee(int id, CancellationToken token = default)
        {
            var queryMaster = _Db.TblBranches
                 .Join(
                    _Db.TblEmployees,
                     Branch => Branch.BranchId,
                     Emp => Emp.BranchId,
                     (Branch, Emp) => new { Branch, Emp }
                    );
            var query = queryMaster
                .Join(
                    _Db.TblEmployeeAttandances,
                    data => data.Emp.EmployeeId,
                    EmpAt => EmpAt.EmployeeId,
                    (data, EmpAt) => new { data, EmpAt }
                  ).Where(x => x.data.Branch.CompanyId == id)
                .Select(n => new
                {
                    EmployeeId = n.data.Emp.EmployeeId,
                    EmployeeName = n.data.Emp.EmployeeName,
                    BranchId = n.data.Branch.BranchId,
                    BranchName = n.data.Branch.BranchName,
                    AttandanceId = n.EmpAt.AttandanceId,
                    AttandanceDate = n.EmpAt.AttandanceDate,
                    CompanyID = n.data.Branch.CompanyId
                });
            var result = query
                  .Select(n => new BranchEmployeeInfo
                  {
                      BranchId = n.BranchId,
                      EmployeeId = n.EmployeeId,
                      EmployeeName = n.EmployeeName,
                      BranchName = n.BranchName,
                      Presents = 0,
                      Absents = 0,
                      CompanyID = (int)n.CompanyID
                  }).ToListAsync(token);
            return await result;
        }
    }
}