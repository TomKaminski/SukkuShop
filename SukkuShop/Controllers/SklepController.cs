using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using DevTrends.MvcDonutCaching;
using SukkuShop.Infrastructure.Generic;
using SukkuShop.Models;

namespace SukkuShop.Controllers
{
    public partial class SklepController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IShop _shop;

        public SklepController(ApplicationDbContext dbContext, IShop shop)
        {
            _dbContext = dbContext;
            _shop = shop;
        }

        // GET: Produkty

        public virtual ActionResult GetProductByCategory(string id=null)
        {
            //getallproducts
            GetAllProducts();

            //Novelty system
            _shop.Bestsellers();

            //bestseller system
            _shop.NewProducts();
            var categoryId = 0;
            var subcategoryList = new List<string>();
            if (id != null)
            {
                var categorylist =
               _dbContext.Categories.Where(j => j.UpperCategoryId == 0 || j.UpperCategoryId == null)
                   .Select(x => x.Name);

                if (categorylist.Contains(id))
                    categoryId = _dbContext.Categories.FirstOrDefault(x => x.Name == id).CategoryId;
                if(categoryId!=0)
                    subcategoryList =
                        _dbContext.Categories.Where(x => x.UpperCategoryId == categoryId)
                            .Select(j => j.Name)
                            .Distinct()
                            .ToList();



                if (categorylist.Contains(id))
                    _shop.Products =
                        _shop.Products.Where(
                            c => c.Category == id || subcategoryList.Contains(c.Category)).ToList();

                if (!categorylist.Contains(id))
                    _shop.Products = _shop.Products.Where(c => c.Name.ToUpper().Contains(id.ToUpper())).ToList();
                
            }
            
            subcategoryList.Add("Wszystko");
            if (!_shop.Products.Any())
                return View(MVC.Sklep.Views.NoProducts);
            var obj = new
            {
                categoryId,
                id,
                productList = _shop.Products,
                subcategoryList
            };
            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        //[DonutOutputCache(Duration=86400,Location = OutputCacheLocation.Server,VaryByParam = "id")]
        public virtual ActionResult Produkty(string id)
        {

            return View((object)id);
        }


        //[DonutOutputCache(Duration = 86400, VaryByParam = "id",Location = OutputCacheLocation.Server)]
        public virtual ActionResult SzczegółyProduktu(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.ProductId == id);
            if (product == null)
                return View(MVC.Sklep.Views.NoProducts);

            var category =
                _dbContext.Categories.First(x => x.CategoryId == product.CategoryId);

            var categoryName = category.UpperCategoryId == 0 ? category : _dbContext.Categories.First(x => x.CategoryId == category.UpperCategoryId);
            var similarProducts =
                _dbContext.Products.Where(
                    x =>
                        (x.Categories.UpperCategoryId==categoryName.CategoryId || x.Categories.CategoryId==categoryName.CategoryId) &&
                        x.ProductId != product.ProductId).Select(j=>new SimilarProductModel
                        {
                            Id = j.ProductId,
                            ImageName = j.ImageName,
                            Name = j.Name,
                            Price = j.Price,
                            PriceAfterDiscount = j.Price - ((j.Price * j.Promotion) / 100) ?? j.Price,
                            Available = j.Quantity > 0
                        }).OrderBy(x => Guid.NewGuid()).Take(4);

            var model = new ProductDetailsViewModel
            {
                Product = new ProductDetailModel
                {
                    Category = categoryName.Name,
                    Id=product.ProductId,
                    ImageName = product.ImageName,
                    Name = product.Name,
                    Price = product.Price,
                    PriceAfterDiscount = product.Price-((product.Promotion*product.Price)/100)??product.Price,
                    Promotion = product.Promotion ?? 0,
                    QuantityInStock = product.Quantity,
                    Packing = product.Packing,
                    Description = product.Description
                },
                SimilarProducts = similarProducts
            };
            return View(model);
        }

        //[DonutOutputCache(Duration = 1800, VaryByParam = "search;method;page", Location = OutputCacheLocation.Client)]
        public virtual ActionResult Wyszukaj(string id)
        {
            ////getallproducts
            //GetAllProducts();

            ////Novelty system
            //_shop.Bestsellers();

            ////bestseller system
            //_shop.NewProducts();

            //ViewBag.SearchString = search;
            //_shop.Products = _shop.Products.Where(c => c.Name.ToUpper().Contains(search.ToUpper())).ToList();

            //if (!_shop.Products.Any())
            //    return View(MVC.Sklep.Views.NoProducts);

            //var paginator = new PagingInfo
            //{
            //    CurrentPage = page,
            //    ItemsPerPage = 12,
            //    TotalItems = _shop.Products.Count()
            //};

            //_shop.SortProducts(method);
            //return View(MVC.Sklep.Views.Produkty, new ProductsListViewModel
            //{
            //    Products = _shop.Products.Select(x => new ProductViewModel
            //    {
            //        Bestseller = x.Bestseller,
            //        Id = x.Id,
            //        ImageName = x.ImageName,
            //        Name = x.Name,
            //        Novelty = x.Novelty,
            //        Price = x.Price.ToString("C"),
            //        PriceAfterDiscount = x.PriceAfterDiscount.ToString("C"),
            //        Promotion = x.Promotion ?? 0,
            //        QuantityInStock = x.QuantityInStock
            //    }).Skip((page - 1)*paginator.ItemsPerPage)
            //        .Take(paginator.ItemsPerPage),
            //    CurrentCategory = null,
            //    CurrentSortMethod = method,
            //    PagingInfo = paginator,
            //    CurrentSubCategory = null,
            //    CurrentSearch = search
            //});
            return View("Produkty", (object)id);
        }

        public virtual ActionResult Szukaj()
        {
            return !String.IsNullOrEmpty(Request["search"]) ? RedirectToAction(MVC.Sklep.Wyszukaj(Request["search"])) : RedirectToAction(MVC.Sklep.Produkty(null));
        }

        public virtual ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(MVC.Home.Index());
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
                OrdersCount = x.OrdersCount,
                QuantityInStock = x.Quantity
            }).ToList();
        }
    }
}