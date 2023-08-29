using Microsoft.AspNetCore.Mvc;

namespace EmployeeManage.Controllers
{
    public class DepartmentsController : Controller
    {
        public string List()
        {
            return "List of Departments";
        }
        public string Detaiils()
        {
            return "Details of Departments";
        }
    }
}
