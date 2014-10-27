using System;
using System.Linq;
using System.Web;
using System.Web.Routing;
using SukkuShop.Models;

namespace SukkuShop.Infrastructure
{
    public class NotEqual : IRouteConstraint
    {
        private readonly string _match = String.Empty;

        public NotEqual(string match)
        {
            _match = match;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return String.Compare(values[parameterName].ToString(), _match, true) != 0;
        }
    }

    
}