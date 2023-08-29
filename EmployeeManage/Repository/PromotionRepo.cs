using EmployeeManage.Models;
using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Request;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository
{
    public class PromotionRepo : IPromotionRepo
    {
        private readonly EmployeesDBContext _db;

        public PromotionRepo(EmployeesDBContext db)
        {
            _db = db;

        }

        public async Task<IEnumerable<SelectListItem>> PromotionTypeList(CancellationToken token = default)
        {
            var data = await _db.TblPromotionTypes.ToListAsync(token);

            List<SelectListItem> list = data
                .OrderBy(n => n.PromotionType)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.PromotionTypeId.ToString(),
                        Text = n.PromotionType
                    }).ToList();
            var typelist = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Type"
            };
            list.Insert(0, typelist);

            return new SelectList(list, "Value", "Text");
        }

        private TblPromotionMaster ValidatePromotion(DateTime startTime, DateTime endDate)
        {
            var record = _db.TblPromotionMasters
                .AsNoTracking()
                .Where(x => x.PromotionStartDate >= startTime && x.PromotionEndDate <= endDate )
                .FirstOrDefault();

            if (record != null)
            {
                throw new Exception("Promotion Dates Already Filled");
            }

            return record;
        }
        public async Task CreatePromotion(PromotionRequestVM model,DataTable promotionItems, string userGUID, string branchGUID, string companyGUID,
            CancellationToken token = default)
        {
            await using var dbTransaction = await _db.Database.BeginTransactionAsync(token);



            try
            {
                ValidatePromotion(model.PromotionStartDate, model.PromotionEndDate);

                var promotionMaster = new TblPromotionMaster
                {
                    PromotionStartDate = model.PromotionStartDate,
                    PromotionEndDate = model.PromotionEndDate,
                    PromotionName = model.PromotionName,
                    PromotionDescription = model.Description,
                    PromotionType = 2,
                    PromotionPercentage =1,
                    CreatedByUserGuid = userGUID,
                    CompanyGuid = companyGUID,
                    BranchGuid = branchGUID
                };

                _db.TblPromotionMasters.Add(promotionMaster);

                await _db.SaveChangesAsync(token);

                await SavePromotionDetail(promotionItems, promotionMaster.PromotionId, token);

                await dbTransaction.CommitAsync(token);
            }

            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync(token);
                throw ex;
            }
        }

        private async Task SavePromotionDetail(DataTable promotionitems, int masterId, CancellationToken token = default)
        {
            foreach (DataRow dr in promotionitems.Rows)
            {
                var Promotiondetail = new TblPromotionDetail
                {
                    ItemId = Convert.ToInt32(dr["ID"]),
                    PromotionPercentage = Convert.ToDecimal(dr["PERCENTAGE"]),
                    PromotionMasterId = masterId

                };

                _db.TblPromotionDetails.Add(Promotiondetail);

                await _db.SaveChangesAsync(token);

            }

        }

        public async Task<IEnumerable<PromotionItemsResponse>> GetItems(int itemgrpid, CancellationToken token)
        {
            var result = await _db.TblItems.Where(x => x.ItemGroupId == itemgrpid)
               .AsNoTracking()
               .Join(
                _db.TblItemPrices,
               data => data.ItemId,
               price => price.ItemId,
               (data, price) => new { data, price }
               )
               .Select(r => new PromotionItemsResponse
               {
                   ItemId = r.data.ItemId,
                   ItemName = r.data.ItemName,
                   ItemsPrice = r.price.ItemsPrice,
                   ItemTaxid = r.data.TaxId
               })
               .ToListAsync(token);

            return  result;

        }
    }
} 





        