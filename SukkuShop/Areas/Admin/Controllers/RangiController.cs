using System.Web.Mvc;

namespace SukkuShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class RangiController : Controller
    {
        // GET: Admin/AdminRole
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}