using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Controllers
{
    public class SupplierController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISupplierRepo _supplierRepo;


        public SupplierController(ISupplierRepo supplierRepo, IHttpContextAccessor httpContextAccessor)
        {

            _httpContextAccessor = httpContextAccessor;
            _supplierRepo = supplierRepo;
        }
        [HttpGet]
        public IActionResult AddSupplier()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSupplier(AddSupplierRequest model, CancellationToken token = default)
        {
            try
            {
                var userGUID = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
                var companyGUID = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");
                var branchGUID = _httpContextAccessor.HttpContext.Session.GetString("branchGUID");

                if (ModelState.IsValid)
                {
                    await _supplierRepo.CreateSupplier(model, userGUID, branchGUID, companyGUID, token);

                    return Json(true);
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(nameof(token), Ex.Message);
                return Json(false);
            }
            return View();
        }
    }
}