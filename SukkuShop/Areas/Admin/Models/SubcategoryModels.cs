using System.Web.Mvc;
using SukkuShop.Infrastructure.Binders;

namespace SukkuShop.Areas.Admin.Models
{
    [ModelBinder(typeof(SubcategoryCreateModelBinder))]
    public class SubcategoryCreateModel
    {
        public string Name { get; set; }
        public int?  UpperCategoryId { get; set; }
        public int? Discount { get; set; }
    }
}