using System;
using System.Web;
using System.Web.Routing;
using SukkuShop.Models;

namespace SukkuShop.Infrastructure
{
    public class IsEnum : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return Enum.IsDefined(typeof (SortMethod), values[parameterName]);
        }
    }
}