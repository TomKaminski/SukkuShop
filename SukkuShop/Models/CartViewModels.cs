using System.Collections.Generic;

namespace SukkuShop.Models
{
    public class CartViewModels
    {
        public List<CartProduct> CartProductsList {get; set;}
        public string TotalValue { get; set; }
    }

    public class CartProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
        public string TotalValue { get; set; }
        public string Image { get; set; }
    }
}