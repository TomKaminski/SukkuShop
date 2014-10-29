using System.Web.Mvc;
using System.Web.Routing;
using SukkuShop.Models;

namespace SukkuShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

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
                "Produkty",
                new
                {
                    controller = "Sklep",
                    action = "Produkty",
                    category = (string) null,
                    method = SortMethod.Nowości,
                    page = 1,
                }
                );

            routes.MapRoute(null,
                "Produkty/{page}",
                new
                {
                    controller = "Sklep",
                    action = "Produkty",
                    method = SortMethod.Nowości,
                    category =
                        (string) null
                },
                new {page = @"\d+"}
                );

            routes.MapRoute(null,
                "{category}",
                new
                {
                    controller = "Sklep",
                    action = "Produkty",
                    page = 1,
                    method = SortMethod.Nowości
                }
                );

            routes.MapRoute(null,
                "{category}/{page}",
                new
                {
                    controller = "Sklep", action = "Produkty", method = SortMethod.Nowości
                },
                new
                {
                    page = @"\d+"
                }
                );

            routes.MapRoute(null,
                "Produkty/{method}",
                new
                {
                    controller = "Sklep",
                    action = "Produkty",
                    page = 1,
                    category = (string) null
                }
                );

            routes.MapRoute(null,
                "Produkty/{method}/{page}",
                new
                {
                    controller = "Sklep", action = "Produkty", category = (string) null
                },
                new
                {
                    page = @"\d+"
                }
                );

            routes.MapRoute(null, "{controller}/{action}/{id}",
                new
                {
                    controller = "Home", action = "Index", id = UrlParameter.Optional
                },
                new
                {
                    controller = "Sklep", action = "SzczegółyProduktu"
                });

            routes.MapRoute(null,
                "{category}/{method}",
                new
                {
                    controller = "Sklep", action = "Produkty", page = 1
                }
                );

            routes.MapRoute(null,
                "{category}/{method}/{page}",
                new
                {
                    controller = "Sklep", action = "Produkty"
                },
                new
                {
                    page = @"\d+"
                }
                );
        }
    }
}