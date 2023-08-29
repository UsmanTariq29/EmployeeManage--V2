using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Request;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IItemGroupRepo _itemGroupRepo;
        private readonly IItemRepo _itemRepo;
        private readonly ISupplierRepo _supplierRepo;
        private readonly IPurchaseRepo _purchaseRepo;
        private readonly ITaxRepo _taxRepo;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public PurchaseController(IItemGroupRepo itemGroupRepo, ISupplierRepo supplierRepo,
            IPurchaseRepo purchaseRepo, ITaxRepo taxRepo, IItemRepo itemRepo, 
             IHttpContextAccessor httpContextAccessor)
        {
            _itemGroupRepo = itemGroupRepo;
            _httpContextAccessor = httpContextAccessor;
            _supplierRepo = supplierRepo;
            _itemRepo = itemRepo;
            _purchaseRepo = purchaseRepo;
            _taxRepo = taxRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Purchase(CancellationToken token = default)
        {
            var supplier = await _supplierRepo.SuppliersList(token);
            var tax = await _taxRepo.GetTaxListAsync(token);
            var itemgrouplist = await _itemGroupRepo.GroupList(token);
            var itemlist = await _itemRepo.ItemsList(token);

            ListsResponseVM Lists = new ListsResponseVM
            {
                SupplierList = supplier,
                ItemList = itemlist,
                ItemGroupList = itemgrouplist,
                taxList = tax,
            };
            return View(Lists);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Purchase(purchaseRequest model, string purchasedItems, CancellationToken token = default)
        {
            var userGUID = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
            var CompanyGUID = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");
            var branchGUID = _httpContextAccessor.HttpContext.Session.GetString("branchGUID");

            DataTable dt = (DataTable)JsonConvert.DeserializeObject(purchasedItems, typeof(DataTable));


            await _purchaseRepo.SavePurchase(model, dt, userGUID, branchGUID, CompanyGUID, token);
            return Json(true);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdatePurchase(purchaseRequest model, string purchasedItems, CancellationToken token = default)
        {
            var userGUID = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
            var CompanyGUID = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");
            var branchGUID = _httpContextAccessor.HttpContext.Session.GetString("branchGUID");

            DataTable dt = (DataTable)JsonConvert.DeserializeObject(purchasedItems, typeof(DataTable));


            await _purchaseRepo.UpdatePurchase(model, dt, userGUID, branchGUID, CompanyGUID, token);
            return Json(true);
        }
        [HttpPost]
        
        public async Task<IActionResult> SuperVisionPurchase(int purchaseMasterID, CancellationToken token = default)
        {
            var userGUID = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
            var CompanyGUID = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");
            var branchGUID = _httpContextAccessor.HttpContext.Session.GetString("branchGUID");

            await _purchaseRepo.SuperVision(purchaseMasterID, userGUID, branchGUID, CompanyGUID, token);
            return Json(true);
        }
        public async Task<IActionResult> ClearencePurchase(int PurchaseMasterId, string PurchaseNumber, decimal Cash
            , decimal Account, decimal Bank, decimal Cheque, CancellationToken token = default)
        {
            var userGUID = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
            var CompanyGUID = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");
            var branchGUID = _httpContextAccessor.HttpContext.Session.GetString("branchGUID");

            await _purchaseRepo.Clearence(PurchaseMasterId, PurchaseNumber, Cash, Account, Bank, Cheque, userGUID, branchGUID, CompanyGUID,
          token);
            return Json(true);
        }
        public async Task<IActionResult> PurchaseDisplayNo(int purchaseMasterID, CancellationToken token = default)
        {
            var CompanyGUID = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");
            var branchGUID = _httpContextAccessor.HttpContext.Session.GetString("branchGUID");

           var voucherDisplayNo =  await _purchaseRepo.VoucherDisplayNo(purchaseMasterID, branchGUID, CompanyGUID, token);
            return Json(voucherDisplayNo);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id, CancellationToken token = default)
        {
            var result = await _purchaseRepo.GetPurchaseById(id, token);
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> getitemsTaxData(int id, CancellationToken token = default)
        {
            var AllData = await _taxRepo.GetItemsTaxListAsync(id, token);


            return Json(AllData);
        }
        [HttpGet]
        public async Task<JsonResult> getitemsGroupData(CancellationToken token = default)
        {
            var AllData = await _itemGroupRepo.GroupList(token);

            return Json(AllData);
        }
        [HttpGet]
        public async Task<JsonResult> getTaxData(CancellationToken token = default)
        {
            var AllData = await _taxRepo.GetTaxListAsync(token);

            return Json(AllData);
        }

        [HttpGet]
        public async Task<JsonResult> getSupplierData(CancellationToken token = default)
        {
            var AllData = await _supplierRepo.SuppliersList(token);

            return Json(AllData);
        }

        [HttpGet]
        public async Task<JsonResult> GetTax(int id, CancellationToken token = default)
        {
            var AllData = await _taxRepo.GetTaxByIdAsync(id, token);

            return Json(AllData);
        }
        [HttpGet]
        [Authorize]
        public ActionResult PurchaseList(CancellationToken token = default)
        {

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetPurchases(CancellationToken token = default)
        {
            var result = await _purchaseRepo.GetAllPurchase(token);

            return Json(result);
        }
        [HttpGet]
        [Authorize]
        public ActionResult PurchasePayment(CancellationToken token = default)
        {

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> PurchasePayments(CancellationToken token = default)
        {
            var result = await _purchaseRepo.GetAllPurchasePayment(token);

            return Json(result);
        }
    }
}