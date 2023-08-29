namespace EmployeeManage.ViewModels.Responses
{
    public class VoucherDetailResponseVM
    {
        public string VoucherMasterGuid { get; set; }
        public short SequenceNumber { get; set; }
        public int DetailCodeId { get; set; }
        public int SubCodeId { get; set; }
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
        public decimal? TaxAmount { get; set; }
        public short? TaxType { get; set; }
        public decimal? TotalAmountIncludingTax { get; set; }
        public int? AnalysisCodeTypeId { get; set; }
        public int? AnalysisCodeDetailCodeId { get; set; }
        public int? AnalysisCodeSubcodeId { get; set; }
        public string AnalysisCodeNumber { get; set; }
        public string Aux1Big { get; set; }
        public string Aux2Big { get; set; }
        public string CompanyGuid { get; set; }
    }
}
