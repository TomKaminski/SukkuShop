using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SukkuShop.Controllers;
using SukkuShop.Models;

namespace SukkuShop.Tests
{
    [TestClass]
    public class SklepControllerTests
    {
        private readonly IQueryable<Products> _products = new List<Products>
        {
            new Products {Name = "Produkt1", ProductId = 1, Price = 10, CategoryId = 1,DateAdded = DateTime.Now,OrdersCount = 5,ImageName = "asd.png",Promotion = 0,Categories = new Categories {CategoryId = 1, Name = "Kosmetyki"}},
            new Products {Name = "Produkt2", ProductId = 2, Price = 20, CategoryId = 2,DateAdded = DateTime.Now,OrdersCount = 6,ImageName = "asd.png",Promotion = 0,Categories = new Categories {CategoryId = 2, Name = "Bakalie"}},
            new Products {Name = "Produkt3", ProductId = 3, Price = 30, CategoryId = 2,DateAdded = DateTime.Now,OrdersCount = 7,ImageName = "asd.png",Promotion = 0,Categories = new Categories {CategoryId = 2, Name = "Bakalie"}},
            new Products {Name = "Produkt4", ProductId = 4, Price = 40, CategoryId = 3,DateAdded = DateTime.Now,OrdersCount = 8,ImageName = "asd.png",Promotion = 0,Categories = new Categories {CategoryId = 3, Name = "Do ciała", UpperCategoryId = 1}},
            new Products {Name = "Produkt5", ProductId = 5, Price = 50, CategoryId = 3,DateAdded = DateTime.Now,OrdersCount = 9,ImageName = "asd.png",Promotion = 0,Categories = new Categories {CategoryId = 3, Name = "Do ciała", UpperCategoryId = 1}},
            new Products {Name = "SearchItemTest", ProductId = 6, Price = 60, CategoryId = 4,DateAdded = DateTime.Now,OrdersCount = 0,ImageName = "asd.png",Promotion = 0,Categories = new Categories {CategoryId = 4, Name = "Do twarzy", UpperCategoryId = 1}},
        }.AsQueryable();

        private readonly IQueryable<Categories> _categories = new List<Categories>
        {
            new Categories {CategoryId = 1, Name = "Kosmetyki"},
            new Categories {CategoryId = 2, Name = "Bakalie"},
            new Categories {CategoryId = 3, Name = "Do ciała", UpperCategoryId = 1},
            new Categories {CategoryId = 4, Name = "Do twarzy", UpperCategoryId = 1}
        }.AsQueryable();

        [TestMethod]
        public void GetFilteredProducts()
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
            mockSetCategories.As<IQueryable<Categories>>().Setup(m => m.GetEnumerator()).Returns(_categories.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSetProducts.Object);
            mockContext.Setup(c => c.Categories).Returns(mockSetCategories.Object);

            var shop = new Shop();
            var controller = new SklepController(mockContext.Object, shop);
            
            
            //Act -> category
            var result = (ViewResult)controller.Produkty("Kosmetyki");
            var model = (ProductsListViewModel)result.Model;
            //Assert
            Assert.AreEqual(model.Products.Count(),4);
            Assert.AreEqual(model.CurrentCategory, "Kosmetyki");
            Assert.AreEqual(model.CurrentSortMethod, SortMethod.Nowość);
            
            //Act -> subcategory
            result = (ViewResult)controller.Produkty("Kosmetyki","Do ciała");
            model = (ProductsListViewModel)result.Model;
            //Assert
            Assert.AreEqual(model.Products.Count(), 2);
            Assert.AreEqual(model.CurrentCategory, "Kosmetyki");
            Assert.AreEqual(model.CurrentSortMethod, SortMethod.Nowość);

            //Act -> all products
            result = (ViewResult)controller.Produkty(null,null,SortMethod.CenaMalejaco);
            model = (ProductsListViewModel)result.Model;
            //Assert
            Assert.AreEqual(model.Products.Count(), 6);
            Assert.AreEqual(model.Products.First().Price,60);
            Assert.AreEqual(model.CurrentCategory,null);
            Assert.AreEqual(model.CurrentSortMethod, SortMethod.CenaMalejaco);

            //Act -> search
            result = (ViewResult)controller.Wyszukaj("search",SortMethod.CenaMalejaco);
            model = (ProductsListViewModel)result.Model;
            //Assert
            Assert.AreEqual(model.Products.Count(),1);
            Assert.AreEqual(model.Products.First().Id,6);

        }        
    }
}