using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using SukkuShop.Areas.Admin.Models;
using SukkuShop.Models;

namespace SukkuShop.Areas.Admin.Controllers
{
    public partial class PodkategorieController : Controller
    {

        private readonly ApplicationDbContext _dbContext;

        public PodkategorieController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Admin/Podkategorie
        public virtual ActionResult Index()
        {
            ViewBag.SelectedOpt = 8;
            return View();
        }

        public virtual JsonResult GetCategoriesList()
        {
            var categoriesList = _dbContext.Categories.Where(x => x.UpperCategoryId == 0).Select(j => new
            {
                j.CategoryId,
                j.Name,
                productsCount = j.Products.Count,
                subCategoriesActive = false,                
                subCategories = _dbContext.Categories.Where(k => k.UpperCategoryId == j.CategoryId).Select(m => new
                {
                    m.CategoryId,
                    m.Name,
                    Promotion = m.Promotion??0,
                    productsCount = m.Products.Count,
                    editNameActive = false,
                    editDiscountActive=false,
                    canDelete = false
                })
            });
            return Json(categoriesList, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult AddSubCategory(SubcategoryCreateModel model)
        {
            var category = new Categories
            {
                Name = model.Name,
                Promotion = model.Discount,
                UpperCategoryId = model.UpperCategoryId
            };
            _dbContext.Categories.AddOrUpdate(category);
            _dbContext.SaveChanges();
            var cat = _dbContext.Categories.OrderByDescending(x => x.CategoryId).First();
            return Json(new
            {
                cat.CategoryId,
                cat.Name,
                cat.Promotion,
                cat.Products.Count,
                editNameActive = false,
                editDiscountActive = false,
                canDelete = false
            },JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult EditCategoryName(string name, int id)
        {
            var firstOrDefault = _dbContext.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (firstOrDefault != null)
            {
                firstOrDefault.Name = name;
                _dbContext.Categories.AddOrUpdate(firstOrDefault);
                _dbContext.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult EditCategoryDiscount(string discount, int id)
        {
            var firstOrDefault = _dbContext.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (firstOrDefault != null)
            {
                firstOrDefault.Promotion = Convert.ToInt32(discount);
                _dbContext.Categories.AddOrUpdate(firstOrDefault);
                _dbContext.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult DeleteSubCategory(int id)
        {
            var fo = _dbContext.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (fo != null)
            {
                var firstOrDefault = fo.Products;
                foreach (var products in firstOrDefault)
                {
                    products.CategoryId = fo.UpperCategoryId;
                }
                _dbContext.Products.AddRange(fo.Products);
                _dbContext.Categories.Remove(fo);
                _dbContext.SaveChanges();
                return Json(true);
            }
            return Json(false);
        }
    }
}