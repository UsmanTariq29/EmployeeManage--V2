using EmployeeManage.Models;
using Microsoft.AspNetCore.Mvc;
using webAPI.Models;

namespace webAPI.Controllers
{
    [Route("api/{controller}")]
    public class UsersController : Controller
    {
        private readonly EmployeesDBContext _employeesDBContext;
        public UsersController(EmployeesDBContext employeesDBContext)
        {
            _employeesDBContext = employeesDBContext;
        }
        [HttpGet]
        public List<RegisterUser> GetUsers()
        {
            return _employeesDBContext.TblUsers.ToList();

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
