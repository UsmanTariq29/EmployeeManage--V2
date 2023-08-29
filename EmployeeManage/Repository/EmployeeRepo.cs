using EmployeeManage.Models;
using EmployeeManage.Utilities.FileUpload;
using EmployeeManage.ViewModels;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Model
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly EmployeesDBContext _db;
        private readonly FileUploadConfig _uploadConfig;
        private readonly IConfiguration _configuration;
        public EmployeeRepo(EmployeesDBContext db, IOptionsSnapshot<FileUploadConfig> configAccessor,
            IConfiguration configuration)
        {
            _db = db;
            _uploadConfig = configAccessor.Get(FileUploadConfigTypeEnum.ClientProfilePicture.ToString());
            _configuration = configuration;
        }

        public void Create(EmployeeCreateVM model)
        {
            if (model.photo != null)
            {
                FileUploadHelper.VerifyUploadedFiles(_uploadConfig, new[] { model.photo });
            }
            var employe = new TblEmployee
            {
                EmployeeName = model.EmployeeName,
                EmployeeEmail = model.EmployeeEmail,
                DepartmentId = model.DepartmentId,
                NationalityId = model.NationalityId,
                GrossSalary =model.grossSalary,
                BranchId = model.branchId,
                PhotoPath =string.Empty,
                IsActive = true
            };
            if (model.photo != null)
            {
                var photoPath = FileUploadHelper.GetUploadDirectory(_uploadConfig, _configuration);
                employe.PhotoPath = FileUploadHelper.SaveFile(model.photo, photoPath,
                         $"{ DateTime.Now.ToString("_MMddyyyy_ss")}.{model.photo.FileName + Path.GetRandomFileName().Length}");
            }

            if (VerifyDuplicateEmployee(model.EmployeeName) == false)
            {
                _db.TblEmployees.Add(employe);
                _db.SaveChanges();
            }
            else
                throw new Exception("Please Chnage Credentials");
        }

            public async Task DeleteAsync(int id,CancellationToken token =default)
        {
            var record = await ValidateRecord(id);

            record.IsActive = false;
            _db.TblEmployees.Update(record);
           await _db.SaveChangesAsync(token);
        }

        public async Task<IEnumerable<EmployeeInfo>> SelectEmployeeInfo(CancellationToken token = default)
        {
            var queryMaster =  _db.TblCompanies
                        .GroupJoin(
                            _db.TblBranches,
                            comp => comp.CompanyId,
                            branch => branch.CompanyId,
                            (comp, branch) => new { comp, branch }
                        )
                        .SelectMany(
                            obj => obj.branch.DefaultIfEmpty(),
                            (data, branch) => new
                            {
                                data.comp.CompanyName,
                                data.comp.CompanyAddress,
                                branch.BranchId,
                                branch.BranchName,
                                branch.CompanyId,
                                branch.CountryId
                            });

            var query = queryMaster
                .GroupJoin(
                    _db.TblEmployees,
                    data => data.BranchId,
                    emp => emp.BranchId,
                    (data, emp) => new { data, emp }
                )
                .SelectMany(
                    obj => obj.emp.DefaultIfEmpty(),
                    (data, emp) => new
                    {
                        data.data.CountryId,
                        data.data.CompanyName,
                        data.data.BranchName,
                        data.data.CompanyAddress,
                        emp.EmployeeId,
                        emp.EmployeeName,
                        emp.DepartmentId,
                        emp.PhotoPath,
                        emp.EmployeeEmail,
                        emp.NationalityId,
                        emp.IsActive
                    }
                 )
                .Join(
                    _db.TblCountries,
                    dataN => dataN.NationalityId,
                    country => country.CountryId,
                    (dataN, country) => new { dataN, country }
                )
                .Join(_db.TblCountries,
                    data => data.dataN.CountryId,
                    country => country.CountryId,
                    (data, country) => new { data, country }
                )
                .Select(r => new
                {
                    CompanyName = r.data.dataN.CompanyName,
                    CompanyAddress = r.data.dataN.CompanyAddress,
                    BranchName = r.data.dataN.BranchName,
                    branchcountry = r.country.CountryName,
                    EmployeeID = r.data.dataN.EmployeeId,
                    EmployeeName = r.data.dataN.EmployeeName,
                    DepartmentId = r.data.dataN.DepartmentId,
                    PhotoPath = r.data.dataN.PhotoPath,
                    EmployeeEmail = r.data.dataN.EmployeeEmail,
                    Nationality = r.data.country.CountryName,
                    IsActive = r.data.dataN.IsActive,
                });


            var result = query
                    .Join(
                        _db.TblDepartments,
                        data => data.DepartmentId,
                        dept => dept.DepartmentId,
                        (data, dept) => new { data, dept }
                    )
                    .Select(r => new EmployeeInfo
                    {
                        CompanyName = r.data.CompanyName,
                        CompanyAddress = r.data.CompanyAddress,
                        BranchName = r.data.BranchName,
                        CountryName = r.data.branchcountry,
                        EmployeeID = r.data.EmployeeID,
                        EmployeeName = r.data.EmployeeName,
                        DepartmentName = r.dept.DepartmentName,
                        EmployeeEmail = r.data.EmployeeEmail,
                        PhotoPath = r.data.PhotoPath,
                        Nationality = r.data.Nationality,
                        DepartmentId = r.dept.DepartmentId,
                        Status = r.data.IsActive == true ? "Active" : "inActive"
                    }).ToListAsync(token);

            return await result;
        }

        public async Task<TblEmployee> GetEmployee(int Id,CancellationToken token = default)
        {
            return await ValidateRecord(Id,token);
        }

        public async Task updateAsync(EmployeeUpdateVM model,CancellationToken token = default)
        {
            var record = await GetEmployee(model.EmployeeID);
            record.EmployeeName = model.EmployeeName;
            record.EmployeeEmail = model.EmployeeEmail;
            record.DepartmentId = model.DepartmentId;
            record.BranchId = model.branchId;
            record.NationalityId = model.NationalityId;
            record.GrossSalary = model.grossSalary;

          //  var a = ValidateRecord(model.EmployeeID);

            _db.TblEmployees.Update(record);
            await _db.SaveChangesAsync(token);
        }

        public IEnumerable<SelectListItem> GetEmployeeList()
        {
            List<SelectListItem> employeeActivelist = _db.TblEmployees
                .AsNoTracking()
                    .OrderBy(n => n.EmployeeName)
                    .Select(n =>
                      new SelectListItem
                      {
                          Value = n.EmployeeId.ToString(),
                          Text = n.EmployeeName
                      }).ToList();
            var Employeetip = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Employee"
            };
            employeeActivelist.Insert(0, Employeetip);
            return new SelectList(employeeActivelist, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetEmployeeListAct(int id)
        {
            List<SelectListItem> employeeActivelist = _db.TblEmployees
                .AsNoTracking()
                    .OrderBy(n => n.EmployeeName)
                    .Where(n => n.DepartmentId == id)
                    .Select(n =>
                           new SelectListItem
                           {
                               Value = n.EmployeeId.ToString(),
                               Text = n.EmployeeName
                           }).ToList();
            var Employeetip = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Employee"
            };
            employeeActivelist.Insert(0, Employeetip);
            return new SelectList(employeeActivelist, "Value", "Text");
        }

        public async Task<EmployeeInfo> GetEmployeeDetails(int Id, CancellationToken token = default)
        {
            var result = await SelectEmployeeInfo(token);

            return  result.Where(r => r.EmployeeID == Id).Single();
        }

        public async Task<IEnumerable<EmployeesDocumentInfo>> GetRemarkPath(int id, CancellationToken token = default)
        {
            return await _db.TblEmployeeDocuments
                .Where(r => r.EmployeeId == id)
                .Select(r => new EmployeesDocumentInfo
                {
                    Remarks = r.Remarks,
                    DocumentPath = r.DocumentPath,
                    EmployeeId = r.EmployeeId,
                    DocumentId = r.DocumentCategoryId
                }).ToListAsync(token);
        }
        public async Task<IEnumerable<CompanyInfo>> GetEmployeeCompnyInfo(CancellationToken token = default)
        {
            return await _db.TblCompanies
             .Join(_db.TblBranches,
             comp => comp.CompanyId,
             branch => branch.CompanyId,
             (comp, branch) => new { comp, branch }
             ).Select(r => new CompanyInfo
             {
                 CompanyName = r.comp.CompanyName,
                 BranchCode = r.branch.BranchCode,
                 BranchName = r.branch.BranchName,
                 CompanyAddress = r.comp.CompanyAddress
             }).ToListAsync(token);
        }

        //public async Task<IEnumerable<VwEmployeeDatum>> GetEmployeeData( CancellationToken token = default)
        //{
        //    var param = new SqlParameter("@EmployeeID", 3);
        //    var result = (IEnumerable<VwEmployeeDatum>)_db.VwEmployeeData
        //        .FromSqlRaw
        //        (
        //        "[dbo].[GetEmployeeData] @EmployeeID "
        //        , param
        //        )
        //        .ToListAsync(token);
        //    return  result;
        //}

        private bool VerifyDuplicateEmployee(string employeeName, int? excludeEmployeeId = null)
        {
            employeeName = employeeName.Trim();

            var query = _db.TblEmployees
                .AsNoTracking()
                .Where(x => x.EmployeeName.Equals(employeeName));

            if (excludeEmployeeId.HasValue)
            {
                query = query.Where(q => q.EmployeeId != excludeEmployeeId);

            }

            return query.Any(q => q.EmployeeName.Equals(employeeName.ToUpper()));
        }
        public async Task<TblEmployee> ValidateRecord(int employeeId,CancellationToken token=default)
        {
            var record = _db.TblEmployees
                .AsNoTracking()
                .Where(x => x.EmployeeId == employeeId)
                .FirstOrDefaultAsync(token);
                

            if (record ==  null)
            {
                throw new Exception("Record not found");
            }

            return await record;
        }



       
    }
}