using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Controllers
{
    public class PromotionController : Controller
    {
        private readonly IPromotionRepo _promotionRepo;
        private readonly IItemGroupRepo _itemGroupRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PromotionController(IPromotionRepo promotionRepo, IItemGroupRepo itemGroupRepo, IHttpContextAccessor httpContextAccessor)
        {
            _promotionRepo = promotionRepo;
            _itemGroupRepo = itemGroupRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Promotion(CancellationToken token = default)
        {

            var Promotiontypelist = await _promotionRepo.PromotionTypeList(token);
            var ItemGroup = await _itemGroupRepo.GroupList(token);

            AddPromotionRequestVM type = new AddPromotionRequestVM()
            {
                PromotionTypeList = Promotiontypelist,
                ItemGroupList = ItemGroup
            };

            return View(type);
        }

        [HttpPost]
        public async Task<IActionResult> Promotion(PromotionRequestVM model, string promotionItems, CancellationToken token = default)
        {

            try
            {
                var userGUID = _httpContextAccessor.HttpContext.Session.GetString("UserGUID");
                var companyGUID = _httpContextAccessor.HttpContext.Session.GetString("companyGUID");
                var branchGUID = _httpContextAccessor.HttpContext.Session.GetString("branchGUID");

                DataTable dt = (DataTable)JsonConvert.DeserializeObject(promotionItems, typeof(DataTable));

                if (ModelState.IsValid)
                {
                    await _promotionRepo.CreatePromotion(model, dt, userGUID, branchGUID, companyGUID, token);

                    return Json(true);
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(nameof(token), Ex.Message);
                TempData["Exception"] = "Promotion Dates Already Filled";
                return Json(false);
            }
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> getitemsData(int id, CancellationToken token = default)
        {
            var AllData = await _promotionRepo.GetItems(id, token);

            return Json(AllData);
        }
    }
}