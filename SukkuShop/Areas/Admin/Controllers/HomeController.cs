using System.Web.Mvc;

namespace SukkuShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class HomeController : Controller
    {
        // GET: Admin/AdminHome
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}