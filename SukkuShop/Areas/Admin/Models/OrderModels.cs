using SukkuShop.Models;

namespace SukkuShop.Areas.Admin.Models
{
    public class AdminOrderViewModelsSummary
    {
        public OrderViewItemsTotal OrderViewItemsTotal { get; set; }
        public CartAddressModel UserAddressModel { get; set; }
        public SharedShippingOrderSummaryModels OrderShipping { get; set; }
        public SharedShippingOrderSummaryModels OrderPayment { get; set; }
        public string OrderDat { get; set; }
        public string TotalTotalValue { get; set; }
        public string OrderInfo { get; set; }
        public bool Firma { get; set; }
        public int Id { get; set; }
        public string DiscountValue { get; set; }
        public int Discount { get; set; }
        public string UserOrderInfo { get; set; }
        public bool AccountExists { get; set; }

    }
}