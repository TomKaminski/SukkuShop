using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using SukkuShop.Models;

namespace SukkuShop.Controllers
{
    public partial class NavController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public NavController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [ChildActionOnly]
        [OutputCache(Duration = 86400, VaryByParam = "category")]
        public virtual PartialViewResult Menu(string category, SortMethod method = SortMethod.Nowość)
        {
            ViewBag.Category = category;
            ViewBag.CurrentSortMethod = method;
            var categoryLinks = _dbContext.Categories.Where(j=>j.UpperCategoryId==0).Select(x => x.Name);
            return PartialView(categoryLinks);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 86400, VaryByParam = "category;subcategory;method")]
        public virtual PartialViewResult SubCategory(string category, string subcategory = null, SortMethod method = SortMethod.Nowość)
        {
            ViewBag.Category = category;
            ViewBag.SubCategory = subcategory;
            ViewBag.CurrentSortMethod = method;

            var r = _dbContext.Categories.FirstOrDefault(c => c.Name == category);
            if (r == null) return null;
            var categoryid = r.CategoryId;
            var subcategoryLinks =
                _dbContext.Products.Where(x => x.Categories.UpperCategoryId != 0 && x.Categories.UpperCategoryId==categoryid)
                    .Select(j => j.Categories.Name)
                    .Distinct()
                    .ToList();
            return PartialView(subcategoryLinks);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 86400, VaryByParam = "category;subcategory;method")]
        public virtual PartialViewResult SortList(string category, string search, string subcategory, SortMethod method = SortMethod.Nowość)
        {
            ViewBag.CurrentSortMethod = method;
            if (search != null)
            {
                ViewBag.SearchString = search;
                return PartialView(MVC.Nav.Views.SearchSortList);
            }            
            ViewBag.SelectedCategory = category;
            ViewBag.SelectedSubCategory = subcategory;
            return PartialView();
        }
    }
}