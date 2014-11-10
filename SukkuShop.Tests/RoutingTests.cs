using System;
using System.Linq;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using Moq;
using SukkuShop.Models;


namespace SukkuShop.Tests
{
    [TestClass]
    public class RoutingTests
    {
        private static HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET")
        {
            // create the mock request
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            // create the mock response
            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            // create the mock context, using the request and response
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            // return the mocked context
            return mockContext.Object;
        }

        private static void TestRouteMatch(string url, string controller, string action, object routeProperties = null,
            string httpMethod = "GET")
        {

            // Arrange
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act - process the route
            var result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
        }

        private static bool TestIncomingRouteResult(RouteData routeResult, string controller, string action,
            object propertySet = null)
        {
            Func<object, object, bool> valCompare =
                (v1, v2) => StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;

            var result = valCompare(routeResult.Values["controller"],
                controller) && valCompare(routeResult.Values["action"], action);

            if (propertySet == null) return result;
            var propInfo = propertySet.GetType().GetProperties();
            if (propInfo.Any(pi => !(routeResult.Values.ContainsKey(pi.Name)
                                     && valCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null)))))
            {
                result = false;
            }
            return result;
        }

        private static void TestRouteFail(string url)
        {
            // Arrange
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act - process the route
            var result = routes.GetRouteData(CreateHttpContext(url));

            // Assert
            Assert.IsTrue(result == null || result.Route == null);
        }

        //[TestMethod]
        //public void TestIncomingRoutes()
        //{
        //    TestRouteMatch("∼/", "Home", "Index");
        //    TestRouteMatch("∼/Home", "Home", "Index");
        //    TestRouteMatch("∼/Home/Index", "Home", "Index");
        //    TestRouteMatch("~/Produkty", "Sklep", "Produkty", new { method = SortMethod.Nowość, page = 1, category = (string)null });
        //    TestRouteMatch("~/Kosmetyk", "Sklep", "Produkty", new { method = SortMethod.Nowość, page = 1, category = "Kosmetyk" });
        //    TestRouteMatch("~/Produkty/5", "Sklep", "Produkty", new { category = (string)null, method = SortMethod.Nowość, page = "5" });
        //    TestRouteMatch("~/Produkty/Promocja/15", "Sklep", "Produkty", new { category = (string)null, method = SortMethod.Promocja.ToString(), page = "15" });
        //    TestRouteMatch("~/Sklep/SzczegółyProduktu/5","Sklep","SzczegółyProduktu",new{id="5"});

        //    TestRouteFail("~/Produkty/Promocja/Kosmetyki/15");
        //}

    }
}