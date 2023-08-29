using EmployeeManage.Models;
using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository
{
    public class DashboardRepo : IDashboardRepo
    {
        private readonly EmployeesDBContext _db;

        public DashboardRepo(EmployeesDBContext db)
        {
            _db = db;
        }

        public async Task<List<EmployeeDataResponseVM>> GetBirthdayBuddies(CancellationToken token = default)
        {
            var data = _db.TblEmployees
                .AsNoTracking()
                .Where(r => r.BirthDate.Value.Day == DateTime.Today.Day
                            && r.BirthDate.Value.Month == DateTime.Today.Month)
                .Join(
                    _db.TblDepartments,
                    data => data.DepartmentId,
                    department=> department.DepartmentId,
                    (data,department) => new
                    {
                        data,
                        department.DepartmentName
                    }
                ).Join(
                    _db.TblBranches,
                    data => data.data.BranchId,
                    branch => branch.BranchId,
                    (data, branch) => new
                    {
                        data,
                        branch.BranchName
                    }
                )
                .Select(r => new EmployeeDataResponseVM
                {
                    EmolyeeName = r.data.data.EmployeeName,
                   DepartmentName = r.data.DepartmentName,
                    BranchName = r.BranchName

                });

            return await data.ToListAsync(token);
        }
        public async Task<List<ExpiredDocuments>> GetExpiredDocuments(CancellationToken token = default)
        {
            var result = _db.TblEmployeeDocuments
                .AsNoTracking()
                .Where(r => r.ExpireDate < DateTime.Today
                            )
                .Join(
                    _db.TblDocumentCategories,
                    Edoc => Edoc.DocumentCategoryId ,
                    Doc=> Doc.DocumentCategoryId,
                    (Edoc, Doc) => new
                    {
                        Edoc,
                        Doc.DocumentName, 
                       
                    }
                )
                .Join(
                    _db.TblEmployees,
                    Data => Data.Edoc.EmployeeId,
                    Emp => Emp.EmployeeId,
                    (Data,Emp ) => new
                    {
                        Emp,
                        Data,
                      Emp.BranchId
                    }
                ).Join(
                    _db.TblBranches,
                    Data2 => Data2.BranchId,
                    branch => branch.BranchId,
                    (Dataall, branch) => new
                    {
                        Dataall,
                        branch.BranchName
                    }
                ).Join(
                    _db.TblDepartments,
                    Data3 => Data3.Dataall.Emp.DepartmentId,
                     Dep => Dep.DepartmentId,
                    (Data,Dep ) => new
                    {
                        Data,
                        Dep.DepartmentName
                    }
                )
                .Select(r => new ExpiredDocuments
                {
                    EmolyeeName = r.Data.Dataall.Emp.EmployeeName,
                    DocumentName = r.Data.Dataall.Data.DocumentName,
                    DepartmentName = r.DepartmentName,
                    BranchName = r.Data.BranchName,
                    DocumentId = r.Data.Dataall.Data.Edoc.EmployeeDocumentId,
                    ExpireDocument =r.Data.Dataall.Data.Edoc.ExpireDate.ToString("dd-MM-yyyy"),
                    Expiredays = DateTime.Now.Subtract(r.Data.Dataall.Data.Edoc.ExpireDate).Days
                    
                });

            return await result.ToListAsync(token);
        }  
        public async Task<List<ActiveEmployee>> GetActiveEmployees(CancellationToken token = default)
        {
            var result = _db.TblEmployees
                .AsNoTracking()
                .Where(r => r.IsActive ==true     
                )
                .Join(
                    _db.TblDepartments,
                    data => data.DepartmentId,
                    dep => dep.DepartmentId,
                    (data, dep) => new
                    {
                        data,
                        dep.DepartmentName,

                    }
                )
                .Join(
                    _db.TblBranches,
                    Data => Data.data.BranchId,
                    branch => branch.BranchId,
                    (Data, branch) => new
                    {
                        Data,
                        branch.BranchName
                    }
                )
                .Select(r => new ActiveEmployee
                {
                    EmployeeName = r.Data.data.EmployeeName,
                    DepartmentName = r.Data.DepartmentName,
                    BranchName = r.BranchName,
                    EmployeeEmail = r.Data.data.EmployeeEmail
                });

            return await result.ToListAsync(token);
        }
        public async Task<List<EmployeeHires>> GetNewHires(CancellationToken token = default)
        {
            DateTime date = DateTime.Now.AddDays(-3);
            var data = _db.TblEmployees.AsNoTracking().Where(r => r.JoinDate <= DateTime.Now &&  r.JoinDate >= DateTime.Now.AddDays(-15))
                .Join(
                    _db.TblDepartments,
                    data => data.DepartmentId,
                    department => department.DepartmentId,
                    (data, department) => new
                    {
                        data,
                        department.DepartmentName
                    }
                ).Join(
                    _db.TblBranches,
                    data => data.data.BranchId,
                    branch => branch.BranchId,
                    (data, branch) => new
                    {
                        data,
                        branch.BranchName
                    }
                ).Select( r=> new EmployeeHires
                {
                    EmolyeeName= r.data.data.EmployeeName,
                    DepartmentName =r.data.DepartmentName,
                    BranchName = r.BranchName
            });

            return await data.ToListAsync(token);
        }
    }
}
