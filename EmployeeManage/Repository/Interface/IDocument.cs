using EmployeeManage.Utilities.FileUpload;
using EmployeeManage.ViewModels;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository.Interface
{
    public interface IDocument
    {
        void AddDocument(EmployeeDocument model, CancellationToken token = default);
        IEnumerable<SelectList> GetSelectedEmployeelist(int id);
        IEnumerable<SelectListItem> GetDocumentName();
        IEnumerable<SelectListItem> GetDocumentList();
        Task<IEnumerable<EmployeesDocumentInfo>> GetemployeeDocument(int id, CancellationToken token = default);
        Task<FileDownloadResult> GetDocument(int documentId, CancellationToken token = default);

    }
}
