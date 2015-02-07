#region

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using ImageResizer;
using SukkuShop.Areas.Admin.Models;
using SukkuShop.Models;

#endregion

namespace SukkuShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class ProduktyController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ProduktyController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Admin/AdminProduct
        public virtual ActionResult Lista(string name, int id = 0)
        {
            ViewBag.SelectedOpt = 1;
            ViewBag.Name = name;
            return View(id);
        }

        //Angular GET products
        public virtual JsonResult GetProductList()
        {
            var products = _dbContext.Products.Select(x => new
            {
                x.ProductId,
                x.Name,
                x.Published,
                x.WrongModel,
                x.IsComplete,
                x.CategoryId,
                upper = x.Categories.UpperCategoryId,
                canDelete = false,
                orders = x.OrdersCount,
                showinfo = false,
                data = new
                {
                    price = x.Price == null ? "Brak ceny" : "",
                    category = x.CategoryId == null ? "Brak kategorii" : "",
                    packing = x.Packing == null ? "Brak metody pakowania" : "",
                    opis = x.Description == null ? "Brak opisu" : "",
                    img = x.ImageName == null ? "Brak zdjęcia" : ""
                }
            });
            var categories = _dbContext.Categories.Where(x => x.UpperCategoryId == 0).Select(k => new
            {
                k.CategoryId,
                k.UpperCategoryId,
                k.Name,
                subcategories = _dbContext.Categories.Where(m => m.UpperCategoryId == k.CategoryId).Select(p => new
                {
                    p.CategoryId,
                    p.UpperCategoryId,
                    p.Name,
                })
            });
            var data = new
            {
                products,
                categories
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //Angular publish product
        public virtual ActionResult PublishProduct(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(m => m.ProductId == id);
            if (product != null)
            {
                product.Published = true;
                _dbContext.Products.AddOrUpdate(product);
                _dbContext.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        //Angular Unpublish product
        public virtual ActionResult UnpublishProduct(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(m => m.ProductId == id);
            if (product != null)
            {
                product.Published = false;
                _dbContext.Products.AddOrUpdate(product);
                _dbContext.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        //Angular delete product
        public virtual ActionResult DeleteProduct(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(m => m.ProductId == id);
            if (product == null) return Json(false);
            if (product.OrderDetails.Count != 0) return Json(false);
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return Json(true);
        }

        //Create product POST
        [HttpPost]
        public virtual ActionResult Dodaj(ProductUploadModel model)
        {
            if (ModelState.IsValid)
            {
                var category = model.Category == 0 ? null : model.Category;
                var price = model.Price == null
                    ? (decimal?) null
                    : Math.Floor((Convert.ToDecimal(model.Price.Replace(",", ".")))*100)/100;

                if ((price == null || category == null || model.Packing == null) && model.PublishAfterCreate)
                {
                    ModelState.AddModelError("PublishAfterCreate",
                        "Cena, Kategoria oraz sposób pakowania muszą być podane przed publikacją!");
                    GetDropDownLists(model.Category);
                    return View(model);                    
                }

                if (model.SubCategory != 0)
                    category = model.SubCategory;

                var product = new Products
                {
                    Quantity = model.Quantity,
                    CategoryId = category,
                    Name = model.Title,
                    DateAdded = DateTime.Now,
                    Description = model.Description,
                    Published = model.PublishAfterCreate,
                    Packing = model.Packing,
                    Promotion = model.Promotion,
                    Price = price,
                    ReservedQuantity = 0
                };

                _dbContext.Products.AddOrUpdate(product);
                _dbContext.SaveChanges();

                var prod = _dbContext.Products.OrderByDescending(x => x.ProductId).First();
                if (model.ImageBig != null && model.ImageBig.ContentLength != 0)
                {
                    var pathForSaving = Server.MapPath("~/Content/Images/Shop/");
                    var imageBig = new ImageJob(model.ImageBig, pathForSaving + prod.ProductId + "_normal",
                        new Instructions("maxwidth=700&maxheight=700&format=jpg"))
                    {
                        CreateParentDirectory = true,
                        AddFileExtension = true
                    };
                    imageBig.Build();
                    var imageSmall = new ImageJob(model.ImageBig, pathForSaving + prod.ProductId + "_small",
                        new Instructions("maxwidth=127&maxheight=127&format=jpg"))
                    {
                        CreateParentDirectory = true,
                        AddFileExtension = true
                    };
                    imageSmall.Build();
                    prod.IconName = prod.ProductId + "_small";
                    prod.ImageName = prod.ProductId + "_normal";
                }

                prod.IsComplete = true;
                foreach (var prop in product.GetType().GetProperties().Where(prop => prop.GetValue(prod, null) == null))
                    prod.IsComplete = false;
                if (prod.Price == null || prod.CategoryId == null || prod.Packing == null)
                {
                    prod.WrongModel = true;
                    prod.IsComplete = false;
                }
                _dbContext.Products.AddOrUpdate(prod);
                _dbContext.SaveChanges();
                return RedirectToAction(MVC.Admin.Produkty.Lista(prod.Name));
            }
            GetDropDownLists(model.Category);
            return View(model);
        }

        //Get category lists if model validation fails
        private void GetDropDownLists(int? id)
        {
            var categoryList = _dbContext.Categories.Where(x => x.UpperCategoryId == 0).Select(k => new SelectListItem
            {
                Text = k.Name,
                Value = k.CategoryId.ToString()
            }).ToList();
            categoryList.Add(new SelectListItem
            {
                Text = "Wybierz kategorię",
                Value = null,
                Selected = true
            });
            ViewBag.CategoryList = categoryList;
            var subCategoryList = new List<SelectListItem>
            {
                new SelectListItem {Text = "Wybierz podkategorię", Value = "0", Selected = true}
            };

            var subCategoryList2 =
                _dbContext.Categories.Where(x => x.UpperCategoryId == id).Select(k => new SelectListItem
                {
                    Text = k.Name,
                    Value = k.CategoryId.ToString()
                }).ToList();
            subCategoryList.AddRange(subCategoryList2);
            ViewBag.SubCategoryList = subCategoryList;
        }

        //Create product GET
        [HttpGet]
        public virtual ActionResult Dodaj()
        {
            ViewBag.SelectedOpt = 1;
            var list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Wybierz kategorię",
                    Value = "0",
                    Selected = true
                }
            };
            var categoryList = _dbContext.Categories.Where(x => x.UpperCategoryId == 0).Select(k => new SelectListItem
            {
                Text = k.Name,
                Value = k.CategoryId.ToString()
            }).ToList();
            list.AddRange(categoryList);
            ViewBag.CategoryList = list;
            var subCategoryList = new List<SelectListItem>
            {
                new SelectListItem {Text = "Wybierz podkategorię", Value = "0", Selected = true}
            };
            ViewBag.SubCategoryList = subCategoryList;
            return View();
        }

        //Left panel with categories for CREATE/EDIT view
        public virtual ActionResult GetCategoriesCreateEditProduct()
        {
            var categories =
                _dbContext.Categories.Where(x => x.UpperCategoryId == 0)
                    .Select(k => new CategoriesEditCreateProductModel
                    {
                        Id = k.CategoryId,
                        Name = k.Name,
                        SubCategoryList =
                            _dbContext.Categories.Where(m => m.UpperCategoryId == k.CategoryId)
                                .Select(p => new CateogriesCreateEditProduct
                                {
                                    Id = p.CategoryId,
                                    Name = p.Name
                                }).ToList()
                    }).ToList();
            return PartialView(categories);
        }

        //Jquery ajax, get subcategory list after main category changes
        [HttpGet]
        public virtual JsonResult GetSubCategoryList(int id)
        {
            var subcategoryList = new List<object>
            {
                new {Text = "Wybierz podkategorię", Value = 0}
            };
            if (id == 0) return Json(subcategoryList, JsonRequestBehavior.AllowGet);
            var subcategoryList2 = _dbContext.Categories.Where(x => x.UpperCategoryId == id).Select(k => new
            {
                Text = k.Name,
                Value = k.CategoryId
            }).ToList();
            subcategoryList.AddRange(subcategoryList2);
            return Json(subcategoryList, JsonRequestBehavior.AllowGet);
        }

        //EDIT GET
        [HttpGet]
        public virtual ActionResult Edytuj(int id)
        {
            ViewBag.SelectedOpt = 1;
            var item = _dbContext.Products.Find(id);
            GetCategoryListEdit(item);

            var model = new ProductEditModel
            {
                Description = item.Description,
                Image = item.ImageName,
                Packing = item.Packing,
                Price = item.Price.ToString(),
                Promotion = item.Promotion,
                Quantity = item.Quantity,
                PublishAfterCreate = item.Published,
                Id = item.ProductId,
                Title = item.Name
            };
            return View(model);
        }

        //Get category list for edit view (selected)
        private void GetCategoryListEdit(Products item)
        {
            var category = _dbContext.Categories.Find(item.CategoryId);
            var list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Wybierz kategorię",
                    Value = "0",
                    Selected = 0 == item.CategoryId || item.CategoryId == null
                }
            };

            var categoryList = _dbContext.Categories.Where(x => x.UpperCategoryId == 0).Select(k => new SelectListItem
            {
                Text = k.Name,
                Value = k.CategoryId.ToString(),
                Selected = false
            }).ToList();
            list.AddRange(categoryList);
            foreach (var selectListItem in list)
            {
                if (category == null) continue;
                if (Convert.ToInt32(selectListItem.Value) == category.CategoryId ||
                    Convert.ToInt32(selectListItem.Value) == category.UpperCategoryId)
                    selectListItem.Selected = true;
            }
            ViewBag.CategoryList = list;
            var subCategoryList = new List<SelectListItem>
            {
                new SelectListItem {Text = "Wybierz podkategorię", Value = "0", Selected = 0 == item.CategoryId}
            };

            if (category != null)
            {
                var subCategoryList2 =
                    _dbContext.Categories.Where(x => x.UpperCategoryId == category.CategoryId)
                        .Select(k => new SelectListItem
                        {
                            Text = k.Name,
                            Value = k.CategoryId.ToString(),
                            Selected = category.UpperCategoryId != 0 && (k.CategoryId == item.CategoryId)
                        }).ToList();
                subCategoryList.AddRange(subCategoryList2);
            }

            ViewBag.SubCategoryList = subCategoryList;
        }

        //EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edytuj(ProductEditModel model)
        {
            var product = _dbContext.Products.Find(model.Id);
            if (ModelState.IsValid)
            {
                var category = model.Category == 0 ? null : model.Category;
                var price = model.Price == null ? (decimal?) null : Convert.ToDecimal(model.Price.Replace(",", "."));
                if (price == null || category == null || model.Packing == null)
                {
                    if (model.PublishAfterCreate)
                    {
                        ModelState.AddModelError("PublishAfterCreate",
                            "Cena, Kategoria oraz sposób pakowania muszą być podane przed publikacją!");
                        GetCategoryListEdit(product);
                        return View(model);
                    }
                }
                if (price != null)
                    price = Math.Floor(((decimal) price)*100)/100;
                if (model.SubCategory != 0)
                    category = model.SubCategory;


                product.Price = price;
                product.CategoryId = category;
                product.Description = model.Description;
                product.Name = model.Title;
                product.Quantity = model.Quantity;
                product.Packing = model.Packing;
                product.Published = model.PublishAfterCreate;
                product.Promotion = model.Promotion;
                if (model.TrueImageDeleted)
                {
                    product.IconName = null;
                    product.ImageName = null;
                }

                if (model.ImageBig != null && model.ImageBig.ContentLength != 0)
                {
                    var pathForSaving = Server.MapPath("~/Content/Images/Shop/");
                    var imageBig = new ImageJob(model.ImageBig, pathForSaving + product.ProductId + "_normal",
                        new Instructions("maxwidth=700&maxheight=700&format=jpg"))
                    {
                        CreateParentDirectory = true,
                        AddFileExtension = true
                    };
                    imageBig.Build();
                    var imageSmall = new ImageJob(model.ImageBig, pathForSaving + product.ProductId + "_small",
                        new Instructions("maxwidth=127&maxheight=127&format=jpg"))
                    {
                        CreateParentDirectory = true,
                        AddFileExtension = true
                    };
                    imageSmall.Build();
                    product.IconName = product.ProductId + "_small";
                    product.ImageName = product.ProductId + "_normal";
                }
                product.WrongModel = false;
                product.IsComplete = true;
                foreach (
                    var prop in product.GetType().GetProperties().Where(prop => prop.GetValue(product, null) == null))
                    product.IsComplete = false;
                if (product.Price == null || product.CategoryId == null || product.Packing == null)
                {
                    product.WrongModel = true;
                    product.IsComplete = false;
                    product.Published = false;
                }
                _dbContext.Products.AddOrUpdate(product);
                _dbContext.SaveChanges();
                return RedirectToAction(MVC.Admin.Produkty.Lista(product.Name));
            }
            GetCategoryListEdit(product);
            return View(model);
        }
    }
}