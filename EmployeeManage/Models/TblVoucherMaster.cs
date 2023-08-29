using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblVoucherMaster
    {
        public string VoucherGuid { get; set; }
        public int VoucherDisplayId { get; set; }
        public string VoucherDisplayNumber { get; set; }
        public DateTime VoucherCreatedDate { get; set; }
        public DateTime UserVoucherCreatedDateOnly { get; set; }
        public int VoucherTypeId { get; set; }
        public string InvoiceNo { get; set; }
        public int? DetailCodeId { get; set; }
        public int? SubCodeId { get; set; }
        public string MasterNarration { get; set; }
        public DateTime? ChequeDate { get; set; }
        public int? SystemVoucherTypeId { get; set; }
        public int? TransactionReferenceDetailCodeId { get; set; }
        public int? TransactionReferenceSubcodeId { get; set; }
        public bool? HideTransaction { get; set; }
        public string ReferenceVoucherNumber { get; set; }
        public string ReferenceTransactionNumber { get; set; }
        public int? BaseCurrencyId { get; set; }
        public decimal? AmountFc { get; set; }
        public decimal? AmountLc { get; set; }
        public string ChequeNumber { get; set; }
        public string Reference { get; set; }
        public string PreviousEntryReference { get; set; }
        public string DetailOne { get; set; }
        public string DetailTwo { get; set; }
        public string DetailThree { get; set; }
        public decimal? WithholdingTaxPercentage { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public bool? IsMultiCurrencyJv { get; set; }
        public string ChangesDetail { get; set; }
        public bool? IsPosted { get; set; }
        public int? BankDetailCodeId { get; set; }
        public int? BankSubCodeId { get; set; }
        public string MasterReferenceNumber { get; set; }
        public string CancelledByUserGuid { get; set; }
        public string SupervisedByUserGuid { get; set; }
        public string ClearedByUserGuid { get; set; }
        public DateTime? CancelledDate { get; set; }
        public DateTime? SupervisionDate { get; set; }
        public DateTime? ClearedDate { get; set; }
        public int StatusId { get; set; }
        public int? StatusIdtwo { get; set; }
        public string CreatedByUserGuid { get; set; }
        public string CashierAccountGuid { get; set; }
        public string BranchGuid { get; set; }
        public string CompanyGuid { get; set; }
    }
}
