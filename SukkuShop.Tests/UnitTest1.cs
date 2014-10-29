using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SukkuShop.Models;
using SukkuShop.Infrastructure.Generic;

namespace SukkuShop.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var products = new List<Products>()
            {
                new Products {Name = "Produkt1", ProductId = 1},
                new Products {Name = "Produkt2", ProductId = 2},
                new Products {Name = "Produkt3", ProductId = 3},
                new Products {Name = "Produkt4", ProductId = 4},
                new Products {Name = "Produkt5", ProductId = 5},
                new Products {Name = "Produkt6", ProductId = 6},
            }.AsQueryable();

            var mock = new Mock<DbSet<Products>>();
            mock.As<IQueryable<Products>>().Setup(m => m.Provider).Returns(products.Provider);
            mock.As<IQueryable<Products>>().Setup(m => m.Expression).Returns(products.Expression);
            mock.As<IQueryable<Products>>().Setup(m => m.ElementType).Returns(products.ElementType);
            mock.As<IQueryable<Products>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());

            var mockContext = new Mock<IAppRepository>();
            mockContext.Setup(c => c.Products).Returns(mock.Object); 
            //act
            var plz = mock.Object.FirstOrDefault(x=>x.ProductId == 6);

            //assert
            Assert.AreEqual(plz.Name, "Produkt6");
            Assert.AreEqual(plz.ProductId, 6);
        }
    }
}