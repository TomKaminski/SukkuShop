using System.Web.Mvc;

namespace SukkuShop.Infrastructure.Binders
{
    public enum SortMethod
    {
        FromHighestPrice,
        FromLowestPrice,
        Popularnosc,
        Promotion,
        Novelty
    }

    public class ProductFilter
    {
        public string Fraza { get; set; }
        public string[] SelectedCategories { get; set; }
        public int CenaMax { get; set; }
        public int CenaMin { get; set; }
        public bool Available { get; set; }
        public SortMethod SortMethod { get; set; }
    }

    public class ProductFilterViewModel
    {
        public string Fraza { get; set; }
        public string[] SelectedCategories { get; set; }
        public int CenaMax { get; set; }
        public int CenaMin { get; set; }
        public bool Available { get; set; }
        public SortMethod SortMethod { get; set; }
    }
    public class ProductFilterBinder : IModelBinder
    {
        private const string SessionName = "ProductFilter";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var filter = controllerContext.HttpContext.Session[SessionName] as ProductFilter ?? new ProductFilter();
            controllerContext.HttpContext.Session[SessionName] = filter;
            return filter;
        }
    }
}