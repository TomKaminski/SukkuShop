using System;
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
        public ActionResult Produkty(string category, string subcategory = null, SortMethod method = SortMethod.Nowość,
            int page = 1)
        {
            //getallproducts
            GetAllProducts();

            //Novelty system
            _shop.Bestsellers();

            //bestseller system
            _shop.NewProducts();

            //get category and subcategory list
            var categorylist =
                _dbContext.Categories.Where(j => j.UpperCategoryId == 0 || j.UpperCategoryId == null)
                    .Select(x => x.Name);

            var categoryId = 0;
            if (categorylist.Contains(category))
                categoryId = _dbContext.Categories.FirstOrDefault(x => x.Name == category).CategoryId;

            var subcategorylist =
                _dbContext.Categories.Where(x => x.UpperCategoryId == categoryId)
                    .Select(j => j.Name)
                    .Distinct()
                    .ToList();

            if (subcategorylist.Contains(subcategory))
                _shop.Products = _shop.Products.Where(c => c.Category == subcategory).ToList();
            else if (categorylist.Contains(category))
                _shop.Products =
                    _shop.Products.Where(
                        c => c.Category == category || subcategorylist.Contains(c.Category)).ToList();

            if (!_shop.Products.Any())
                return View("NoProducts");

            var paginator = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = 18,
                TotalItems = _shop.Products.Count()
            };

            _shop.SortProducts(method);
            return View(new ProductsListViewModel
            {
                Products = _shop.Products.Select(x => new ProductViewModel
                {
                    Bestseller = x.Bestseller,
                    Id = x.Id,
                    ImageName = x.ImageName,
                    Name = x.Name,
                    Novelty = x.Novelty,
                    Price = x.Price,
                    PriceAfterDiscount = x.PriceAfterDiscount,
                    Promotion = x.Promotion ?? 0,
                    QuantityInStock = x.QuantityInStock
                }).Skip((page - 1)*paginator.ItemsPerPage)
                    .Take(paginator.ItemsPerPage),
                CurrentCategory = category,
                CurrentSortMethod = method,
                PagingInfo = paginator,
                CurrentSubCategory = subcategory,
                CurrentSearch = null
            });
        }

        public ActionResult SzczegółyProduktu(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.ProductId == id);
            if (product == null)
                return View("NoProducts");
            
            var similarProducts = _dbContext.Products.Where(x => x.CategoryId == product.CategoryId).
                Select(j =>
                    new SimilarProductModel
                    {
                        Id = j.ProductId,
                        ImageName = j.ImageName,
                        Name = j.Name,
                        Price = j.Price,
                        PriceAfterDiscount = j.Price - ((j.Price*j.Promotion)/100) ?? j.Price
                    }).OrderBy(x => Guid.NewGuid()).Take(6);

            var model = new ProductDetailsViewModel
            {
                Product = product,
                SimilarProducts = similarProducts
            };
            return View(model);
        }

        public ActionResult Wyszukaj(string search, SortMethod method = SortMethod.Nowość, int page = 1)
        {
            //getallproducts
            GetAllProducts();

            //Novelty system
            _shop.Bestsellers();

            //bestseller system
            _shop.NewProducts();

            ViewBag.SearchString = search;
            _shop.Products = _shop.Products.Where(c => c.Name.ToUpper().Contains(search.ToUpper())).ToList();

            if (!_shop.Products.Any())
                return View("NoProducts");

            var paginator = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = 18,
                TotalItems = _shop.Products.Count()
            };

            _shop.SortProducts(method);
            return View("Produkty", new ProductsListViewModel
            {
                Products = _shop.Products.Select(x => new ProductViewModel
                {
                    Bestseller = x.Bestseller,
                    Id = x.Id,
                    ImageName = x.ImageName,
                    Name = x.Name,
                    Novelty = x.Novelty,
                    Price = x.Price,
                    PriceAfterDiscount = x.PriceAfterDiscount,
                    Promotion = x.Promotion ?? 0,
                    QuantityInStock = x.QuantityInStock
                }).Skip((page - 1)*paginator.ItemsPerPage)
                    .Take(paginator.ItemsPerPage),
                CurrentCategory = null,
                CurrentSortMethod = method,
                PagingInfo = paginator,
                CurrentSubCategory = null,
                CurrentSearch = search
            });
        }

        public ActionResult Szukaj()
        {
            return !String.IsNullOrEmpty(Request["search"]) ? RedirectToAction("Wyszukaj", new { search = Request["search"],method=SortMethod.Nowość,page=1 }) : RedirectToAction("Produkty", new {method = SortMethod.Nowość, page = 1 });
        }

        public ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void GetAllProducts()
        {
            _shop.Products = _dbContext.Products.Select(x => new ProductModel
            {
                Name = x.Name,
                ImageName = x.ImageName,
                Price = x.Price,
                Promotion = x.Promotion ?? 0,
                Id = x.ProductId,
                PriceAfterDiscount = x.Price - ((x.Price * x.Promotion) / 100) ?? x.Price,
                Category = x.Categories.Name,
                DateAdded = x.DateAdded,
                OrdersCount = x.OrdersCount
            }).ToList();
        }
    }
}