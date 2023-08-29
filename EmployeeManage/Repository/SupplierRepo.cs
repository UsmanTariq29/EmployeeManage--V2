using EmployeeManage.Models;
using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Request;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository
{
    public class SupplierRepo : ISupplierRepo
    {
        private readonly EmployeesDBContext _db;

        public SupplierRepo(EmployeesDBContext db)
        {
            _db = db;

        }
        public async Task CreateSupplier(AddSupplierRequest model, string userGUID, string branchGUID, string companyGUID, CancellationToken token = default)
        {
            var query = new TblSupplier
            {
                Name = model.Name,
                Address = model.Address,
                Cnic = model.CNIC,
                IsActive = model.IsActive,
                CreatedByUserGuid = userGUID,
                CompanyGuid = companyGUID,
                BranchGuid = branchGUID,
                DetailCodeId = 0,
                SubCodeId = 0
            };

            _db.TblSuppliers.Add(query);

            await _db.SaveChangesAsync(token);

        }
        public async Task<IEnumerable<SelectListItem>> SuppliersList(CancellationToken token = default)
        {
            var data = await _db.TblSuppliers
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .ToListAsync(token);

            List<SelectListItem> itemslist = data
                .OrderBy(n => n.Name)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.SupplierId.ToString(),
                        Text = n.Name
                    }).ToList();
            var Itemslist = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Supplier"
            };
            itemslist.Insert(0, Itemslist);
            return new SelectList(itemslist, "Value", "Text");
        }
    }
}