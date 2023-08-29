using EmployeeManage.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManage.Controllers
{
    public class VoucherController : Controller
    {
        private readonly IVoucherHelperRepo _voucherHelperRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public VoucherController(IVoucherHelperRepo voucherHelperRepo, IHttpContextAccessor httpContextAccessor)
        {
            _voucherHelperRepo = voucherHelperRepo;
            _httpContextAccessor = httpContextAccessor;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult VoucherMaster()
        {
            var userGUID = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
            var companyGUID = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");
            var branchGUID = _httpContextAccessor.HttpContext.Session.GetString("branchGUID");

            return View();
        }
        public IActionResult VoucherDetail()
        {
            return View();
        }
    }
}
