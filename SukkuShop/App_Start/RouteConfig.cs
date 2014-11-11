using System.Web.Mvc;
using System.Web.Routing;
using SukkuShop.Infrastructure;

namespace SukkuShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "{action}/{id}",
                new
                {
                    controller = "Sklep",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                new
                {
                    id = @"\d+",
                    action = "SzczegółyProduktu"
                });

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                new
                {
                    controller = "Home|Nav|Konto"
                });

            routes.MapRoute(null,
                "{action}",
                new
                {
                    controller = "Sklep",
                    search = (string)null
                },
                new
                {
                    action = "Wyszukaj"
                }
                );

            routes.MapRoute(null,
                "{action}/{search}/{method}/{page}",
                new
                {
                    controller = "Sklep",                    
                    page = 1
                },
                new
                {
                    page = @"\d+",
                    method = new IsEnum(),
                    action = "Wyszukaj"
                }
                );

            routes.MapRoute(null,
                "{action}/{method}/{page}",
                new
                {
                    controller = "Sklep",
                    category = (string)null,
                    subcategory = (string)null,
                    page=1
                },
                new
                {
                    page = @"\d+",
                    method = new IsEnum()
                }
                );

            routes.MapRoute(null,
                "{action}/{category}/{method}/{page}",
                new
                {
                    controller = "Sklep",
                    subcategory = (string)null,
                    page=1
                },
                new
                {
                    page = @"\d+",
                    method = new IsEnum()
                }
                );

            routes.MapRoute(null,
                "{action}/{category}/{subcategory}/{method}/{page}",
                new
                {
                    controller = "Sklep",
                    page = 1
                },
                new
                {
                    page = @"\d+",
                    method = new IsEnum()
                }
                );
        }
    }
}