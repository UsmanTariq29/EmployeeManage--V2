using EmployeeManage.Models;
using EmployeeManage.ViewModels.Request;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EmployeeManage.Repository.Interface;

namespace EmployeeManage.Repository
{
    public class UnitofMeasureRepo : IUnitOfMeasureRepo
    {
        private readonly EmployeesDBContext _db;

        public UnitofMeasureRepo(EmployeesDBContext db)
        {
            _db = db;

        }
        public async Task<List<TblUnitOfMeasure>> getUOM(CancellationToken token = default)
        {
            return await _db.TblUnitOfMeasures.ToListAsync(token);
        }

        public async Task<IEnumerable<SelectListItem>> UnitInCaseList(CancellationToken token = default)
        {
            var data = await getUOM(token);

            List<SelectListItem> unit = data
                .OrderBy(n => n.UnitOfMeasure)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.UnitOfMeasureId.ToString(),
                        Text = n.UnitOfMeasure
                    }).ToList();
            var UOM = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Unit"
            };
            unit.Insert(0, UOM);
            return new SelectList(unit, "Value", "Text");

        }
        public async Task AddUnitOfMeasure(UnitOfMeasureRequest model, CancellationToken token = default)
        {
            await ValidateUnitMeasureRecord(model.unitOfMeasure, token);
            var measure = new TblUnitOfMeasure
            {
                UnitOfMeasure = model.unitOfMeasure,
                UnitOfMeasureDescription = model.UnitOfMeasureDescription
            };
            _db.TblUnitOfMeasures.Add(measure);
            await _db.SaveChangesAsync(token);
        }
        public async Task<TblUnitOfMeasure> ValidateUnitMeasureRecord(string UnitOfMeasureName, CancellationToken token = default)
        {
            var record = await _db.TblUnitOfMeasures
                .AsNoTracking()
                .Where(x => x.UnitOfMeasure.Equals(UnitOfMeasureName))
                .FirstOrDefaultAsync(token);
            if (record != null)
            {
                throw new Exception("Unit Of Measure Already Exists");
            }
            return record;
        }

    }
}
