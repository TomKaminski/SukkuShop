using System.Web.Mvc;
using SukkuShop.Infrastructure.Binders;

namespace SukkuShop.Areas.Admin.Models
{
    [ModelBinder(typeof(ShippingPaymentModelBinder))]
    public class ShippingPaymentCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }
}