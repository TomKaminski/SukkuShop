using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SukkuShop.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(CartProduct product, int quantity = 1)
        {
            CartLine line = lineCollection.FirstOrDefault(p => p.CartProduct.Id == product.Id);

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    CartProduct = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
        public void RemoveLine(CartProduct cartProduct)
        {
            lineCollection.RemoveAll(l => l.CartProduct.Id == cartProduct.Id);
        }

        public decimal TotalValue()
        {
            return lineCollection.Sum(e => e.CartProduct.Price*e.Quantity);
        }

        public IEnumerable<CartLine> Lines { get { return lineCollection; } } 
        public class CartProduct
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            // public string ImageName {get; set;}
            public int QuantityInStock { get; set; }
        }

        public class CartLine
        {
            public CartProduct CartProduct { get; set; }
            public int Quantity { get; set; }
            public int LinePrice { get; set; }
        }
    }
}