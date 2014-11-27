using System;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using SukkuShop.Models;

namespace SukkuShop.Areas.Admin.Controllers
{
    public class AdminProductController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminProductController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Admin/AdminProduct
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products model)
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
        public ActionResult Delete(int id)
        {
            var item = _dbContext.Products.Find(id);
            return View(item);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
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

        public ActionResult Edit(int id)
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
        public ActionResult Edit(Products model)
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
        public ActionResult Details(int id)
        {
            var prod = _dbContext.Products.Find(id);

            return View(prod);
        }
    }
}