using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DevTrends.MvcDonutCaching;

namespace SukkuShop.Controllers
{

    public partial class HomeController : Controller
    {
        //[DonutOutputCache(Duration = 86400, Location = OutputCacheLocation.Server)]
        public virtual ViewResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public virtual PartialViewResult LoginPartial()
        {
            return PartialView(MVC.Shared.Views._LoginPartial);
        }

        public virtual ViewResult Regulamin()
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/regulamin.html");
            var model = new HtmlString(System.IO.File.ReadAllText(path));            
            return View(model);
        }

        public virtual ViewResult Reklamacje()
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/reklamacje.html");
            var model = new HtmlString(System.IO.File.ReadAllText(path));           
            return View(model);
        }

        public virtual ViewResult Platnosci()
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/platnosci.html");
            var model = new HtmlString(System.IO.File.ReadAllText(path));  
            return View(model);
        }

        public virtual ViewResult PolitykaPrywatnosci()
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/politykaprywatnosci.html");
            var model = new HtmlString(System.IO.File.ReadAllText(path));  
            return View(model);
        }

        public virtual ViewResult Gwarancje()
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/gwarancja.html");
            var model = new HtmlString(System.IO.File.ReadAllText(path));
            return View(model);
        }

        public virtual ViewResult ZwrotTowarow()
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/zwrottowarow.html");
            var model = new HtmlString(System.IO.File.ReadAllText(path));
            return View(model);
        }
    }
}