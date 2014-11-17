using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SukkuShop.Models
{
    public class CartViewModels
    {
        public List<CartProduct> CartProductsList {get; set;}
        public decimal TotalValue { get; set; }
    }

    public class CartProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
        public decimal TotalValue { get; set; }
    }
}