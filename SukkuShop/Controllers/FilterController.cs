using System.Web.Mvc;
using SukkuShop.Infrastructure.Binders;

namespace SukkuShop.Controllers
{
    public class FilterController : Controller
    {
        // GET: Filter
        public PartialViewResult Index(ProductFilter filter)
        {
            var model = new ProductFilterViewModel
            {
                Available = filter.Available,
                CenaMax = filter.CenaMax,
                CenaMin = filter.CenaMin,
                Fraza = filter.Fraza,
                SelectedCategories = filter.SelectedCategories,
                SortMethod = filter.SortMethod,
            };
            return PartialView(model);
        }
    }
}