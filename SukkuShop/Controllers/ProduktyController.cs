using System.Web.Mvc;

namespace SukkuShop.Controllers
{
    public class ProduktyController : Controller
    {
        // GET: Produkty
        public ActionResult Index(string category)
        {
            return View();
        }
    }
}