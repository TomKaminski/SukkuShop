using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SukkuShop.Models;

namespace SukkuShop.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly Products[] _products =
        {
            new Products {Name = "Produkt1", ProductId = 1},
            new Products {Name = "Produkt2", ProductId = 2},
            new Products {Name = "Produkt3", ProductId = 3},
            new Products {Name = "Produkt4", ProductId = 4},
            new Products {Name = "Produkt5", ProductId = 5},
            new Products {Name = "Produkt6", ProductId = 6},
        };
        [TestMethod]
        public void TestMethod1()
        {
            //act
            var result = _products.FirstOrDefault(x => x.ProductId == 5);

            //assert
            Assert.AreSame(result,_products[4]);

        }
    }
}