using System.Linq;
using System.Web.Mvc;
using SukkuShop.Infrastructure.Generic;
using SukkuShop.Models;

namespace SukkuShop.Controllers
{
    public class SklepController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IShop _shop;

        public SklepController(ApplicationDbContext dbContext, IShop shop)
        {
            _dbContext = dbContext;
            _shop = shop;
        }

        // GET: Produkty
        public ActionResult Produkty(string subcategory, string category, SortMethod method = SortMethod.Nowości,
            string search = null,
            int page = 1)
        {
            var categorylist =
                _dbContext.Categories.Where(j => j.UpperCategoryId == 0).Select(x => x.Name);
            var categoryId = 0;
            if (category != null)
                categoryId = _dbContext.Categories.FirstOrDefault(x => x.Name == category).CategoryId;

            var subcategorylist =
                _dbContext.Categories.Where(x => x.UpperCategoryId == categoryId)
                    .Select(j => j.Name)
                    .Distinct()
                    .ToList();
            if (!subcategorylist.Contains(subcategory) && !categorylist.Contains(category))
            {
                if (search == null)
                    search = category;
                ViewBag.SearchString = search;
                category = search;
                _shop.Products = _dbContext.Products.Where(c => c.Name.Contains(category));
                if (search == null && category == null)
                    _shop.Products = _dbContext.Products.Select(c => c);
            }
            else if (subcategorylist.Contains(subcategory))
                _shop.Products = _dbContext.Products.Where(c => c.Categories.Name == subcategory);
            else if (!subcategorylist.Contains(subcategory) && categorylist.Contains(category))
                _shop.Products =
                    _dbContext.Products.Where(
                        c => c.Categories.Name == category || subcategorylist.Contains(c.Categories.Name));


            var paginator = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = 1,
                TotalItems = _shop.Products.Count()
            };

            var viewModel = new ProductsListViewModel
            {
                Products = _shop.SortProducts(_shop.Products, method).Select(x => new ProductViewModel
                {
                    Name = x.Name,
                    ImageName = x.ImageName,
                    Price = x.Price,
                    Promotion = x.Promotion ?? 0,
                    Id = x.ProductId,
                    PriceAfterDiscount = x.Price*x.Promotion ?? 0
                }).Skip((page - 1)*paginator.ItemsPerPage)
                    .Take(paginator.ItemsPerPage),
                CurrentCategory = category,
                CurrentSortMethod = method,
                PagingInfo = paginator,
                CurrentSubCategory = subcategory
            };
            return View(viewModel);
        }

        public ActionResult SzczegółyProduktu(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.ProductId == id);
            return View(product);
        }
    }
}