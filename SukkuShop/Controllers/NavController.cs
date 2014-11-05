using System;
using System.Collections.Generic;
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

        public PartialViewResult SubCategory(string category = null, string subcategory = null, SortMethod method = SortMethod.Nowości)
        {
            ViewBag.Category = category;
            ViewBag.SubCategory = subcategory;
            ViewBag.CurrentSortMethod = method;

            var r = _dbContext.Categories.FirstOrDefault(c => c.Name == category);
            if (r != null)
            {
                var categoryid = r.CategoryId;
                var subcategoryLinks =
                    _dbContext.Categories.Where(j => j.UpperCategoryId == categoryid)
                        .Select(x => x.Name)
                        .Distinct()
                        .ToList();
                return PartialView(subcategoryLinks);
            }
            return null;
        }

        public PartialViewResult SortList(string category, string search,string subcategory,SortMethod method = SortMethod.Nowości)
        {
            if (search != null)
                category = search;
            ViewBag.CurrentSortMethod = method;
            ViewBag.SelectedCategory = category;
            ViewBag.SelectedSubCategory = subcategory;
            var sortlist = Enum.GetValues(typeof (SortMethod))
                .Cast<SortMethod>()
                .Select(v => v.ToString());
            return PartialView(sortlist);
        }
    }
}