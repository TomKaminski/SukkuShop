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
        private readonly List<Products> _products = new List<Products>
        {
            new Products {Name = "Produkt1", ProductId = 1, Price = 10},
            new Products {Name = "Produkt2", ProductId = 2, Price = 20},
            new Products {Name = "Produkt3", ProductId = 3, Price = 30},
            new Products {Name = "Produkt4", ProductId = 4, Price = 40},
            new Products {Name = "Produkt5", ProductId = 5, Price = 50},
            new Products {Name = "Produkt6", ProductId = 6, Price = 60},
        };

        [TestMethod]
        public void GetProduct()
        {
            //Arrange
            var mock = new Mock<IShop>();
            mock.As<IShop>().Setup(m => m.Products).Returns(_products.AsQueryable);

            //act
            var plz = mock.Object.Products.FirstOrDefault(x => x.ProductId == 6);

            //assert
            Assert.AreEqual(plz.Name, "Produkt6");
            Assert.AreEqual(plz.ProductId, 6);
        }

        //[TestMethod]
        //public void SortProducts()
        //{
        //    //Arrange
        //    var mock = new Mock<IShop>();
        //    mock.As<IShop>().Setup(m => m.Products).Returns(_products.AsQueryable);

        //    //act
        //    var plz = mock.Object.SortProducts(mock.Object.Products, SortMethod.CenaRosnąco);

        //    //assert
        //    var i = 0;
        //    foreach (var item in plz)
        //    {

        //        Assert.AreEqual(item.Price,_products[i].Price);
        //        i++;
        //    }
        //}
    }
}