using System.Linq;
using System.Web.Mvc;
using SukkuShop.Models;

namespace SukkuShop.Controllers
{
    public class NavController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public NavController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.Category = category;
            var categoryLinks = _dbContext.Categories.Where(j=>j.UpperCategoryId==0).Select(x => x.Name);
            return PartialView(categoryLinks);
        }

        public PartialViewResult SubCategory(string category = null, string subcategory = null, SortMethod method = SortMethod.Nowość)
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

        public PartialViewResult SortList(string category, string search,string subcategory,SortMethod method = SortMethod.Nowość)
        {
            ViewBag.CurrentSortMethod = method;
            if (search != null)
            {
                ViewBag.SearchString = search;
                return PartialView("SearchSortList");
            }            
            ViewBag.SelectedCategory = category;
            ViewBag.SelectedSubCategory = subcategory;
            return PartialView();
        }
    }
}