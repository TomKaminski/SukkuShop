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
    public class KoszykControllerTests
    {
        private readonly IQueryable<Products> _products = new List<Products>
        {
            new Products {ReservedQuantity = 50,Quantity = 100,Name = "Produkt1", ProductId = 1, Price = (decimal)(10.20), CategoryId = 1,DateAdded = DateTime.Now,OrdersCount = 5,ImageName = "asd.png",Promotion = 0,Categories = new Categories {CategoryId = 1, Name = "Kosmetyki"}},
            new Products {ReservedQuantity = 50,Quantity = 100,Name = "Produkt2", ProductId = 2, Price = 20, CategoryId = 2,DateAdded = DateTime.Now,OrdersCount = 6,ImageName = "asd.png",Promotion = 10,Categories = new Categories {CategoryId = 2, Name = "Bakalie"}},
            new Products {ReservedQuantity = 100,Quantity = 100,Name = "Produkt3", ProductId = 3, Price = 30, CategoryId = 2,DateAdded = DateTime.Now,OrdersCount = 7,ImageName = "asd.png",Promotion = 5,Categories = new Categories {CategoryId = 2, Name = "Bakalie"}},
            new Products {ReservedQuantity = 100,Quantity = 100,Name = "Produkt4", ProductId = 4, Price = 40, CategoryId = 3,DateAdded = DateTime.Now,OrdersCount = 8,ImageName = "asd.png",Promotion = 0,Categories = new Categories {CategoryId = 3, Name = "Do ciała", UpperCategoryId = 1}},
            new Products {ReservedQuantity = 20,Quantity = 100,Name = "Produkt5", ProductId = 5, Price = (decimal)(50.99), CategoryId = 3,DateAdded = DateTime.Now,OrdersCount = 9,ImageName = "asd.png",Promotion = 5,Categories = new Categories {CategoryId = 3, Name = "Do ciała", UpperCategoryId = 1}},
            new Products {ReservedQuantity = 0,Quantity = 100,Name = "SearchItemTest", ProductId = 6, Price = 60, CategoryId = 4,DateAdded = DateTime.Now,OrdersCount = 0,ImageName = "asd.png",Promotion = 0,Categories = new Categories {CategoryId = 4, Name = "Do twarzy", UpperCategoryId = 1}},
        }.AsQueryable();


         private readonly IQueryable<Categories> _categories = new List<Categories>
        {
            new Categories {CategoryId = 1, Name = "Kosmetyki"},
            new Categories {CategoryId = 2, Name = "Bakalie"},
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
            mockSetCategories.As<IQueryable<Categories>>().Setup(m => m.GetEnumerator()).Returns(_categories.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSetProducts.Object);
            mockContext.Setup(c => c.Categories).Returns(mockSetCategories.Object);

            var cart = new Cart();
            var controller = new KoszykController(mockContext.Object);

            //act
            controller.AddToCart(3, cart, 10);

            //assert
            var firstOrDefault = cart.Lines.FirstOrDefault(x=>x.Id==3);
            if (firstOrDefault != null)
            {
                Assert.AreEqual(firstOrDefault.Quantity, 10);                
            }
            Assert.AreEqual(cart.Lines.Count(), 0);

            //act
            controller.AddToCart(1, cart, 10);

            //assert
            firstOrDefault = cart.Lines.FirstOrDefault(x => x.Id == 1);
            if (firstOrDefault != null)
            {
                Assert.AreEqual(firstOrDefault.Quantity, 10);
                Assert.AreEqual(cart.Lines.Count(),1);
            }

            controller.RemoveFromCart(1, cart);
            Assert.AreEqual(cart.Lines.Count(),0);

            controller.AddToCart(1, cart, 10);
            controller.IncreaseQuantity(1, cart);
            var orDefault = cart.Lines.FirstOrDefault(x=>x.Id==1);
            if (orDefault != null)
                Assert.AreEqual(orDefault.Quantity,11);
            controller.DecreaseQuantity(1, cart);
            orDefault = cart.Lines.FirstOrDefault(x => x.Id == 1);
            if (orDefault != null)
                Assert.AreEqual(orDefault.Quantity, 10);

            controller.AddToCart(2, cart, 10);

            controller.Index(cart);

            var result = (ViewResult)controller.Index(cart);
            var model = (CartViewModels)result.Model;

            Assert.AreEqual(model.CartProductsList.Count(),2);
            Assert.AreEqual(model.TotalValue, ((CalcPrice((decimal?)10.20, 0)) * 10 + (CalcPrice(20, 10)) * 10).ToString("c").Replace(",","."));
        }

        private static decimal CalcPrice(decimal? price, int? promotion)
        {
            var pricee = (price - ((price * promotion) / 100)) ?? price;
            return Math.Floor((pricee ?? 0) * 100) / 100;
        }
    }
}
