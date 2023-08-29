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
    public class BarcodeRepo : IBarcodeRepo
    {

        private readonly EmployeesDBContext _db;

        public BarcodeRepo(EmployeesDBContext db)
        {
            _db = db;

        }
        private async Task<TblItem> ValidateRecord(int itemid, CancellationToken token = default)
        {
            var record = await _db.TblItems
                .AsNoTracking()
                .Where(x => x.ItemId == itemid)
                .FirstOrDefaultAsync(token);

            if (record == null)
            {
                throw new Exception("Record not found");

            }

            return record;
        }
        public async Task GenerateBarcode(int id, CancellationToken token = default)
        {
            await ValidateRecord(id, token);
            await ValidateBarcodeRecord(id, token);
            var barcode = new TblItemBarcode
            {
                ItemId = id,
                ItemBarcode = 12345 + id.ToString()
            };
            _db.TblItemBarcodes.Add(barcode);
            await _db.SaveChangesAsync(token);
        }
        public async Task<IEnumerable<BarcodeResponse>> GetAllGeneratedbarcodes(BarcodeResponse model, CancellationToken token = default)
        {
            var query = _db.TblItems
                      .Join(
                          _db.TblItemBarcodes,
                          item => item.ItemId,
                          barcode => barcode.ItemId,
                          (item, barcode) => new { item, barcode }
                      )
                      .Select(r => new BarcodeResponse
                      {
                          ItemId = r.item.ItemId,
                          ItemName = r.item.ItemName,
                          ItemBarcode = r.barcode.ItemBarcode
                      });
            return await query.ToListAsync(token);
        }

        private async Task<TblItemBarcode> ValidateBarcodeRecord(int itemid, CancellationToken token = default)
        {
            var record = await _db.TblItemBarcodes
                .AsNoTracking()
                .Where(x => x.ItemId == itemid)
                .FirstOrDefaultAsync(token);
            if (record != null)
            {
                throw new Exception("Record already Exists");
            }
            return record;
        }

        public async Task<BarcodeRequest> GetBarcodeitem(int itemid, CancellationToken token = default)
        {
            var record = await _db.TblItemBarcodes
                 .AsNoTracking()
                 .Where(x => x.ItemId == itemid)
                 .Select(r => new BarcodeRequest
                 {

                     ItemId = itemid,

                 })
                 .FirstOrDefaultAsync(token);

            return record;
        }

        public async Task<TblItemBarcode> GetBarcode(int id, CancellationToken token = default)
        {
            return await ValidateBarcode(id, token);
        }

        private async Task<TblItemBarcode> ValidateBarcode(int itemid, CancellationToken token = default)
        {
            var record = await _db.TblItemBarcodes
                .AsNoTracking()
                .Where(x => x.ItemId == itemid)
                .FirstOrDefaultAsync(token);

            if (record == null)
            {
                throw new Exception("Record not found");
            }
            return record;
        }
        private async Task<TblItemBarcode> ValidateBarcodeId(string barcodeId, CancellationToken token = default)
        {
            var record = await _db.TblItemBarcodes
                .AsNoTracking()
                .Where(x => x.ItemBarcode.Equals(barcodeId))
                .FirstOrDefaultAsync(token);

            if (record == null)
            {
                throw new Exception("Record not found");

            }

            return record;
        }
        public async Task<BarcodeResponse> SearchBarcode(string GeneratedBarcode, CancellationToken token = default)
        {
            await ValidateBarcodeId(GeneratedBarcode, token);

            var result = await _db.TblItemBarcodes
                .Join(
                _db.TblItems,
                barcode => barcode.ItemId,
                item => item.ItemId,
                (barcode, item) => new { barcode, item }
                ).AsNoTracking()
                .Where(x => x.barcode.ItemBarcode.Equals(GeneratedBarcode))
                .Select(r => new BarcodeResponse
                {
                    ItemId = r.barcode.ItemId,
                    ItemBarcode = r.barcode.ItemBarcode,
                    ItemName = r.item.ItemName
                }).FirstOrDefaultAsync(token);

            return result;
        }

        public async Task<IEnumerable<BarcodeResponse>> GetAllItemsBarcode(CancellationToken token = default)
        {
            var result = _db.TblItemBarcodes
                    .AsNoTracking()
                    .Join(
                _db.TblItems,
                barcode => barcode.ItemId,
                item => item.ItemId,
                (barcode, item) => new { barcode, item }
                )
               .Join(
                _db.TblItemPrices,
                data => data.barcode.ItemId,
                Price => Price.ItemId,
                (data, Price) => new { data, Price }
                )
               .Join(
                _db.TblTaxes,
                data => data.data.item.TaxId,
                tax => tax.TaxId,
                (data, tax) => new { data.data, data.Price, tax }
                )

                .Select(r => new BarcodeResponse
                {
                    ItemId = r.data.item.ItemId,
                    ItemName = r.data.item.ItemName,
                    ItemBarcode = r.data.barcode.ItemBarcode,
                    Price = r.Price.ItemsPrice,
                    Discount = r.data.item.Discount,
                    Name = r.tax.Name,
                    Percentage = r.tax.Percentage
                });
            return await result.ToListAsync(token);
        }

        public async Task SaveOrderMaster(DataTable orderItem, decimal TotalAmount, decimal CashAmount, decimal balance, string userId, string branchGUID,
            string companyId, int CustomerId, CancellationToken token = default)
        {
            await using var dbTransaction = await _db.Database.BeginTransactionAsync(token);

            try
            {
                var MasterData = new TblInvoiceMaster
                {
                    CreatedByUserGuid = userId,
                    CompanyGuid = companyId,
                    DateCreated = DateTime.Now,
                    BranchGuid = branchGUID,
                    TotalAmount = TotalAmount,
                    AmountReceived = CashAmount,
                    Balance = balance,
                    CustomerId = CustomerId
                };

                _db.TblInvoiceMasters.Add(MasterData);

                await _db.SaveChangesAsync(token);
                await SaveOrderDetail(orderItem, MasterData.InvoiceMasterId, token);

                await dbTransaction.CommitAsync(token);

            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync(token);
                throw ex;
            }
        }

        private async Task SaveOrderDetail(DataTable orderItem, int masterId, CancellationToken token = default)
        {
            foreach (DataRow dr in orderItem.Rows)
            {
                var invoiceDetail = new TblInvoiceDetail
                {
                    ItemId = Convert.ToInt32(dr["ID"]),
                    Price = Convert.ToDecimal(dr["PRICE"]),
                    Quantity = Convert.ToDecimal(dr["QTY"]),
                    Amount = Convert.ToDecimal(dr["AMOUNT"]),
                    InvoiceMasterId = masterId
                };
                _db.TblInvoiceDetails.Add(invoiceDetail);

                await _db.SaveChangesAsync(token);

            }

        }
        private async Task SaveHoldOrderDetail(DataTable orderItem, int masterId, CancellationToken token = default)
        {
            foreach (DataRow dr in orderItem.Rows)
            {

                var invoiceDetail = new TblHoldOrderDetail
                {
                    ItemId = Convert.ToInt32(dr["ID"]),
                    Price = Convert.ToDecimal(dr["PRICE"]),
                    Quantity = Convert.ToDecimal(dr["QTY"]),
                    Amount = Convert.ToDecimal(dr["AMOUNT"]),
                    HoldOrderMasterId = masterId
                };
                _db.TblHoldOrderDetails.Add(invoiceDetail);

                await _db.SaveChangesAsync(token);

            }

        }

        public async Task<int> SaveCustomerDetails(string name, string PhoneNo, string NTN, string Cnic, string userGUID, string CompanyGUID, string BranchGUID, CancellationToken token = default)
        {
            var customerdetails = new TblCustomer
            {
                CustomerName = name,
                PhoneNumber = PhoneNo,
                Ntnnumber = NTN,
                Cnicnumber = Cnic,
                CreatedByUserGuid = userGUID,
                CompanyGuid = CompanyGUID,
                BranchGuid = BranchGUID
            };
            _db.TblCustomers.Add(customerdetails);

            await _db.SaveChangesAsync(token);
            return customerdetails.CustomerId;
        }

        public async Task<CustomerResponse> GetCustomerData(int id, CancellationToken token = default)
        {
            var result = _db.TblCustomers
                        .Where(x => x.CustomerId == id)
                    .AsNoTracking()
                .Select(r => new CustomerResponse
                {
                    CustomerName = r.CustomerName,
                    PhoneNumber = r.PhoneNumber
                }).FirstOrDefaultAsync(token);

            return await result;
        }
        public async Task<IList<UnholdOrderResponse>> getHoldedOrder(string id, CancellationToken token = default)
        {
            var result = await _db.TblHoldOrderMasters
                        .Where(x => x.OrderId == id)
                        .AsNoTracking()
                         .Join(
                          _db.TblHoldOrderDetails,
                          master => master.HoldInvoiceMasterId,
                          detail => detail.HoldOrderMasterId,
                          (master, detail) => new { master, detail }
                      )
                         .Join(
                          _db.TblItems,
                          data => data.detail.ItemId,
                          item => item.ItemId,
                          (data, item) => new
                          {
                              data,
                              data.master,
                              data.detail,
                              item
                          }
                      )
                         .Join(
                          _db.TblCustomers,
                          data => data.master.CustomerId,
                          customer => customer.CustomerId,
                          (data, customer) => new
                          {
                              data,
                              data.master,
                              data.detail,
                              data.item,
                              customer

                          }
                      )
                         .Join(
                          _db.TblItemBarcodes,
                          data => data.item.ItemId,
                          barcode => barcode.ItemId,
                          (data, barcode) => new
                          {
                              data,
                              data.master,
                              data.detail,
                              data.item,
                              data.customer,
                              barcode

                          }
                      )
                .Select(r => new UnholdOrderResponse
                {
                    CustomerId = r.master.CustomerId,
                    CustomerName = r.customer.CustomerName,
                    PhoneNumber = r.customer.PhoneNumber,
                    TotalAmount = r.master.TotalAmount,
                    DateCreated = r.master.DateCreated,
                    HoldOrderId = r.master.OrderId,
                    ItemId = r.detail.ItemId,
                    ItemBarcode = r.barcode.ItemBarcode,
                    ItemName = r.item.ItemName,
                    Quantity = r.detail.Quantity,
                    Price = r.detail.Price,
                    Amount = r.detail.Amount
                }).ToListAsync(token);

            return result;
        }
        public async Task<CustomerResponse> GetwalkingcustomerData(string id, string userGUID, CancellationToken token = default)
        {
            var result = _db.TblCustomers
                        .Where(x => x.CreatedByUserGuid == userGUID)
                        .Where(x => x.CustomerCode == id)
                        .AsNoTracking()
                .Select(r => new CustomerResponse
                {
                    CustomerName = r.CustomerName,
                    PhoneNumber = r.PhoneNumber,
                    CustomerId = r.CustomerId

                }).FirstOrDefaultAsync(token);

            return await result;
        }

        public async Task holdOrderMaster(DataTable orderItems, decimal TotalAmount, string userId, string branchGUID, string CompanyId, string Holdkey, int CustomerId, CancellationToken token = default)
        {
            await using var dbTransaction = await _db.Database.BeginTransactionAsync(token);

            try
            {
                var HoldMasterData = new TblHoldOrderMaster
                {
                    CustomerId = CustomerId,
                    OrderId = Holdkey,
                    CreatedByUserGuid = userId,
                    CompanyGuid = CompanyId,
                    DateCreated = DateTime.Now,
                    BranchGuid = branchGUID,
                    TotalAmount = TotalAmount,

                };
                _db.TblHoldOrderMasters.Add(HoldMasterData);

                await _db.SaveChangesAsync(token);
                await SaveHoldOrderDetail(orderItems, HoldMasterData.HoldInvoiceMasterId, token);

                await dbTransaction.CommitAsync(token);

            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync(token);
                throw ex;
            }

        }
    }
}