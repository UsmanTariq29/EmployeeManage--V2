using EmployeeManage.Models;
using EmployeeManage.ViewModels.Request;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using EmployeeManage.ViewModels.Responses;
using EmployeeManage.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmployeeManage.Repository
{
    public class ItemRepo : IItemRepo
    {
        private readonly EmployeesDBContext _db;

        public ItemRepo(EmployeesDBContext db)
        {
            _db = db;

        }


        public async Task<List<TblItem>> getItems(CancellationToken token = default)
        {
            return await _db.TblItems.ToListAsync(token);
        }

        public async Task AddItem(ItemsRequest model, string userGUID, string CompanyId, 
            CancellationToken token = default)
        {

            var item = new TblItem
            {
                ItemCode = model.ItemCode,
                ItemName = model.ItemName,
                IsExempted = model.IsExempted,
                IsActive = model.IsActive,
                ItemDateTime = DateTime.Today.Date,
                ItemReOrder = model.ItemReorder,
                ItemGroupId = model.ItemGroupId,
                IsBatchItem = model.IsBatchItem,
                IsRepalaceable = model.IsReplaceable,
                IsExpiryAllowed = model.IsExpiryAllowed,
                UnitInCase = model.UnitInCase,
                UserGuId = userGUID,
                TaxId = model.TaxId,
                CompanyGuid = CompanyId
            };

            _db.TblItems.Add(item);
            await _db.SaveChangesAsync(token);
        }


        public async Task<IEnumerable<SelectListItem>> ItemsList(CancellationToken token = default)
        {
            var data = await getItems(token);

            List<SelectListItem> itemslist = data
                .OrderBy(n => n.ItemName)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.ItemId.ToString(),
                        Text = n.ItemName
                    }).ToList();
            var Itemslist = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Items"
            };
            itemslist.Insert(0, Itemslist);
            return new SelectList(itemslist, "Value", "Text");

        }


        public async Task<TblItem> GetItem(int Id, CancellationToken token = default)
        {
            return await ValidateRecord(Id, token);
        }

        public async Task AddItemsPrice(AdditemsPriceRequestVM model, string UserGUID, string CompanyGUID, CancellationToken token = default)
        {
            var data = new TblItemPrice
            {
                ItemId = model.ItemId,
                ItemsPrice = model.ItemsPrice,
                UserGuid = UserGUID,
                CompanyGuid = CompanyGUID
            };
            Serilog.Log.Information(LoggerMessageClass.ItemPrice.ItemPriceInserted, data);
            _db.TblItemPrices.Add(data);
            await _db.SaveChangesAsync(token);
        }
        public async Task UpdateItemData(UpdateItemRequestVm model, CancellationToken token = default)
        {
            var record = await GetItem(model.ItemId, token);
            record.ItemId = model.ItemId;
            record.ItemGroupId = model.ItemGroupId;
            record.ItemName = model.ItemName;

            record.ItemCode = model.ItemCode;
            record.ItemName = model.ItemName;
            record.IsExempted = model.IsExempted;
            record.IsActive = model.IsActive;
            record.ItemReOrder = model.ItemReOrder;
            record.ItemGroupId = model.ItemGroupId;
            record.IsBatchItem = model.IsBatchItem;
            record.IsRepalaceable = model.IsRepalaceable;
            record.IsExpiryAllowed = model.IsExpiryAllowed;
            record.UnitInCase = model.UnitInCase;
            record.TaxId = model.TaxId;

            //  var a = ValidateRecord(model.EmployeeID);

            _db.TblItems.Update(record);
            await _db.SaveChangesAsync(token);
        }
        public async Task<IEnumerable<ItemsDataListsResponseVM>> AllItemData(CancellationToken token = default)
        {
            var query = await _db.TblItems
                .Join(_db.TblItemGroups,

                item => item.ItemGroupId,
                group => group.ItemGroupId,
                (item, group) => new { item, group }
                )
                .Join(_db.TblUnitOfMeasures,
                data => data.item.UnitInCase,
                uom => uom.UnitOfMeasureId,
                (data, uom) => new
                {
                    data,
                    data.item,
                    data.group,
                    uom
                }
                )
                .Join(_db.TblItemPrices,
                data => data.item.ItemId,
                price => price.ItemId,
                (data, price) => new
                {
                    data,
                    data.item,
                    data.uom,
                    data.group,
                    price
                }
                )
                .Select(r => new ItemsDataListsResponseVM
                {
                    ItemId = r.item.ItemId,
                    unitOfMeasure = r.uom.UnitOfMeasure,
                    ItemGroupName = r.group.ItemGroupName,
                    ItemsPrice = r.price.ItemsPrice,
                    UnitOfMeasureDescription = r.uom.UnitOfMeasureDescription,
                    ItemName = r.item.ItemName
                }).ToListAsync(token);

            return query;
        }






        private async Task<TblItem> ValidateRecord(int itemid, CancellationToken token = default)
        {
            var record = _db.TblItems
                .AsNoTracking()
                .Where(x => x.ItemId == itemid)
                .FirstOrDefaultAsync(token);

            if (record == null)
            {
                throw new Exception("Record not found");

            }

            return await record;
        }

        public async Task<TblItemPrice> GetItemprice(int id, CancellationToken token = default)
        {
            return await validteItemPrice(id, token);
        }

        private async Task<TblItemPrice> validteItemPrice(int itemid, CancellationToken token = default)
        {
            var record = await _db.TblItemPrices
                .AsNoTracking()
                .Where(x => x.ItemId == itemid)
                .FirstOrDefaultAsync(token);

            if (record == null)
            {
                throw new Exception("Price not found");
            }

            return record;
        }
    
    

    
    
    
    }

}
