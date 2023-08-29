using EmployeeManage.Models;
using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Responses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository
{
    public class DocumentCountRepo : IDocumentCount
    {
        private readonly EmployeesDBContext _db;

        public DocumentCountRepo(EmployeesDBContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<TotalDocumentsInfo>> DocumentsInfoList(CancellationToken token = default)
        {
            var queryMaster = _db.TblEmployees
                 .GroupJoin(
                    _db.TblEmployeeDocuments,
                     Emp => Emp.EmployeeId,
                     EmpD => EmpD.EmployeeId,
                     (Emp, EmpD) => new { Emp, EmpD }
                    )
                 .SelectMany(
                    obj => obj.EmpD.DefaultIfEmpty(),
                    (data, EmpD) => new
                    {
                        EmployeeId = data.Emp.EmployeeId,
                        EmployeeName = data.Emp.EmployeeName,
                        DocumentId = (int?)EmpD.DocumentCategoryId
                    });
            var result = queryMaster
             .Join(_db.TblDocumentCategories,
                data => data.DocumentId,

                cat => cat.DocumentCategoryId,
                (data, cat) => new { data, cat })
             .Select(
                (result) => new TotalDocumentsInfo
                {
                    EmployeeId = result.data.EmployeeId,
                    EmployeeName = result.data.EmployeeName,
                    DocumentName = result.cat.DocumentName,
                    DocumentId = result.data.DocumentId
                });
            var final = result
                .GroupBy(r => new
                {
                    r.DocumentName,
                    r.EmployeeName
                }).Select(n => new TotalDocumentsInfo
                {
                    DocumentName = n.Key.DocumentName,
                    EmployeeName = n.Key.EmployeeName,
                    DocumentCount = n.Count()
                }).ToListAsync(token);
            return await final;
        }
    }
}