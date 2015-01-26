using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using SukkuShop.Models;

namespace SukkuShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class KlienciController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public KlienciController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}