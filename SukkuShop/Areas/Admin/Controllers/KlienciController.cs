using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SukkuShop.Areas.Admin.Models;
using SukkuShop.Identity;
using SukkuShop.Models;

namespace SukkuShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class KlienciController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ApplicationUserManager _userManager;

        public KlienciController(ApplicationDbContext dbContext, ApplicationUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        // GET: Admin/AdminUser/Index

        public virtual ActionResult Lista()
        {
            ViewBag.SelectedOpt = 3;
            return View();
        }

        public virtual JsonResult GetUserList()
        {
            var users = _dbContext.Users.Select(x => new
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
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                user.Rabat = rabat;
                _dbContext.Users.AddOrUpdate(user);
                _dbContext.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public virtual async Task<ActionResult> Szczegóły(int id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var model = new UserDetailsModel
            {
                Ulica = user.Street,
                KodPocztowy = user.PostalCode,
                Telefon = user.PhoneNumber,
                Numer = user.Number,
                Miasto = user.City,
                NameTitle = user.KontoFirmowe?user.NazwaFirmy:user.Name,
                NipUsername = user.KontoFirmowe?user.AccNip:user.LastName,
                Rabat = user.Rabat,
                AccountOrderItemViewModel = _dbContext.Orders.Where(m => m.UserId == user.Id).Select(x => new AccountOrderItemViewModel
            {
                ActualState = x.OrderInfo,
                Id = x.OrderId,
                OrderDate = x.OrderDate.ToShortDateString(),
                TotalPrice = x.TotalPrice
            }).ToList()
            };
            return View(model);
        }
    }
}