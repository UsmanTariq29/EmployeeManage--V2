using EmployeeManage.Models;
using EmployeeManage.Repository.Interface;
using EmployeeManage.Utilities;
using EmployeeManage.ViewModels.Request;
using EmployeeManage.ViewModels.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManage.Repository
{
    public class PurchaseRepo : IPurchaseRepo
    {
        private readonly EmployeesDBContext _db;
        private readonly IVoucherHelperRepo _voucherHelperRepo;


        public PurchaseRepo(EmployeesDBContext db, IVoucherHelperRepo voucherHelperRepo)
        {
            _db = db;
            _voucherHelperRepo = voucherHelperRepo;
        }


        public async Task SavePurchase(purchaseRequest model, DataTable purchasedItems, string userGUID,
            string branchGUID, string companyGUID, CancellationToken token = default)
        {
           var result =  await _voucherHelperRepo.CreateVoucherDisplayNumber((int)CodesEnum.PurchaseInvoiceEnum.PurchaseInvoice ,"PUR",branchGUID,companyGUID,token);

            await using var dbTransaction = await _db.Database.BeginTransactionAsync(token);

            try
            {
                var PurchaseMaster = new TblPurchaseMaster
                {
                    SupplierId = model.SupplierId,
                    DateCreated = System.DateTime.Today,
                    CreatedByUserGuid = userGUID,
                    CompanyGuid = companyGUID,
                    BranchGuid = branchGUID,
                    TotalDiscount = model.TotalDiscount,
                    NetAmount = model.NetAmount,
                    TotalAmount = model.TotalAmount,
                    TotalTax = model.TotalTax,
                    InvoiceNo = model.InvoiceNo,
                    Description = model.Description,
                    StatusId = 1,
                    VoucherDisplayNo = result.VoucherDisplayNumber
                };
                _db.TblPurchaseMasters.Add(PurchaseMaster);
                await _db.SaveChangesAsync(token);

                await InsertPurchaseDetail(purchasedItems, PurchaseMaster.PurchaseMasterId, token);

                await dbTransaction.CommitAsync(token);
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync(token);
                throw ex;
            }
        }
        private async Task InsertPurchaseDetail(DataTable purchasedItems, int masterId, CancellationToken token = default)
        {
            foreach (DataRow dr in purchasedItems.Rows)
            {
                var Purchasedetail = new TblPurchaseDetail
                {
                    ItemId = Convert.ToInt32(dr["ItemId"]),
                    Quantity = Convert.ToInt32(dr["ItemQuantity"]),
                    Price = Convert.ToDecimal(dr["ItemPRICE"]),
                    PurchaseMasterId = masterId,
                    Amount = Convert.ToDecimal(dr["ItemAmount"]),
                    FixedDiscount = Convert.ToDecimal(dr["ItemFixedDiscount"]),
                    Percentage = Convert.ToDecimal(dr["ItemTax"]),
                    AmountAfterDiscount = Convert.ToDecimal(dr["DiscountedAmount"]),
                    DiscountPercentage = Convert.ToDecimal(dr["ItemsPercentageDiscount"]),
                    ItemGroupId = Convert.ToInt32(dr["ItemGroupId"]),
                    ItemTaxId = Convert.ToInt32(dr["TaxId"]),
                    Description = Convert.ToString(dr["Description"])
                };

                _db.TblPurchaseDetails.Add(Purchasedetail);
                await _db.SaveChangesAsync(token);
            }
        }

        public async Task UpdatePurchase(purchaseRequest model, DataTable purchasedItems, string userGUID, string branchGUID,
            string companyGUID, CancellationToken token = default)
        {
            await using var dbTransaction = await _db.Database.BeginTransactionAsync(token);

            try
            {
                var record = await ValidatePurchaseMaster(model.PurchaseMasterId, branchGUID, companyGUID, token);

                record.SupplierId = model.SupplierId;
                record.TotalDiscount = model.TotalDiscount;
                record.NetAmount = model.NetAmount;
                record.TotalAmount = model.TotalAmount;
                record.TotalTax = model.TotalTax;
                record.InvoiceNo = model.InvoiceNo;
                record.Description = model.Description;

                _db.TblPurchaseMasters.Update(record);
                await _db.SaveChangesAsync(token);

                await RemovePurchaseDetailAsync(model.PurchaseMasterId, token);
                await InsertPurchaseDetail(purchasedItems, model.PurchaseMasterId, token);

                await dbTransaction.CommitAsync(token);
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync(token);
                throw ex;
            }
        }


        private async Task<TblPurchaseMaster> ValidatePurchaseMaster(int purchaseMasterId, string branchGUID, string companyGUID,
            CancellationToken token = default)
        {
            var record = _db.TblPurchaseMasters
                .AsNoTracking()
                .Where(x => x.PurchaseMasterId == purchaseMasterId)
                .Where(x => x.CompanyGuid.Equals(companyGUID));

            if (!string.IsNullOrEmpty(branchGUID))
            {
                record = record.Where(x => x.BranchGuid.Equals(branchGUID));
            }

            var result = record.FirstOrDefaultAsync(token);

            return await result;
        }
        private async Task<TblPurchaseMaster> ValidatePurchaseMasterId(int purchaseMasterID, string branchGUID, string companyGUID,
            CancellationToken token = default)
        {
            var record = _db.TblPurchaseMasters
                .AsNoTracking()
                .Where(x => x.PurchaseMasterId == purchaseMasterID)
                .Where(x => x.CompanyGuid.Equals(companyGUID));

            if (!string.IsNullOrEmpty(branchGUID))
            {
                record = record.Where(x => x.BranchGuid.Equals(branchGUID));
            }

            var result = record.FirstOrDefaultAsync(token);

            return await result;
        }
        public async Task RemovePurchaseDetailAsync(int masterId, CancellationToken token = default)
        {
            var records = await _db.TblPurchaseDetails
                .AsNoTracking()
                .Where(f => f.PurchaseMasterId == masterId)
                .Take(200)
                .ToListAsync(token);

            if (records.Count > 0)
            {
                _db.TblPurchaseDetails.RemoveRange(records);
                await _db.SaveChangesAsync(token);
            }
        }

        public async Task<IEnumerable<AllPurchaseResponseVM>> GetAllPurchase(CancellationToken token = default)
        {
            var query = _db.TblPurchaseMasters
                      .Join(
                          _db.TblSuppliers,
                          data => data.SupplierId,
                          supplier => supplier.SupplierId,
                          (data, supplier) => new { data, supplier }
                      ).OrderByDescending(n => n.data.DateCreated)
                      .ThenByDescending(n => n.data.PurchaseMasterId)
                      .Where(x => x.data.StatusId == 1)
                      .Select(r => new AllPurchaseResponseVM
                      {
                          PurchaseMasterId = r.data.PurchaseMasterId,
                          SupplierId = r.data.SupplierId,
                          SupplierName = r.supplier.Name,
                          DateCreated = r.data.DateCreated,
                          InvoiceNo = r.data.InvoiceNo,
                          MasterDescription = r.data.Description

                      });
            return await query.ToListAsync(token);
        }
        public async Task<IEnumerable<AllPurchaseResponseVM>> GetAllPurchasePayment(CancellationToken token = default)
        {
            var query = _db.TblPurchaseMasters
                      .Join(
                          _db.TblSuppliers,
                          data => data.SupplierId,
                          supplier => supplier.SupplierId,
                          (data, supplier) => new { data, supplier }
                      ).OrderByDescending(n => n.data.DateCreated)
                      .ThenByDescending(n => n.data.PurchaseMasterId)
                      .Where(x => x.data.StatusId == 2)
                      .Select(r => new AllPurchaseResponseVM
                      {
                          PurchaseMasterId = r.data.PurchaseMasterId,
                          SupplierId = r.data.SupplierId,
                          SupplierName = r.supplier.Name,
                          DateCreated = r.data.DateCreated,
                          InvoiceNo = r.data.InvoiceNo,
                          MasterDescription = r.data.Description,
                          Amount = r.data.NetAmount,
                          TotalAmount = r.data.TotalAmount,
                          TotalDiscount = r.data.TotalDiscount,
                          TotalTax = r.data.TotalTax

                      });
            return await query.ToListAsync(token);
        }

        public async Task<IEnumerable<AllPurchaseResponseVM>> GetPurchaseById(int purchaseMasterID, CancellationToken token = default)
        {

            await ValidateRecord(purchaseMasterID, token);
            var data = await GetDetailsPurchase(purchaseMasterID, token);
            return data;
        }


        //public async Task<TblPurchaseMaster> GetPurchase(int id, CancellationToken token = default)
        //{
        //    return await ValidateRecord(id, token);
        //}


        private async Task<TblPurchaseMaster> ValidateRecord(int purchaseMasterId, CancellationToken token = default)
        {
            var record = _db.TblPurchaseMasters
                .AsNoTracking()
                .Where(x => x.PurchaseMasterId == purchaseMasterId)
                .FirstOrDefaultAsync(token);

            if (record == null)
            {
                throw new Exception("Record not found");
            }

            return await record;
        }
        public async Task<TblPurchaseDetail> GetPurchaseDetails(int id, CancellationToken token = default)
        {
            return await ValidatedetailRecord(id, token);
        }

        private async Task<TblPurchaseDetail> ValidatedetailRecord(int id, CancellationToken token = default)
        {
            var record = _db.TblPurchaseDetails
                .AsNoTracking()
                .Where(x => x.PurchaseMasterId == id)
                .FirstOrDefaultAsync(token);

            if (record == null)
            {
                throw new Exception("Detail Record not found");
            }

            return await record;
        }

        public async Task<IEnumerable<AllPurchaseResponseVM>> GetDetailsPurchase(int purchaseMasterId,
            CancellationToken token)
        {
            var query = _db.TblPurchaseDetails
                           .Where(x => x.PurchaseMasterId == purchaseMasterId)
                           .
                           Join(
                           _db.TblPurchaseMasters,
                           Pd => Pd.PurchaseMasterId,
                           PM => PM.PurchaseMasterId,
                           (Pd, PM) => new { Pd, PM }
                           )
                          .Join(
                           _db.TblItems,
                           data => data.Pd.ItemId,
                           item => item.ItemId,
                           (data, item) => new { data, item }
                           )
                          .Join(
                           _db.TblSuppliers,
                           data => data.data.PM.SupplierId,
                           supplier => supplier.SupplierId,
                           (data, supplier) => new { data, supplier }
                           )
                 .Select(r => new AllPurchaseResponseVM
                 {

                     SupplierId = r.supplier.SupplierId,
                     SupplierName = r.supplier.Name,
                     MasterDescription = r.data.data.Pd.Description,
                     InvoiceNo = r.data.data.PM.InvoiceNo,
                     ItemGroupId = r.data.data.Pd.ItemGroupId,
                     DetailDescription = r.data.data.PM.Description,
                     ItemId = r.data.item.ItemId,
                     ItemName = r.data.item.ItemName,
                     Quantity = r.data.data.Pd.Quantity,
                     Price = r.data.data.Pd.Price,
                     Amount = r.data.data.Pd.Amount,
                     DiscountPercentage = r.data.data.Pd.DiscountPercentage,
                     FixedDiscount = r.data.data.Pd.FixedDiscount,
                     AmountAfterDiscount = r.data.data.Pd.AmountAfterDiscount,
                     TotalAmount = r.data.data.PM.TotalAmount,
                     TotalDiscount = r.data.data.PM.TotalDiscount,
                     TotalTax = r.data.data.PM.TotalTax,
                     PurchaseMasterId = r.data.data.Pd.PurchaseMasterId,
                     ItemTaxId = r.data.data.Pd.ItemTaxId,


                 });
            return await query.ToListAsync(token);
        }

        public async Task SuperVision(int purchaseMasterID, string userGUID, string branchGUID, string companyGUID, CancellationToken token = default)
        {
            var record = await ValidatePurchaseMasterId(purchaseMasterID, branchGUID, companyGUID, token);

                record.StatusId = 2;
            
            _db.TblPurchaseMasters.Update(record);
            await _db.SaveChangesAsync(token);
        }
        public async Task Clearence(int purchaseMasterID, string PurchaseNumber, decimal cash, decimal Account, decimal Bank, decimal Check, string userGUID, string branchGUID, string companyGUID,
           CancellationToken token = default)
        {
            var record = await ValidatePurchaseMasterId(purchaseMasterID, branchGUID, companyGUID, token);

            record.StatusId = 3;
            
            _db.TblPurchaseMasters.Update(record);
            await _db.SaveChangesAsync(token);
        }

        public async Task<VoucherDisplayNumberResponseVM> VoucherDisplayNo(int purchaseMasterID, string branchGUID, string companyGUID, CancellationToken token = default)
        {
            await ValidatePurchaseMasterId(purchaseMasterID, branchGUID, companyGUID, token);
            var record = _db.TblPurchaseMasters
                 .AsNoTracking()
                 .Where(x => x.PurchaseMasterId == purchaseMasterID)
                 .Select(r => new VoucherDisplayNumberResponseVM
                 {
                     VoucherDisplayNumber = r.VoucherDisplayNo

                 }).FirstOrDefaultAsync(token);


            if (record == null)
            {
                throw new Exception("Detail Record not found");
            }

            return await record;
        }
    }
}
