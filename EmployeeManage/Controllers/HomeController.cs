using EmployeeManage.Model;
using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Controllers
{
    public class HomeController : Controller
    {
        // EmployeeAPI _API = new EmployeeAPI();

        private readonly IEmployeeRepo _employeerepository;
        private readonly IDepartmentRepo _departmentRepo;
        private readonly IDocument _documentRepo;
        private readonly IDashboardRepo _employeeData;

        public HomeController(IEmployeeRepo employeeRepository,
            IDocument documentRepository, IDepartmentRepo departmentRepository, IDashboardRepo employeeDataRepo)
        {
            _employeerepository = employeeRepository;
            _departmentRepo = departmentRepository;
            _documentRepo = documentRepository;
            _employeeData = employeeDataRepo;
        }

        public IActionResult GetseSssion()
        {
            this.HttpContext.Session.SetString("Session Key", "SessionValue");
            return RedirectToAction("GetseSssion");
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken token = default)
        {
            var result = await _employeerepository.SelectEmployeeInfo(token);

            return View(result);

        }

        [HttpGet]
        public async Task<ViewResult> Details(int id)
        {
            //var data = _employeerepository.GetEmployeeDetails(id);

            var result = await _employeerepository.GetEmployee(id);
            if (result == null)
            {
                return View("EmployeeNotFound", id);
            }
            var dep = await _departmentRepo.GetDeptartmentList();
            var nation = await _departmentRepo.GetNationalityList();
            var branch = await _departmentRepo.GetBranchList();

            EmployeedetailsVM employeedetails = new EmployeedetailsVM
            {
                EmployeeID = result.EmployeeId,
                EmployeeEmail = result.EmployeeEmail,
                EmployeeName = result.EmployeeName,
                DepartmentId = result.DepartmentId,
                grossSalary = result.GrossSalary,
                branchId = result.BranchId,
                NationalityId = result.NationalityId,
                photoPath = result.PhotoPath,
                DepartmentList = dep,
                BranchList = branch,
                NationalityList = nation
            };

            //EmployeedetailsVM employeedata = null;
            //HttpClient client = _API.Initial();
            //HttpResponseMessage res = await client.GetAsync("Employee/Details/" + id);
            //if (res.IsSuccessStatusCode)
            //{
            //    var result = res.Content.ReadAsStringAsync().Result;
            //    employeedata = JsonConvert.DeserializeObject<EmployeedetailsVM>(result);
            //}

            return View(employeedetails);
        }

        [HttpGet]
        public async Task<ViewResult> Create(CancellationToken token = default)
        {
            var deplist = await _departmentRepo.GetDeptartmentList(token);
            var nation = await _departmentRepo.GetNationalityList(token);
            var branch = await _departmentRepo.GetBranchList(token);

            EmployeeCreateVM employeeCreateVM = new EmployeeCreateVM()
            {
                DepartmentList = deplist,
                NationalityList = nation,
                BranchList = branch

            };
            return View(employeeCreateVM);

            //EmployeeCreateVM createEmployee = null;
            //HttpClient client = _API.Initial();
            //HttpResponseMessage res = await client.GetAsync("Department/AllDepartments");
            //if (res.IsSuccessStatusCode)
            //{
            //    var result = res.Content.ReadAsStringAsync().Result;
            //    createEmployee.DepartmentList = (IEnumerable<SelectListItem>)JsonConvert.DeserializeObject(result);
            //}
            //EmployeeCreateVM employeeCreateVM = new EmployeeCreateVM()
            //{

            //    DepartmentList = (IEnumerable<SelectListItem>)createEmployee
            //};
            //return View(employeeCreateVM);
        }

        [HttpPost]
        public IActionResult Create([FromForm] EmployeeCreateVM employee)
        {
            if (ModelState.IsValid)
            {
                _employeerepository.Create(employee);
            }
            // _employeerepository.Create(employee);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _employeerepository.GetEmployee(id);
            if (result == null)
            {
                return View("EmployeeNotFound", id);
            }
            var dep = await _departmentRepo.GetDeptartmentList();
            var nation = await _departmentRepo.GetNationalityList();
            var branch = await _departmentRepo.GetBranchList();
            EmployeeUpdateVM employeeUpdate = new EmployeeUpdateVM
            {
                EmployeeID = result.EmployeeId,
                EmployeeEmail = result.EmployeeEmail,
                EmployeeName = result.EmployeeName,
                DepartmentId = result.DepartmentId,
                grossSalary = result.GrossSalary,
                branchId = result.BranchId,
                NationalityId = result.NationalityId,
                photoPath = result.PhotoPath,
                DepartmentList = dep,
                BranchList = branch,
                NationalityList = nation
            };
            return View(employeeUpdate);
        }

        [HttpPost]
        public IActionResult update(EmployeeUpdateVM emp)
        {
            _employeerepository.updateAsync(emp);
            return RedirectToAction("Index");
        }
    }
}