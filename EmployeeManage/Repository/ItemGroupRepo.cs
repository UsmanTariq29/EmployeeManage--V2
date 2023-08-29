using EmployeeManage.Models;
using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Request;
using EmployeeManage.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository
{
    public class ItemGroupRepo : IItemGroupRepo
    {
        private readonly EmployeesDBContext _db;

        public ItemGroupRepo(EmployeesDBContext db)
        {
            _db = db;
            
        }


        public async Task AddItemGroup(Item_GroupRequest model, string userId, string CompanyId, CancellationToken  token= default)
        {
            await ValidateGroup(model.ItemGroupName,token);
            var itemGroup = new TblItemGroup
            {
                ItemGroupName = model.ItemGroupName,
                ItemCreatedDateTime = DateTime.Today.Date,
                UserGuId = userId,
                CompanyGuid = CompanyId
            };
           
            _db.TblItemGroups.Add(itemGroup);
            await _db.SaveChangesAsync(token);
        }

        public async Task<IEnumerable<SelectListItem>> GroupList(CancellationToken token = default)
        {
            var data = await _db.TblItemGroups.ToListAsync(token);

            List<SelectListItem> group = data
                .OrderBy(n => n.ItemGroupName)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.ItemGroupId.ToString(),
                        Text = n.ItemGroupName
                    }).ToList();

            var grplist = new SelectListItem()
            {
                Value = null,
                Text = "Please Select Group"
            };
            group.Insert(0, grplist);

            return new SelectList(group, "Value", "Text");

        }

        public async Task<TblItemGroup> ValidateGroup(string GroupName, CancellationToken token = default)
        {
            var record = await _db.TblItemGroups
                .AsNoTracking()
                .Where(x => x.ItemGroupName.Equals(GroupName))
                .FirstOrDefaultAsync(token);
           
            if ( record != null)
            {
                throw new Exception("Group Name Already Exists");
            }
            return  record;
        }



      
    }
}