﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SukkuShop.Identity;
using SukkuShop.Models;
using WebGrease.Css.Extensions;

namespace SukkuShop.Areas.Admin.Controllers
{
    public partial class StatystykiController : Controller
    {

        private readonly ApplicationDbContext _dbContext;

        public StatystykiController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
            var datesOfOrders = _dbContext.Orders.Select(j => j.OrderDate).ToList();
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
            var datesOfOrders = _dbContext.OrderDetails.Where(k=>k.Orders.OrderInfo!="Anulowano").Select(j => new
            {
                name = j.Products.Name,
                quantity = j.Quantity,
                date = j.Orders.OrderDate
            }).ToList();

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
                _dbContext.OrderDetails.Where(k => k.Orders.OrderInfo != "Anulowano")
                    .Select(m => new
                    {
                        m.Quantity,
                        m.Products.Name
                    }).GroupBy(j => j.Name).Select(y=>new
                    {
                        name=y.Key,
                        sum=y.Select(m=>m.Quantity).Sum()
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

        public virtual JsonResult GetOrdersByCategory()
        {
            var data = _dbContext.Products.Where(m=>m.Categories.UpperCategoryId==0).Select(x => new
            {
                name = x.Name,
                orders = x.OrdersCount,
                category = x.Categories.Name
            }).GroupBy(k=>k.category).Select(x=>new
            {
                x.Key,
                sum = x.Sum(j=>j.orders)
            }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}