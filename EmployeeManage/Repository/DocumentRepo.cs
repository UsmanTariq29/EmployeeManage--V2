using EmployeeManage.Models;
using EmployeeManage.Repository.Interface;
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

namespace EmployeeManage.Repository
{
    public class DocumentRepo : IDocument
    {
        private readonly EmployeesDBContext _db;
        private readonly FileUploadConfig _uploadConfig;
        private readonly IConfiguration _configuration;

        public DocumentRepo(EmployeesDBContext db, IOptionsSnapshot<FileUploadConfig> configAccessor,
            IConfiguration configuration)
        {
            _db = db;
            _uploadConfig = configAccessor.Get(FileUploadConfigTypeEnum.EmployeeDocument.ToString());
            _configuration = configuration;
        }
        public void AddDocument(EmployeeDocument model, CancellationToken token = default)
        {
            if (model.DocumentPath != null)
            {
                FileUploadHelper.VerifyUploadedFiles(_uploadConfig, new[] { model.DocumentPath });
            }
            var employeDocument = new TblEmployeeDocument
            {
                DepartmentId = model.DepartmentId,
                DocumentCategoryId = model.DocumentId,
                EmployeeId = model.EmployeeID,
                DocumentPath = string.Empty,
                Remarks = model.Remarks
            };
            if (model.DocumentPath != null)
            {
                var documentPath = FileUploadHelper.GetUploadDirectory(_uploadConfig, _configuration);

                employeDocument.DocumentPath = FileUploadHelper.SaveFile(model.DocumentPath, documentPath,
                    $"{ DateTime.Now.ToString("_MMddyyyy_ss")}.{model.DocumentPath.FileName + Path.GetRandomFileName().Length}");
            }

            _db.TblEmployeeDocuments.Add(employeDocument);
            _db.SaveChangesAsync(token);
        }
        public IEnumerable<SelectListItem> GetDocumentName()
        {
            List<SelectListItem> documentlist = _db.TblDocumentCategories.AsNoTracking()
                   .OrderBy(n => n.DocumentName)
                       .Select(n =>
                       new SelectListItem
                       {
                           Value = n.DocumentCategoryId.ToString(),
                           Text = n.DocumentName
                       }).ToList();
            var documenttip = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Category"
            };
            documentlist.Insert(0, documenttip);
            return new SelectList(documentlist, "Value", "Text");
        }
        public async Task<IEnumerable<EmployeesDocumentInfo>> GetemployeeDocument(int id, CancellationToken token = default)
        {
            return await _db.TblEmployeeDocuments
                .AsNoTracking()
                .Where(n => n.EmployeeId == id)
                .Select(a => new EmployeesDocumentInfo
                {
                    DocumentId = a.DocumentCategoryId,
                    DocumentPath = a.DocumentPath,
                    EmployeeId = a.EmployeeId,
                    Remarks = a.Remarks
                })
                .ToListAsync();
        }
        public IEnumerable<SelectList> GetSelectedEmployeelist(int id)
        {
            return (IEnumerable<SelectList>)_db.TblEmployees
                .Where(a => a.DepartmentId == id)
                .ToList();
        }
        public IEnumerable<SelectListItem> GetDocumentList()
        {
         List<SelectListItem> docList = _db.TblDocumentCategories.AsNoTracking()
                    .OrderBy(n => n.DocumentName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.DocumentCategoryId.ToString(),
                            Text = n.DocumentName
                        }).ToList();
         var Documenttip = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Document"
            };
            docList.Insert(0, Documenttip);
            return new SelectList(docList, "Value", "Text");
        }
        //public async FileDownloadResult GetDocument(int documentId)
        //{
        //    var record = await RetrieveDocumentRecord(documentId);
        //    var documentPath =  FileUploadHelper.GetUploadDirectory(_uploadConfig, _configuration);
        //    return  FileUploadHelper.GetSavedFile(documentPath, record.DocumentPath);
        //}
        public async Task<EmployeesDocumentInfo> RetrieveDocumentRecord(int id, CancellationToken token = default)
        {
            var data =  _db.TblEmployeeDocuments
                .AsNoTracking()
                .Where(n => n.EmployeeDocumentId == id)
                .Select(a => new EmployeesDocumentInfo
                {
                    DocumentId = a.EmployeeDocumentId,
                    DocumentPath = a.DocumentPath,
                    EmployeeId = a.EmployeeId,
                    Remarks = a.Remarks
                })
                .FirstOrDefaultAsync(token);

            if(data == null)
            {
                throw new Exception("Record Not Found--");
            }
            return await data;
        }

       public async Task<FileDownloadResult> GetDocument(int documentId, CancellationToken token = default)
        {
            var record = await RetrieveDocumentRecord(documentId,token);
            
                var documentPath = FileUploadHelper.GetUploadDirectory(_uploadConfig, _configuration);
                return  FileUploadHelper.GetSavedFile(documentPath, record.DocumentPath);
            
        }
    }
}