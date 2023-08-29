using System;

namespace EmployeeManage.ViewModels.Request
{
    public class VoucherRequestVM
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
    public class VoucherDetailRequestVM:VoucherRequestVM
    {
        public string VoucherDetailGuid { get; set; }
        public string VoucherMasterGuid { get; set; }
        public short SequenceNumber { get; set; }
        public decimal ForeignCurrencyAmount { get; set; }
        public decimal BaseCurrencyAmount { get; set; }
        public string Description { get; set; }
        public int DetailCodeIdone { get; set; }
        public int SubCodeIdone { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? Rate { get; set; }
        public bool IsDebit { get; set; }
        public bool? ApprovedByTeller { get; set; }
        public bool? IsCurrencyStockEntry { get; set; }
        public bool? IsBalanceEntry { get; set; }
        public int? StockId { get; set; }
        public string DescriptionReference { get; set; }
        public int? DetailCodeIdreference { get; set; }
        public int? SubcodeIdreference { get; set; }
        public int? TaxId { get; set; }
        public decimal? TaxPercentage { get; set; }
        public short? TaxType { get; set; }
        public decimal? TotalAmountIncludingTax { get; set; }
        public int? AnalysisCodeTypeId { get; set; }
        public int? AnalysisCodeDetailCodeId { get; set; }
        public int? AnalysisCodeSubcodeId { get; set; }
        public string AnalysisCodeNumber { get; set; }
        public string Aux1Big { get; set; }
        public string Aux2Big { get; set; }
    }

}
