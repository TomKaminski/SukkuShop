using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SukkuShop.Infrastructure.Generic;
using SukkuShop.Models;

namespace SukkuShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class StatystykiController : Controller
    {
        private readonly IAppRepository _appRepository;
        private readonly ApplicationDbContext _dbContext;

        public StatystykiController(ApplicationDbContext dbContext, IAppRepository appRepository)
        {
            _dbContext = dbContext;
            _appRepository = appRepository;
        }

        // GET: Admin/Statystyki
        public virtual ActionResult Index()
        {
            ViewBag.SelectedOpt = 6;
            return View();
        }

        public virtual JsonResult GetOrderData()
        {
            var startDate = DateTime.Now.AddDays(-13);
            var datesOfOrders = _appRepository.GetAll<Orders>().Select(j => j.OrderDate).ToList();
            var groupOrdersByDate = datesOfOrders.GroupBy(x => x.ToShortDateString()).Select(x => new
            {
                Date = x.Key,
                Count = x.Count()
            }).ToList();
            var ordersCount = Enumerable.Range(0, 14).Select(offset =>
            {
                var firstOrDefault = groupOrdersByDate.FirstOrDefault(x => x.Date == startDate.AddDays(offset).ToShortDateString());
                return new
                {
                    date = startDate.AddDays(offset).ToShortDateString(),
                    count = firstOrDefault != null ? firstOrDefault.Count : 0
                };
            }).ToList();

            var startMonth = DateTime.Now.AddMonths(-11);
            var groupOrdersByMonths = datesOfOrders.Select(x => new
            {
                Date = x.ToString("yyyy/MM")
            }).GroupBy(m=>m.Date).Select(m=>new
            {
                date=m.Key,
                sum=m.Count()
            }).ToList();

            var ordersCountMonth = new List<object>();
            for (var i = 0; i < 12; i++)
            {
                var firstOrDefault = groupOrdersByMonths.FirstOrDefault(x => x.date == startMonth.AddMonths(i).ToString("yyyy/MM"));
                ordersCountMonth.Add(new
                {
                    date = startMonth.AddMonths(i).ToString("yyyy/MM"),
                    count = firstOrDefault == null?0:firstOrDefault.sum
                });
            }
            var mergedobj = new
            {
                days = ordersCount,
                months = ordersCountMonth
            };

            return Json(mergedobj, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult GetTopProducts()
        {

            var datesOfOrders = _appRepository.GetAll<OrderDetails>().Select(k =>
            {
                var order = _appRepository.GetSingle<Orders>(x => x.OrderId == k.OrderId);
                return new {
                    order.OrderInfo,
                    quantity = k.Quantity,
                    date = order.OrderDate,
                    name = _appRepository.GetSingle<Products>(n=>n.ProductId==k.ProductId)
                };
            }).Where(k => k.OrderInfo!="Anulowano").ToList();

            //var datesOfOrders = _dbContext.OrderDetails.Where(k => k.Orders.OrderInfo != "Anulowano").Select(j => new
            //{
            //    name = j.Products.Name,
            //    quantity = j.Quantity,
            //    date = j.Orders.OrderDate
            //}).ToList();

            var ordersbymonth = datesOfOrders.Select(x => new
            {
                date = x.date.Month,
                x.name,
                x.quantity
            }).GroupBy(k => new { k.date, k.name }).Select(m => new
            {
                m.Key.date,
                m.Key.name,
                sum = m.Sum(x => x.quantity)
            }).ToList().GroupBy(j=>j.date).Select(x=>new{
                products = x.OrderByDescending(k=>k.sum).Select(j=>new
                {
                    j.name,j.sum
                }).Take(10).ToList(),
                month = x.Key
            }).ToList();

            var total =
                _appRepository.GetAll<OrderDetails>().Select(k =>
                {
                    var order = _appRepository.GetSingle<Orders>(x => x.OrderId == k.OrderId);
                    return new
                    {
                        order.OrderInfo,
                        quantity = k.Quantity,
                        date = order.OrderDate,
                        name = _appRepository.GetSingle<Products>(n => n.ProductId == k.ProductId)
                    };
                }).Where(k => k.OrderInfo != "Anulowano").GroupBy(j => j.name).Select(y => new
                    {
                        name=y.Key,
                        sum=y.Select(m=>m.quantity).Sum()
                    }).ToList();

            //var total = _dbContext.Products.Select(x => new
            //{
            //    name= x.Name,
            //    sum = x.OrderDetails.Sum(k=>k.Quantity)
            //}).OrderByDescending(k=>k.sum).Take(10).ToList();

            var mergedObj = new
            {
                total,
                ordersbymonth
            };
            return Json(mergedObj, JsonRequestBehavior.AllowGet);
        }

        //public virtual JsonResult GetOrdersByCategory()
        //{
        //    var data = _dbContext.Products.Where(m=>m.Categories.UpperCategoryId==0).Select(x => new
        //    {
        //        name = x.Name,
        //        orders = x.OrdersCount,
        //        category = x.Categories.Name
        //    }).GroupBy(k=>k.category).Select(x=>new
        //    {
        //        x.Key,
        //        sum = x.Sum(j=>j.orders)
        //    }).ToList();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
    }
}