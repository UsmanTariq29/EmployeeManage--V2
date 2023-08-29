using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepo _dashboardRepo;
        private readonly IDocument _document;

        public DashboardController(IDashboardRepo dashboardRepo, IDocument document)
        {
            _dashboardRepo = dashboardRepo;
            _document = document;
        }

        public async Task<IActionResult> Index(CancellationToken token = default)
        {
            EmployeeBirthdayAndNewHires employee = new EmployeeBirthdayAndNewHires();

            employee.employeedata = await _dashboardRepo.GetBirthdayBuddies(token);
            employee.employeeHires = await _dashboardRepo.GetNewHires(token);
            employee.employeeExpireDoc = await _dashboardRepo.GetExpiredDocuments(token);
            employee.activeEmployees = await _dashboardRepo.GetActiveEmployees(token);

            return View(employee);
        }
        public async Task<IActionResult> DownloadDocument(int id, CancellationToken token = default)
        {
            var document = await _document.GetDocument(id, token);
            return new FileStreamResult(document.GetStream(), document.ContentType);
        }
    }
}