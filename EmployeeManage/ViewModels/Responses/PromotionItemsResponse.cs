
namespace EmployeeManage.ViewModels.Responses
{
    public class PromotionItemsResponse
    {
        public int ItemId { get; set; }
        public int? ItemTaxid { get; set; }
        public string ItemName { get; set; }
        public decimal? Discount { get; set; }
        public decimal ItemsPrice { get; set; }

    }
}