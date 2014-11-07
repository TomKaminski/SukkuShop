using System.Web;
using System.Web.Routing;
using SukkuShop.Models;

namespace SukkuShop.Infrastructure
{
    public class IsEnum : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return values[parameterName] is SortMethod;
        }
    }

    public class NotEnum : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return !(values[parameterName] is SortMethod);
        }
    }    
}