using EmployeeManage.Models;
using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Request;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository
{
    public class TaxRepo : ITaxRepo
    {
        private readonly EmployeesDBContext _db;

        public TaxRepo(EmployeesDBContext db)
        {
            _db = db;

        }

        public async Task CreateTaxAsync(AddTaxRequestVM model, string userGUID, string companyGUID,
            string branchGUID, CancellationToken token = default)
        {
            var tax = new TblTax
            {
                TaxId = model.taxId,
                Name = model.Name,
                Percentage = model.Percentage,
                Date = DateTime.Today.Date,
                CreatedByUserGuid = userGUID,
                CompanyGuid = companyGUID,
                BranchGuid = branchGUID
            };
            _db.TblTaxes.Add(tax);
            await _db.SaveChangesAsync(token);

        }

        public async Task<IEnumerable<SelectListItem>> GetTaxListAsync(CancellationToken token = default)
        {
            var data = await _db.TblTaxes
                .AsNoTracking()
                .ToListAsync(token);

            List<SelectListItem> taxlist = data
                .OrderBy(n => n.Name)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.TaxId.ToString(),
                        Text = n.Name
                    }).ToList();
            var Itemslist = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Tax"
            };
            taxlist.Insert(0, Itemslist);
            return new SelectList(taxlist, "Value", "Text");
        }

        public async Task<IEnumerable<TaxResponseVM>> GetItemsTaxListAsync(int id, CancellationToken token)
        {
            {
                var result = await _db.TblItems
                    .AsNoTracking()
                    .Where(x => x.ItemId == id)
                    .Join(
                       _db.TblTaxes,
                      data => data.TaxId,
                      tax => tax.TaxId,
                      (data, tax) => new { data, tax }
                      )
                      .Select(r => new TaxResponseVM
                      {
                          TaxId = r.data.TaxId,
                          Name = r.tax.Name
                      })
                       .ToListAsync(token);

                return result;
            }
        }

        public async Task<TaxResponseVM> GetTaxByIdAsync(int id, CancellationToken token)
        {
            var record = await ValidateRecord(id, token);

            return new TaxResponseVM
            {
                TaxId = record.TaxId,
                Name = record.Name,
                Percentage = record.Percentage
            };
        }

        public async Task<TaxResponseVM> GetTax(int id, CancellationToken token)
        {
            var result = await _db.TblTaxes
                      .AsNoTracking()
                      .Where(x => x.TaxId == id)
                        .Select(r => new TaxResponseVM
                        {
                            Percentage = r.Percentage
                        }).FirstOrDefaultAsync(token);

            return result;
        }


        private async Task<TblTax> ValidateRecord(int Taxid, CancellationToken token = default)
        {
            var record = _db.TblTaxes
                .AsNoTracking()
                .Where(x => x.TaxId == Taxid)
                .FirstOrDefaultAsync(token);

            if (record == null)
            {
                throw new Exception("Record not found");
            }

            return await record;
        }

       
    }
}
