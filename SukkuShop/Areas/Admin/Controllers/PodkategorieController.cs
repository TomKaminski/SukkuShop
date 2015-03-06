using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using SukkuShop.Areas.Admin.Models;
using SukkuShop.Infrastructure.Generic;
using SukkuShop.Models;

namespace SukkuShop.Areas.Admin.Controllers
{
    public partial class PodkategorieController : Controller
    {
        private readonly IAppRepository _appRepository;
        private readonly ApplicationDbContext _dbContext;

        public PodkategorieController(ApplicationDbContext dbContext, IAppRepository appRepository)
        {
            _dbContext = dbContext;
            _appRepository = appRepository;
        }

        // GET: Admin/Podkategorie
        public virtual ActionResult Index()
        {
            ViewBag.SelectedOpt = 8;
            return View();
        }

        public virtual JsonResult GetCategoriesList()
        {
            var categoriesList = _appRepository.GetAll<Categories>(x => x.UpperCategoryId == 0).Select(j => new
            {
                j.CategoryId,
                j.Name,
                productsCount = _appRepository.GetAll<Products>(y => y.CategoryId == j.CategoryId).Count(),
                subCategoriesActive = false,    
                newDiscount = 0,
                Promotion=j.Promotion??0,
                editDiscountActive=false,
                subCategories = _appRepository.GetAll<Categories>(k => k.UpperCategoryId == j.CategoryId).Select(m => new
                {
                    m.CategoryId,
                    m.Name,
                    Promotion = m.Promotion??0,
                    productsCount = _appRepository.GetAll<Products>(v => v.CategoryId == m.CategoryId).Count(),
                    editNameActive = false,
                    editDiscountActive=false,
                    canDelete = false,
                    NewPromotion = 0
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
            var cat = _appRepository.GetAll<Categories>().OrderByDescending(x=>x.CategoryId).First();
            return Json(new
            {
                cat.CategoryId,
                cat.Name,
                cat.Promotion,
                productsCount = _appRepository.GetAll<Products>(x=>x.CategoryId==cat.CategoryId).Count(),
                editNameActive = false,
                editDiscountActive = false,
                canDelete = false,
                NewPromotion = 0
            },JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult EditCategoryName(string name, int id)
        {
            var firstOrDefault = _appRepository.GetSingle<Categories>(x => x.CategoryId == id);
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
            var firstOrDefault = _appRepository.GetSingle<Categories>(x => x.CategoryId == id);
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
            var fo = _appRepository.GetSingle<Categories>(x => x.CategoryId == id);
            if (fo != null)
            {
                var firstOrDefault = _appRepository.GetAll<Products>(x => x.CategoryId == fo.CategoryId);
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