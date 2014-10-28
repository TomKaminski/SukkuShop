using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
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
            IEnumerable<string> categoryLinks = _dbContext.Categories.Select(x => x.Name);
            return PartialView(categoryLinks);
        }

        public PartialViewResult SortList( string category, string search,SortMethod method = SortMethod.Nowości)
        {
            if (search != null)
                category = search;
            ViewBag.CurrentSortMethod = method;
            ViewBag.SelectedCategory = category;
            var sortlist = Enum.GetValues(typeof (SortMethod))
                .Cast<SortMethod>()
                .Select(v => v.ToString());
            return PartialView(sortlist);
        }
    }
}