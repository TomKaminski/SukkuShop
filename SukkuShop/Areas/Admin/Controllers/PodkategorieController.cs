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
                    m.Promotion,
                    productsCount = j.Products.Count,
                    editNameActive = false,
                    editDiscountActive=false
                })
            });
            return Json(categoriesList, JsonRequestBehavior.AllowGet);
        }

        //public virtual JsonResult AddSubcategory(SubcategoryCreateModel model)
        //{
            

        //}

    }
}