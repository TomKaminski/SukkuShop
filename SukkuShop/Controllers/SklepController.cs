﻿using System;
using System.Globalization;
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
        public ActionResult Produkty(string category, string subcategory= null, SortMethod method = SortMethod.Nowość,
            string search = null,
            int page = 1)
        {

            //getallproducts
            var allProducts = _dbContext.Products.Select(x => new ProductModel
            {
                Name = x.Name,
                ImageName = x.ImageName,
                Price = x.Price,
                Promotion = x.Promotion ?? 0,
                Id = x.ProductId,
                PriceAfterDiscount = x.Price*x.Promotion ?? 0,
                Category = x.Categories.Name,
                DateAdded = x.DateAdded,
                OrdersCount = x.OrdersCount
            }).ToList();

           
            //Novelty system
            foreach (var item in allProducts.Where(item => (DateTime.Now - item.DateAdded).Days < 14))
                item.Novelty = true;
            
            //bestseller system
            var bestsellerCounter = Math.Ceiling(allProducts.Count * 0.1);
            var i = 0;
            allProducts = allProducts.OrderByDescending(x => x.OrdersCount).ToList();
            foreach (var item in allProducts.TakeWhile(item => i != bestsellerCounter))
            {
                item.Bestseller = true;
                i++;
            }

            //get category and subcategory list
            var categorylist =
                _dbContext.Categories.Where(j => j.UpperCategoryId == 0 || j.UpperCategoryId == null).Select(x => x.Name);

            var categoryId = 0;
            if (categorylist.Contains(category))
                categoryId = _dbContext.Categories.FirstOrDefault(x => x.Name == category).CategoryId;
            
            var subcategorylist =
                _dbContext.Categories.Where(x => x.UpperCategoryId == categoryId)
                    .Select(j => j.Name)
                    .Distinct()
                    .ToList();

            //search system
            if (!subcategorylist.Contains(subcategory) && !categorylist.Contains(category))
            {
                if (search == null)
                    search = category;
                ViewBag.SearchString = search;
                category = search;
                if (search == null && category == null)
                    _shop.Products = allProducts.Select(c => c).ToList();
                else
                    _shop.Products = allProducts.Where(c => c.Name.ToUpper().Contains(category.ToUpper())).ToList();
            }
            else if (subcategorylist.Contains(subcategory))
                _shop.Products = allProducts.Where(c => c.Category == subcategory).ToList();
            else if (!subcategorylist.Contains(subcategory) && categorylist.Contains(category))
                _shop.Products =
                    allProducts.Where(
                        c => c.Category == category || subcategorylist.Contains(c.Category)).ToList();

            if (!_shop.Products.Any())
                return View("NoProducts");
            
            var paginator = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = 18,
                TotalItems = _shop.Products.Count()
            };

            var viewModel = new ProductsListViewModel
            {
                Products = _shop.SortProducts(_shop.Products, method).Select(x=>new ProductViewModel
                {
                    Bestseller = x.Bestseller,
                    Id = x.Id,
                    ImageName = x.ImageName,
                    Name = x.Name,
                    Novelty = x.Novelty,
                    Price = x.Price,
                    PriceAfterDiscount = x.Price * x.Promotion ?? 0,
                    Promotion = x.Promotion ?? 0,
                    QuantityInStock = x.QuantityInStock
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
            var similarProducts =
                _dbContext.Products.Where(x => x.CategoryId == product.CategoryId).OrderBy(x => Guid.NewGuid()).Take(6);

            var model = new ProductDetailsViewModel
            {
                Product = product,
                SimilarProducts = similarProducts
            };

            return View(model);
        }
    }
}