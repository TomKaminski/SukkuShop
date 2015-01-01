using System.Web.Mvc;
using System.Web.Routing;

namespace SukkuShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(null, "{controller}/{action}/{category}",new
            //{
            //    area = ""
            //},
            //new
            //{
            //    action = "GetProductByCategory"
            //});

            //routes.MapRoute(null, "{action}/{id}",
            //    new
            //    {
            //        controller = "Sklep",
            //        action = "Index",
            //        id = UrlParameter.Optional,
            //        area = ""
            //    },
            //    new
            //    {
            //        id = @"\d+",
            //        action = "SzczegółyProduktu"
            //    });

            //routes.MapRoute(null,
            //   "{action}",
            //   new
            //   {
            //       controller = "Sklep",
            //       area = ""
            //   },
            //   new
            //   {
            //       action = "Szukaj"
            //   }
            //   );

            //routes.MapRoute(null,
            //    "{action}/{search}",
            //    new
            //    {
            //        controller = "Sklep",
            //        area = ""
            //    },
            //    new
            //    {
            //        action = "Wyszukaj"
            //    }
            //    );

            //routes.MapRoute(null,
            //    "{action}/{category}",
            //    new
            //    {
            //        controller = "Sklep",
            //        category = (string)null,
            //        area = ""
            //    },
            //    new
            //    {
            //        action = "Produkty",
            //    }
            //    );

            

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional,
                    area = ""
                }
                );

            routes.MapRoute(
                 "404-PageNotFound",
                 "{*url}",
                 new
                 {
                     controller = "Error", action = "Blad404" 
                 
                 }
                 );

            
        }
    }
}