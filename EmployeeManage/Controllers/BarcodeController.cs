using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Request;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Controllers
{
    public class BarcodeController : Controller
    {

        private readonly IBarcodeRepo _iBarcodeRepo;
        private readonly IItemRepo _itemRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public BarcodeController(IBarcodeRepo barcodeRepo, IItemRepo itemRepo, IHttpContextAccessor httpContextAccessor)
        {
            _iBarcodeRepo = barcodeRepo;
            _itemRepo = itemRepo;
            _httpContextAccessor = httpContextAccessor;

        }

        [HttpGet]
        public async Task<IActionResult> BarcodeIndex(CancellationToken token = default)
        {
            var ItemsList = await _itemRepo.ItemsList(token);
            BarcodeRequest Items = new BarcodeRequest()
            {
                ItemsList = ItemsList
            };
            return View(Items);
        }

        [HttpPost]
        public async Task<IActionResult> BarcodeIndex(int id, CancellationToken token = default)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iBarcodeRepo.GenerateBarcode(id, token);
                    return Json(true);
                }
                var ItemsList = await _itemRepo.ItemsList(token);
                BarcodeRequest Items = new BarcodeRequest()
                {
                    ItemsList = ItemsList
                };
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("already Generated", Ex.Message);

                var ItemsList = await _itemRepo.ItemsList(token);
                BarcodeRequest Items = new BarcodeRequest()
                {
                    ItemsList = ItemsList
                };
                return Json(false);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItemsBarcode(BarcodeResponse model, CancellationToken token = default)
        {
            var result = await _iBarcodeRepo.GetAllGeneratedbarcodes(model, token);
            return View(result);
        }

        [HttpGet]
        public async Task<JsonResult> BarcodeLoad(CancellationToken token = default)
        {
            var AllData = await _iBarcodeRepo.GetAllItemsBarcode(token);

            return Json(AllData);
        }
        [HttpGet]
        public IActionResult BarcodeSearch(CancellationToken token = default)
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> HoldOrder(string orderedItems, decimal TotalAmount, string Holdkey, int CustomerId, CancellationToken token = default)
        {
            var userGUID = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
            var CompanyGUID = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");
            var branchGUID = _httpContextAccessor.HttpContext.Session.GetString("branchGUID");

            try
            {
                if (ModelState.IsValid)
                {

                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(orderedItems, typeof(DataTable));
                    await _iBarcodeRepo.holdOrderMaster(dt, TotalAmount, userGUID, branchGUID, CompanyGUID, Holdkey, CustomerId, token);

                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(nameof(orderedItems), Ex.Message);
                return Json(orderedItems);
            }

            return Json("Usman");
        }

        [HttpPost]
        public async Task<JsonResult> SaveOrder(string orderedItems, decimal TotalAmount, decimal CashAmount, decimal balance, int CustomerId, CancellationToken token = default)
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
            var CompanyId = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");
            var branchGUID = _httpContextAccessor.HttpContext.Session.GetString("branchGUID");

            try
            {
                if (ModelState.IsValid)
                {

                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(orderedItems, typeof(DataTable));
                    await _iBarcodeRepo.SaveOrderMaster(dt, TotalAmount, CashAmount, balance, userId, branchGUID, CompanyId, CustomerId, token);

                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(nameof(orderedItems), Ex.Message);
                return Json(orderedItems);
            }

            return Json("Usman");
        }

        [HttpPost]
        public async Task<int> SaveCustomer(string Name, string phoneNo, string Ntn, string Cnic, CancellationToken token = default)
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
            var CompanyId = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");
            var branchGUID = _httpContextAccessor.HttpContext.Session.GetString("branchGUID");

            try
            {
                if (ModelState.IsValid)
                {

                    var customerId = await _iBarcodeRepo.SaveCustomerDetails(Name, phoneNo, Ntn, Cnic, userId, branchGUID, CompanyId, token);
                    return customerId;
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(nameof(Name), Ex.Message);

            }

            return 1;
        }

        [HttpPost]
        public async Task<JsonResult> getWalkinCustomerData(string id, CancellationToken token = default)
        {
            var userGUID = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");

            var data = await _iBarcodeRepo.GetwalkingcustomerData(id, userGUID, token);

            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> getHoldOrder(string id, CancellationToken token = default)
        {
            var data = await _iBarcodeRepo.getHoldedOrder(id, token);

            string jsonObject = JsonConvert.SerializeObject(data);

            return Json(jsonObject);
        }

        [HttpPost]
        public async Task<JsonResult> getCustomer(int id, CancellationToken token = default)
        {
            var data = await _iBarcodeRepo.GetCustomerData(id, token);

            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> EditBarcode(int id, CancellationToken token = default)
        {
            var result = await _iBarcodeRepo.GetBarcode(id, token);
            var list = await _itemRepo.ItemsList(token);
            if (result == null)
            {
                return View("ItemNotFound", id);
            }
            BarcodeRequest barcodeResponse = new BarcodeRequest
            {
                ItemId = result.ItemId,
                GeneratedBarcodeId = result.ItemBarcode
                // ItemsList =  list

            };
            return View(barcodeResponse);
        }
 
        [HttpPost]
        public async Task<IActionResult> EditBarcode(BarcodeRequest model, CancellationToken token = default)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //  await _iBarcodeRepo.EditBarcode(model, token);
                    TempData["success"] = "Updated Successfylly";
                    return RedirectToAction("EditBarcode", "Barcode");
                }
                //  var ItemsList = await _iBarcodeRepo.ItemsList(token);
                BarcodeRequest Items = new BarcodeRequest()
                {
                    ItemId = model.ItemId
                };
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(nameof(model.ItemId), Ex.Message);

                var ItemsList = await _itemRepo.ItemsList(token);
                BarcodeRequest Items = new BarcodeRequest()
                {
                    // ItemsList = ItemsList
                };
                return View(Items);
            }
            return View();
        }
    }
}