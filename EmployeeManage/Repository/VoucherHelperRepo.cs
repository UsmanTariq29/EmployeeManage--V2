using System.Threading.Tasks;
using System.Threading;
using System;
using EmployeeManage.Repository.Interface;
using EmployeeManage.ViewModels.Responses;
using EmployeeManage.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Configuration;
using EmployeeManage.ViewModels.Request;
using System.Data;

namespace EmployeeManage.Repository
{
    public class VoucherHelperRepo : IVoucherHelperRepo
    {
        private readonly EmployeesDBContext _db;
        private readonly IConfiguration _config;

        public VoucherHelperRepo(EmployeesDBContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<VoucherDisplayNumberResponseVM> CreateVoucherDisplayNumber(int voucherTypeId,
           string voucherTypeShortName, string branchGUID, string companyGUID, CancellationToken token = default)
        {
            var tblBranch = await _db.TblBranches
                .AsNoTracking()
                .Where(f => f.BranchGuid.Equals(branchGUID))
                .Where(f => f.CompanyGuid.Equals(companyGUID))
                .FirstOrDefaultAsync(token);

            if (tblBranch == null)
            {
                throw new Exception("ERROR_RECORD_NOT_FOUND");
            }

            //?? throw new APIException(ErrorCodesEnum.ERROR_RECORD_NOT_FOUND,
            //StatusCodes.Status404NotFound);


            // Record exists
            var record = await _db.TblVoucherDisplayNumbers
                .Where(f => f.VoucherTypeId == voucherTypeId)
                .Where(f => f.BranchGuid.Equals(branchGUID))
                .Where(f => f.CompanyGuid.Equals(companyGUID))
                .FirstOrDefaultAsync(token);

            var newVrNoDisplay = 1;

            if (record == null)
            {
                var tblVoucherDisplayNumber = new TblVoucherDisplayNumber
                {
                    VoucherTypeId = voucherTypeId,
                    VoucherDisplayNumber = newVrNoDisplay,
                    BranchGuid = branchGUID,
                    CompanyGuid = companyGUID,
                };
                _db.TblVoucherDisplayNumbers.Add(tblVoucherDisplayNumber);
            }
            else
            {
                newVrNoDisplay = record.VoucherDisplayNumber + 1;
                record.VoucherDisplayNumber = newVrNoDisplay;

                _db.TblVoucherDisplayNumbers.Update(record);
            }

            await _db.SaveChangesAsync(token);

            var voucherLength = _config.GetValue<int>("Voucher:Length");

            var svrNoDisplayS = VoucherZeroPadding(_config.GetValue<string>("Voucher:Padding"), voucherLength,
                voucherTypeShortName, tblBranch.BranchShortName);

            var newVrNoDisplayS = svrNoDisplayS.Substring(0, svrNoDisplayS.Length - voucherLength);
            var replicate = new string('0', (voucherLength - newVrNoDisplay.ToString().Length)) + newVrNoDisplay;

            newVrNoDisplayS += replicate;

            return new VoucherDisplayNumberResponseVM
            {
                VoucherNumber = newVrNoDisplay,
                VoucherDisplayNumber = newVrNoDisplayS,
                MasterReferenceNumber = MasterReferenceNumber()
            };
        }


        private static string MasterReferenceNumber()
        {
            var strMasterRefNo = DateTime.UtcNow.ToString("yyMMddHHmmsss");
            strMasterRefNo += DateTime.UtcNow.Millisecond.ToString("000");

            return strMasterRefNo;
        }

        private static string VoucherZeroPadding(string strPadding, int padLength, string voucherType,
            string branchShortName)
        {
            var dt = DateTime.UtcNow.Date;
            var fyscalYear = Convert.ToString(dt.Year)[2..];
            const char pad = '0';

            return branchShortName + "." + voucherType + "." + fyscalYear + "." + strPadding.PadLeft(padLength, pad);
        }
        public async Task CreateVoucherMaster(VoucherRequestVM model,DataTable VoucherDetail, string userGUID, string branchGUID, string companyGUID, CancellationToken token = default)
        {
            await using var dbTransaction = await _db.Database.BeginTransactionAsync(token);

            try
            {
                 ValidateVoucher(model.VoucherCreatedDate, model.VoucherCreatedDate);

                var VoucherMaster = new TblVoucherMaster
                {
                    VoucherDisplayId = model.VoucherDisplayId,
                    VoucherDisplayNumber = model.VoucherDisplayNumber,
                    VoucherCreatedDate = model.VoucherCreatedDate,
                    UserVoucherCreatedDateOnly = model.UserVoucherCreatedDateOnly,
                    VoucherTypeId = model.VoucherTypeId,
                    InvoiceNo = model.InvoiceNo,
                    DetailCodeId = model.DetailCodeId,
                    SubCodeId = model.SubCodeId,
                    MasterNarration = model.MasterNarration,
                    ChequeDate = model.ChequeDate,
                    SystemVoucherTypeId = model.SystemVoucherTypeId,
                    TransactionReferenceDetailCodeId = model.TransactionReferenceDetailCodeId,
                    TransactionReferenceSubcodeId = model.TransactionReferenceSubcodeId,
                    HideTransaction = model.HideTransaction,
                    ReferenceVoucherNumber = model.ReferenceTransactionNumber,
                    ReferenceTransactionNumber = model.ReferenceTransactionNumber,
                    BaseCurrencyId = model.BaseCurrencyId,
                    AmountFc = model.AmountFc,
                    AmountLc = model.AmountLc,
                    ChequeNumber = model.ChequeNumber,
                    Reference = model.Reference,
                    PreviousEntryReference = model.PreviousEntryReference,
                    DetailOne = model.DetailOne,
                    DetailTwo = model.DetailTwo,
                    DetailThree = model.DetailThree,
                    WithholdingTaxPercentage = model.WithholdingTaxPercentage,
                    TaxAmount = model.TaxAmount,
                    NetAmount = model.NetAmount,
                    IsMultiCurrencyJv = model.IsMultiCurrencyJv,
                    ChangesDetail = model.ChangesDetail,
                    IsPosted = model.IsPosted,
                    BankDetailCodeId = model.BankDetailCodeId,
                    BankSubCodeId = model.BankSubCodeId,
                    MasterReferenceNumber = model.MasterReferenceNumber,
                    CancelledByUserGuid = model.CancelledByUserGuid,
                    SupervisedByUserGuid = model.SupervisedByUserGuid,
                    ClearedByUserGuid = model.ClearedByUserGuid,
                    CancelledDate = model.CancelledDate,
                    SupervisionDate = model.SupervisionDate,
                    ClearedDate = model.ClearedDate,
                    StatusId = model.StatusId,
                    StatusIdtwo = model.StatusIdtwo,
                    CreatedByUserGuid = userGUID,
                    CashierAccountGuid = model.CashierAccountGuid,
                    BranchGuid = branchGUID,
                    CompanyGuid = companyGUID
                };

                _db.TblVoucherMasters.Add(VoucherMaster);

                await _db.SaveChangesAsync(token);

                await CreateVoucherDetail(VoucherDetail , VoucherMaster.VoucherGuid, token);

                await dbTransaction.CommitAsync(token);

            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync(token);
                throw ex;
            }
        }

            private async Task CreateVoucherDetail(DataTable VoucherDetails, string VoucherMaserGUID, CancellationToken token = default)
        {
            //foreach (DataRow dr in orderItem.Rows)
            //{
                var Voucherdetail = new TblVoucherDetail
                {
                    VoucherMasterGuid = VoucherMaserGUID,
                    //VoucherDetailGuid = VoucherDetails.VoucherDetailGuid,
                    //SequenceNumber = model.SequenceNumber,
                    //ForeignCurrencyAmount = model.ForeignCurrencyAmount,
                    //BaseCurrencyAmount = model.BaseCurrencyAmount,
                    //Description = model.Description,
                    //DetailCodeIdone = model.DetailCodeIdone,
                    //SubCodeIdone = model.SubCodeIdone,
                    //CurrencyId = model.CurrencyId,
                    //Rate = model.Rate,
                    //IsDebit = model.IsDebit,
                    //ApprovedByTeller = model.ApprovedByTeller,
                    //IsCurrencyStockEntry = model.IsCurrencyStockEntry,
                    //IsBalanceEntry = model.IsBalanceEntry,
                    //StockId = model.StockId,
                    //DescriptionReference = model.DescriptionReference,
                    //DetailCodeIdreference = model.DetailCodeIdreference,
                    //SubcodeIdreference = model.SubcodeIdreference,
                    //TaxId = model.TaxId,
                    //TaxPercentage = model.TaxPercentage,
                    //TaxType = model.TaxType,
                    //TotalAmountIncludingTax = model.TotalAmountIncludingTax,
                    //AnalysisCodeTypeId = model.AnalysisCodeTypeId,
                    //AnalysisCodeDetailCodeId = model.AnalysisCodeDetailCodeId,
                    //AnalysisCodeSubcodeId = model.AnalysisCodeSubcodeId,
                    //AnalysisCodeNumber = model.AnalysisCodeNumber,
                    //Aux1Big = model.Aux1Big,
                    //Aux2Big = model.Aux2Big
                };

                _db.TblVoucherDetails.Add(Voucherdetail);

                await _db.SaveChangesAsync(token);

            //}
        }
        private TblVoucherMaster ValidateVoucher(DateTime startTime, DateTime endDate)
        {
            var record = _db.TblVoucherMasters
                .AsNoTracking()
                .Where(x => x.VoucherCreatedDate >= startTime && x.VoucherCreatedDate<= endDate)
                .FirstOrDefault();

            if (record == null)
            {
                throw new Exception("Voucher Record Cannot Found");
            }

            return record;
        }
    }
}
