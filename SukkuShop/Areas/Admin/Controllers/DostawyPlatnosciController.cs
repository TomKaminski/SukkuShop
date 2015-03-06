using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;
using SukkuShop.Areas.Admin.Models;
using SukkuShop.Infrastructure.Generic;
using SukkuShop.Models;

namespace SukkuShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class DostawyPlatnosciController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAppRepository _appRepository;

        public DostawyPlatnosciController(ApplicationDbContext dbContext, IAppRepository appRepository)
        {
            _dbContext = dbContext;
            _appRepository = appRepository;
        }

        // GET: Admin/DostawyPlatnosci
        public virtual ActionResult Index()
        {
            ViewBag.SelectedOpt = 7;
            return View();

        }

        public virtual JsonResult GetDataJson()
        {
            return Json(new
            {
                shipping =_appRepository.GetAll<ShippingType>().Select(x => new
                {
                    x.ShippingId,
                    x.ShippingDescription,
                    x.ShippingName,
                    x.ShippingPrice,
                    x.Active,
                    canDelete = x.Orders.Count==0,
                    editActive = false,
                    x.MaxWeight,
                    editWeight = false
                }),
                payment = _appRepository.GetAll<PaymentType>().Select(x => new
                {
                    x.PaymentId,
                    x.PaymentDescription,
                    x.PaymentName,
                    x.PaymentPrice,
                    x.Active,
                    canDelete = x.Orders.Count == 0,
                    editActive = false
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult DeletePayment(int id)
        {
            var payment = _appRepository.GetSingle<PaymentType>(m => m.PaymentId == id);
            if (payment == null) return Json(false);
            if (payment.Orders.Count != 0) return Json(false);
            _dbContext.PaymentTypes.Remove(payment);
            _dbContext.SaveChanges();
            return Json(true);
        }
        public virtual ActionResult DeleteShipping(int id)
        {
            var shipping = _appRepository.GetSingle<ShippingType>(m => m.ShippingId == id);
            if (shipping == null) return Json(false);
            if (shipping.Orders.Count != 0) return Json(false);
            _dbContext.ShippingTypes.Remove(shipping);
            _dbContext.SaveChanges();
            return Json(true);
        }

        //Angular activate payment
        public virtual ActionResult ActivatePayment(int id)
        {
            var payment = _appRepository.GetSingle<PaymentType>(m => m.PaymentId == id);
            if (payment != null)
            {
                payment.Active = true;
                _dbContext.PaymentTypes.AddOrUpdate(payment);
                _dbContext.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        //Angular deactivate payment
        public virtual ActionResult DeactivatePayment(int id)
        {
            var payment = _appRepository.GetSingle<PaymentType>(m => m.PaymentId == id);
            if (payment != null)
            {
                payment.Active = false;
                _dbContext.PaymentTypes.AddOrUpdate(payment);
                _dbContext.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        //Angular activate shipping
        public virtual ActionResult ActivateShipping(int id)
        {
            var shipping = _appRepository.GetSingle<ShippingType>(m => m.ShippingId == id);
            if (shipping != null)
            {
                shipping.Active = true;
                _dbContext.ShippingTypes.AddOrUpdate(shipping);
                _dbContext.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        //Angular deactivate shipping
        public virtual ActionResult DeactivateShipping(int id)
        {
            var shipping = _appRepository.GetSingle<ShippingType>(m => m.ShippingId == id);
            if (shipping != null)
            {
                shipping.Active = false;
                _dbContext.ShippingTypes.AddOrUpdate(shipping);
                _dbContext.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual JsonResult AddPayment(ShippingPaymentCreateModel model)
        {
            var payment = new PaymentType
            {
                Active = false,
                PaymentDescription = model.Description,
                PaymentName = model.Name,
                PaymentPrice = Convert.ToDecimal(model.Price.Replace(".",","))
            };
            try
            {
                _dbContext.PaymentTypes.Add(payment);
                _dbContext.SaveChanges();
                var pay = _dbContext.PaymentTypes.OrderByDescending(x => x.PaymentId).First();
                return Json(new
                {
                    pay.PaymentId,
                    pay.PaymentDescription,
                    pay.PaymentName,
                    pay.PaymentPrice,
                    canDelete = pay.Orders.Count == 0,
                    editActive = false
                }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public virtual JsonResult AddShipping(ShippingPaymentCreateModel model)
        {
            var shipping = new ShippingType
            {
                Active = false,
                ShippingDescription = model.Description,
                ShippingName = model.Name,
                ShippingPrice = Convert.ToDecimal(model.Price.Replace(".", ",")),
                MaxWeight = Convert.ToDecimal(model.MaxWeight.Replace(".", ","))
            };
            try
            {
                _dbContext.ShippingTypes.Add(shipping);
                _dbContext.SaveChanges();
                var ship = _dbContext.ShippingTypes.OrderByDescending(x => x.ShippingId).First();
                return Json(new
                {
                    ship.ShippingId,
                    ship.ShippingDescription,
                    ship.ShippingName,
                    ship.ShippingPrice,
                    canDelete = ship.Orders.Count == 0,
                    editActive = false,
                    ship.MaxWeight,
                    editWeight = false
                }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public virtual JsonResult EditShippingWeight(int id, string weight)
        {
            var firstOrDefault = _appRepository.GetSingle<ShippingType>(x => x.ShippingId == id);
            if (firstOrDefault != null)
            {
                firstOrDefault.MaxWeight = Convert.ToDecimal(weight.Replace(".", ","));
                _dbContext.ShippingTypes.AddOrUpdate(firstOrDefault);
                _dbContext.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual JsonResult EditShippingDescription(int id, string description)
        {
            var firstOrDefault = _appRepository.GetSingle<ShippingType>(x => x.ShippingId == id);
            if (firstOrDefault != null)
            {
                firstOrDefault.ShippingDescription = description;
                _dbContext.ShippingTypes.AddOrUpdate(firstOrDefault);
                _dbContext.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        

        [HttpPost]
        public virtual JsonResult EditPaymentDescription(int id, string description)
        {
            var firstOrDefault = _appRepository.GetSingle<PaymentType>(x => x.PaymentId == id);
            if (firstOrDefault != null)
            {
                firstOrDefault.PaymentDescription = description;
                _dbContext.PaymentTypes.AddOrUpdate(firstOrDefault);
                _dbContext.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}