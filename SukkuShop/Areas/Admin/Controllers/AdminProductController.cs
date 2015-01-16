using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Web;
using System.Web.Mvc;
using SukkuShop.Areas.Admin.Models;
using SukkuShop.Models;

namespace SukkuShop.Areas.Admin.Controllers
{
    public partial class AdminProductController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminProductController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Admin/AdminProduct
        public virtual ActionResult Index()
        {            
            return View();
        }

        public virtual ActionResult UploadFile(ProductUploadModel model)
        {
            if (model.ImageBig != null && model.ImageBig.ContentLength != 0)
            {
                var pathForSaving = Server.MapPath("~/Uploads");
                if (CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        model.ImageNormal.SaveAs(Path.Combine(pathForSaving, model.ImageNormal.FileName));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public virtual ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(Products model)
        {
            if (ModelState.IsValid)
            {
                var item = new Products
                {
                    DateAdded = DateTime.Now,
                    CategoryId = model.CategoryId,
                    ImageName = model.ImageName,
                    Name = model.Name,
                    OrderDetails = new Collection<OrderDetails>(),
                    OrdersCount = 0,
                    Packing = model.Packing,
                    Price=model.Price,
                    Promotion = model.Promotion,
                    Producer = model.Producer                    
                };
                if (item.Quantity < 0)
                    item.Quantity = 0;
                _dbContext.Products.Add(item);
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Delete(int id)
        {
            var item = _dbContext.Products.Find(id);
            return View(item);
        }

        [HttpPost]
        [ActionName("Delete")]
        public virtual ActionResult DeletePost(int id)
        {

            var item = _dbContext.Products.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            _dbContext.Products.Remove(item);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        public virtual ActionResult Edit(int id)
        {

            var item = _dbContext.Products.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(Products model)
        {

            var item = _dbContext.Products.Find(model.ProductId);
            item.Price = model.Price;
            item.Quantity = model.Quantity;
            item.CategoryId = model.CategoryId;
            item.Name = model.Name;
            item.ImageName = model.ImageName;
            item.Packing = model.Packing;
            item.Producer = model.Producer;
            item.Promotion = model.Promotion;
            
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        [HttpGet]
        public virtual ActionResult Details(int id)
        {
            var prod = _dbContext.Products.Find(id);

            return View(prod);
        }

        private bool CreateFolderIfNeeded(string path)
        {
            var result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }
    }
}