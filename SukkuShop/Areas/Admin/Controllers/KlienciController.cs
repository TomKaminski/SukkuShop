using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SukkuShop.Areas.Admin.Models;
using SukkuShop.Identity;
using SukkuShop.Infrastructure.Generic;
using SukkuShop.Models;

namespace SukkuShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class KlienciController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ApplicationUserManager _userManager;
        private readonly IAppRepository _appRepository;

        public KlienciController(ApplicationDbContext dbContext, ApplicationUserManager userManager, IAppRepository appRepository)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _appRepository = appRepository;
        }

        // GET: Admin/AdminUser/Index

        public virtual ActionResult Lista()
        {
            ViewBag.SelectedOpt = 3;
            return View();
        }

        public virtual JsonResult GetUserList()
        {
            
            var users = _userManager.Users.Select(x => new
            {
                x.Email,
                x.Id,
                Orders = x.Orders.Count,
                x.Rabat,
                HasDiscount = x.Rabat != 0
            });
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult SetDiscount(int id, int rabat)
        {
            var user = _userManager.FindById(id);
            if (user != null)
            {
                user.Rabat = rabat;
                _dbContext.Users.AddOrUpdate(user);
                _dbContext.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public virtual async Task<ActionResult> Szczegóły(int id=1)
        {
            ViewBag.SelectedOpt = 3;
            var user = await _userManager.FindByIdAsync(id);

            var userOrders = _appRepository.GetAll<Orders>(m => m.UserId == user.Id).Select(x => new AccountOrderItemModel
            {
                ActualState = x.OrderInfo,
                Id = x.OrderId,
                OrderDate = x.OrderDate,
                TotalPrice = x.TotalPrice
            }).ToList();

            var model = new UserDetailsModel
            {
                Ulica = user.Street??"Nie podano",
                KodPocztowy = user.PostalCode ?? "Nie podano",
                Telefon = user.PhoneNumber ?? "Nie podano",
                Numer = user.Number ?? "Nie podano",
                Miasto = user.City ?? "Nie podano",
                NameTitle = (user.KontoFirmowe ? user.NazwaFirmy : user.Name) ?? "Nie podano",
                NipUsername = (user.KontoFirmowe ? user.AccNip : user.LastName) ?? "Nie podano",
                Rabat = user.Rabat,
                KontoFirmowe = user.KontoFirmowe,
                OrdersCount = userOrders.Count,
                Id = user.Id,
                AccountOrderItemViewModel = userOrders.Select(itemModel => new AccountOrderItemViewModel
                {
                    ActualState = itemModel.ActualState,
                    Id = itemModel.Id,
                    OrderDate = itemModel.OrderDate.ToShortDateString(),
                    TotalPrice = itemModel.TotalPrice
                }).ToList()
            };
            return View(model);
        }
    }
}