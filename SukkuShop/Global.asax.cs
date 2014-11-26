using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SukkuShop.Controllers;
using SukkuShop.Infrastructure.Binders;
using SukkuShop.Models;

namespace SukkuShop
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelMetadataProviders.Current = new CachedDataAnnotationsModelMetadataProvider();
            Database.SetInitializer(new ApplicationDbContext.DropCreateInitializer());
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }

        protected void Application_EndRequest()
        {
            if (Context.Response.StatusCode != 404) return;
            Response.Clear();
            Server.ClearError();
            var rd = new RouteData();
            rd.DataTokens["area"] = "";
            rd.Values["controller"] = "Error";
            rd.Values["action"] = "Blad404";
            Response.TrySkipIisCustomErrors = true;
            IController c = new ErrorController();
            c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
        }
    }
}
