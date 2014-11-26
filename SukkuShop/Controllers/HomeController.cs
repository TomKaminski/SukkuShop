using System.Web.Mvc;
using System.Web.UI;
using DevTrends.MvcDonutCaching;

namespace SukkuShop.Controllers
{

    public partial class HomeController : Controller
    {
        [DonutOutputCache(Duration = 86400, Location = OutputCacheLocation.Server)]
        public virtual ViewResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public virtual PartialViewResult LoginPartial()
        {
            return PartialView(MVC.Shared.Views._LoginPartial);
        }



        [DonutOutputCache(Duration = 86400, Location = OutputCacheLocation.Server)]
        public virtual ViewResult Regulamin()
        {
            return View();
        }

        [DonutOutputCache(Duration = 86400, Location = OutputCacheLocation.Server)]
        public virtual ViewResult Dostawa()
        {
            return View();
        }

        [DonutOutputCache(Duration = 86400, Location = OutputCacheLocation.Server)]
        public virtual ViewResult Platnosci()
        {
            return View();
        }

        [DonutOutputCache(Duration = 86400, Location = OutputCacheLocation.Server)]
        public virtual ViewResult PolitykaPrywatnosci()
        {
            return View();
        }
    }
}