using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SukkuShop.Areas.Admin.Controllers;
using SukkuShop.Models;


namespace SukkuShop.Tests
{
    [TestClass]
    public class AdminProduktyControllerTests
    {

        private readonly IQueryable<Products> _products = new List<Products>
        {
            new Products
            {
                ReservedQuantity = 50,
                Quantity = 100,
                Name = "Produkt1",
                ProductId = 1,
                Price = (decimal) (10.20),
                CategoryId = 1,
                DateAdded = DateTime.Now,
                OrdersCount = 5,
                ImageName = "asd.png",
                Promotion = 0,
                Categories = new Categories {CategoryId = 1, Name = "Kosmetyki"}
            },
            new Products
            {
                ReservedQuantity = 50,
                Quantity = 100,
                Name = "Produkt2",
                ProductId = 2,
                Price = 20,
                CategoryId = 2,
                DateAdded = DateTime.Now,
                OrdersCount = 6,
                ImageName = "asd.png",
                Promotion = 10,
                Categories = new Categories {CategoryId = 2, Name = "Bakalie"}
            },
            new Products
            {
                ReservedQuantity = 100,
                Quantity = 100,
                Name = "Produkt3",
                ProductId = 3,
                Price = 30,
                CategoryId = 2,
                DateAdded = DateTime.Now,
                OrdersCount = 7,
                ImageName = "asd.png",
                Promotion = 5,
                Categories = new Categories {CategoryId = 2, Name = "Bakalie"}
            },
            new Products
            {
                ReservedQuantity = 100,
                Quantity = 100,
                Name = "Produkt4",
                ProductId = 4,
                Price = 40,
                CategoryId = 3,
                DateAdded = DateTime.Now,
                OrdersCount = 8,
                ImageName = "asd.png",
                Promotion = 0,
                Categories = new Categories {CategoryId = 3, Name = "Do ciała", UpperCategoryId = 1}
            },
            new Products
            {
                ReservedQuantity = 20,
                Quantity = 100,
                Name = "Produkt5",
                ProductId = 5,
                Price = (decimal) (50.99),
                CategoryId = 3,
                DateAdded = DateTime.Now,
                OrdersCount = 9,
                ImageName = "asd.png",
                Promotion = 5,
                Categories = new Categories {CategoryId = 3, Name = "Do ciała", UpperCategoryId = 1}
            },
            new Products
            {
                ReservedQuantity = 0,
                Quantity = 100,
                Name = "SearchItemTest",
                ProductId = 6,
                Price = 60,
                CategoryId = 4,
                DateAdded = DateTime.Now,
                OrdersCount = 0,
                ImageName = "asd.png",
                Promotion = 0,
                Categories = new Categories {CategoryId = 4, Name = "Do twarzy", UpperCategoryId = 1}
            },
        }.AsQueryable();


        private readonly IQueryable<Categories> _categories = new List<Categories>
        {
            new Categories {CategoryId = 1, Name = "Kosmetyki",UpperCategoryId = 0},
            new Categories {CategoryId = 2, Name = "Bakalie",UpperCategoryId = 0},
            new Categories {CategoryId = 3, Name = "Do ciała", UpperCategoryId = 1},
            new Categories {CategoryId = 4, Name = "Do twarzy", UpperCategoryId = 1}
        }.AsQueryable();

        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var mockSetProducts = new Mock<DbSet<Products>>();
            mockSetProducts.As<IQueryable<Products>>().Setup(m => m.Provider).Returns(_products.Provider);
            mockSetProducts.As<IQueryable<Products>>().Setup(m => m.Expression).Returns(_products.Expression);
            mockSetProducts.As<IQueryable<Products>>().Setup(m => m.ElementType).Returns(_products.ElementType);
            mockSetProducts.As<IQueryable<Products>>().Setup(m => m.GetEnumerator()).Returns(_products.GetEnumerator());

            var mockSetCategories = new Mock<DbSet<Categories>>();
            mockSetCategories.As<IQueryable<Categories>>().Setup(m => m.Provider).Returns(_categories.Provider);
            mockSetCategories.As<IQueryable<Categories>>().Setup(m => m.Expression).Returns(_categories.Expression);
            mockSetCategories.As<IQueryable<Categories>>().Setup(m => m.ElementType).Returns(_categories.ElementType);
            mockSetCategories.As<IQueryable<Categories>>()
                .Setup(m => m.GetEnumerator())
                .Returns(_categories.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSetProducts.Object);
            mockContext.Setup(c => c.Categories).Returns(mockSetCategories.Object);

            var controller = new ProduktyController(mockContext.Object);
            var actual = controller.GetProductList();
            var result = AsRouteValueDictionary(actual);
            var list = (result["products"] as IEnumerable<object>).ToList();
            var list2 = (result["categories"] as IEnumerable<object>).ToList();
            Assert.AreEqual(list.Count, 6);
            Assert.AreEqual(list2.Count, 2);

        }

        public static RouteValueDictionary AsRouteValueDictionary(JsonResult jsonResult)
        {

            return new RouteValueDictionary(jsonResult.Data);

        }
    }
}
