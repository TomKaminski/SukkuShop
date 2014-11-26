using System.Web.Mvc;

namespace SukkuShop.Areas.Admin.Controllers
{
    public partial class AdminHomeController : Controller
    {
        // GET: Admin/AdminHome
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}