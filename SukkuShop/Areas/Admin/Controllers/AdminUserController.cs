using System.Linq;
using System.Web.Mvc;
using SukkuShop.Models;

namespace SukkuShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class AdminUserController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminUserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Admin/AdminUser/Index

        public virtual ActionResult Index()
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
    }
}