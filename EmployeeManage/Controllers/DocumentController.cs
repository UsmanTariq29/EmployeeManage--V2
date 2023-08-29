using EmployeeManage.Model;
using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Controllers
{
    public class DocumentController : Controller
    {

        private readonly IEmployeeRepo _employeerepository;
        private readonly IDepartmentRepo _departmentRepo;
        private readonly IDocument _documentRepo;
        public DocumentController(IEmployeeRepo employeeRepository, IDepartmentRepo departmentRepo, IDocument documentRepo)
        {
            _employeerepository = employeeRepository;
            _departmentRepo = departmentRepo;
            _documentRepo = documentRepo;
        }

        [HttpGet]
        public async Task<ViewResult> InsertDocumentAsync()
        {
            var deplist = await _departmentRepo.GetDeptartmentList();
            var emplolyeelist = _employeerepository.GetEmployeeList();
            var documentlist = _documentRepo.GetDocumentName();
            EmployeeDocument employeeDocument = new EmployeeDocument()
            {
                DepartmentList = deplist,
                DocumentList = documentlist,
                EmployeeActiveList = emplolyeelist
            };
            return View(employeeDocument);
        }

        [HttpPost]
        public IActionResult InsertDocument(EmployeeDocument model)
        {
            if (ModelState.IsValid)
            {
                _documentRepo.AddDocument(model);
            }

            var deplist = _departmentRepo.GetDeptartmentList();
            var emplolyeelist = _employeerepository.GetEmployeeList();
            var documentlist = _documentRepo.GetDocumentName();

            EmployeeDocument employeeDocument1 = new EmployeeDocument()
            {
                EmployeeActiveList = emplolyeelist,
                DepartmentList = (IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>)deplist,
                DocumentList = documentlist
            };
            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult getemplList(int id)
        {

            var emplist = _employeerepository.GetEmployeeListAct(id);
            return Json(emplist.ToList());
        }

        public ActionResult getRemarksAndPath(int DepartID, int EmpID, CancellationToken token = default)
        {
            var emplist = _employeerepository.GetRemarkPath(DepartID, token);
            return Json(emplist);
        }

        public async Task<ViewResult> DocumentDetailsAsync(int id)
        {
            var department = await _departmentRepo.GetDeptartmentList();
            var emp = _employeerepository.GetEmployeeList();
            var docList = _documentRepo.GetDocumentList();
            EmployeesDocumentInfo employeesDocumentInfo = new EmployeesDocumentInfo()
            {
                EmployeeActiveList = emp,
                DepartmentList = department,
                DocumentList = docList

            };
            return View(employeesDocumentInfo);
        }

        //Download Document using Document Repository 
        public async Task<FileStreamResult> DownloadDocument(int id)
        {
            var document = await _documentRepo.GetDocument(id);
            return new FileStreamResult(document.GetStream(), document.ContentType) { FileDownloadName = document.Name }; ;
        }
    }
}