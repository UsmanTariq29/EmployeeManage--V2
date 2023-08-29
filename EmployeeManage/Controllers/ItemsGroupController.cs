using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Request;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Controllers
{
    public class ItemsGroupController : Controller
    {

        private readonly IItemGroupRepo _itemGroupRepo;
        private readonly ITaxRepo _taxRepo;
        private readonly IItemRepo _itemRepo;
        private readonly IUnitOfMeasureRepo _unitofmeasure;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ItemsGroupController(IItemGroupRepo itemGroupRepo, IUnitOfMeasureRepo unitOfMeasureRepo, ITaxRepo taxRepo, IItemRepo itemRepo, IHttpContextAccessor httpContextAccessor)
        {
            _itemGroupRepo = itemGroupRepo;
            _taxRepo = taxRepo;
            _itemRepo = itemRepo;
            _unitofmeasure = unitOfMeasureRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult AddGroup()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup(Item_GroupRequest model, CancellationToken token = default)
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
            var CompanyId = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");
            try
            {
                if (ModelState.IsValid)
                {
                    await _itemGroupRepo.AddItemGroup(model, userId, CompanyId, token);
                    TempData["success"] = "Added Successfylly";
                    return RedirectToAction("AddGroup", "ItemsGroup");
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(nameof(model.ItemGroupName), Ex.Message);
                return View(model);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddItems(CancellationToken token = default)
        {
            var Grouplist = await _itemGroupRepo.GroupList(token);
            var Unitlist = await _unitofmeasure.UnitInCaseList(token);
            var taxlist = await _taxRepo.GetTaxListAsync(token);
            ItemsRequest Items = new ItemsRequest()
            {
                GroupList = Grouplist,
                UnitInCaseList = Unitlist,
                TaxList = taxlist
            };
            return View(Items);
        }

        [HttpPost]
        public async Task<IActionResult> AddItems(ItemsRequest model, CancellationToken token = default)
        {
            var UserId = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
            var CompanyId = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");

            if (ModelState.IsValid)
            {
                await _itemRepo.AddItem(model, UserId, CompanyId, token);
                TempData["success"] = "Added Successfylly";
                return RedirectToAction("AddItems", "ItemsGroup");
            }
            var Grouplist = await _itemGroupRepo.GroupList(token);
            var Unitlist = await _unitofmeasure.UnitInCaseList(token);
            var taxlist = await _taxRepo.GetTaxListAsync(token);
            ItemsRequest Items = new ItemsRequest()
            {
                GroupList = Grouplist,
                UnitInCaseList = Unitlist,
                TaxList = taxlist
            };
            return View();
        }

        [HttpGet]
        public IActionResult AddUnitOfMeasure()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUnitOfMeasure(UnitOfMeasureRequest model, CancellationToken token = default)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    await _unitofmeasure.AddUnitOfMeasure(model, token);
                    TempData["success"] = "Added Successfylly";
                    return RedirectToAction("AddUnitOfMeasure", "ItemsGroup");
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(nameof(model.unitOfMeasure), Ex.Message);
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddItemTax()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddItemTax(AddTaxRequestVM model, CancellationToken token = default)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
                    var CompanyGUID = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");
                    var branchGUID = _httpContextAccessor.HttpContext.Session.GetString("branchGUID");

                    await _taxRepo.CreateTaxAsync(model, userId, CompanyGUID, branchGUID, token);
                    TempData["success"] = "Added Successfylly";
                    return RedirectToAction("AddItemTax", "ItemsGroup");
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(nameof(model.taxId), Ex.Message);
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddItemsPrice(CancellationToken token = default)
        {
            var ItemsList = await _itemRepo.ItemsList(token);
            AdditemsPriceRequestVM Items = new AdditemsPriceRequestVM()
            {
                ItemsList = ItemsList

            };
            return View(Items);

        }

        [HttpPost]
        public async Task<IActionResult> AddItemsPrice(AdditemsPriceRequestVM model, CancellationToken token = default)
        {
            var UserId = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
            var CompanyId = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");

            if (ModelState.IsValid)
            {
                await _itemRepo.AddItemsPrice(model, UserId, CompanyId, token);
                TempData["success"] = "Added Successfylly";
                return RedirectToAction("AddItemsPrice", "ItemsGroup");
            }
            var itemslist = await _itemRepo.ItemsList(token);
            AdditemsPriceRequestVM Items = new AdditemsPriceRequestVM()
            {
                ItemsList = itemslist,
            };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> update(UpdateItemRequestVm model, CancellationToken token = default)
        {

            await _itemRepo.UpdateItemData(model, token);

            return RedirectToAction("AddItemTax", "ItemsGroup");
        }

        [HttpGet]
        public async Task<IActionResult> AllItems(CancellationToken token = default)
        {
            var itemresult = await _itemRepo.AllItemData(token);

            return View(itemresult);
        }

        [HttpGet]
        public async Task<ActionResult> EditItems(int id, CancellationToken token = default)
        {
            var result = await _itemRepo.GetItem(id, token);
            var price = await _itemRepo.GetItemprice(id, token);


            if (result == null)
            {
                return View("ItemNotFound", id);
            }
            var grouplist = await _itemGroupRepo.GroupList(token);
            var itemList = await _itemRepo.ItemsList(token);
            var UomList = await _unitofmeasure.UnitInCaseList(token);
            var taxlist = await _taxRepo.GetTaxListAsync(token);
            ItemsDataListsResponseVM itemUpdate = new ItemsDataListsResponseVM
            {
                ItemId = result.ItemId,
                ItemName = result.ItemName,
                ItemGroupId = result.ItemGroupId,
                ItemCode = result.ItemCode,
                IsExempted = result.IsExempted,
                IsActive = result.IsActive,
                ItemReOrder = result.ItemReOrder,
                IsBatchItem = result.IsBatchItem,
                IsRepalaceable = result.IsRepalaceable,
                IsExpiryAllowed = result.IsExpiryAllowed,
                UnitInCase = result.UnitInCase,
                TaxId = result.TaxId,
                ItemsPrice = price.ItemsPrice,
                unitOfMeasureId = result.UnitInCase,
                ItemsList = itemList,
                GroupList = grouplist,
                UOMList = UomList,
                taxList = taxlist
            };
            return View(itemUpdate);
        }
    }
}