using System;
using System.Collections.Generic;
using System.Linq;
using SukkuShop.Infrastructure.Generic;

namespace SukkuShop.Models
{
    public class Shop:IShop
    {
        public IList<ProductModel> Products { get; set; }

        public void SortProducts(SortMethod method)
        {
            switch (method)
            {
                case SortMethod.Popularność:
                    Products = Products.OrderByDescending(o => o.OrdersCount).ToList();
                    break;
                case SortMethod.Promocja:
                    Products = Products.OrderByDescending(o => o.Promotion).ToList();
                    break;
                case SortMethod.CenaMalejaco:
                    Products = Products.OrderByDescending(o => o.PriceAfterDiscount).ToList();
                    break;
                case SortMethod.CenaRosnaco:
                    Products = Products.OrderBy(o => o.PriceAfterDiscount).ToList();
                    break;
                default:
                    Products = Products.OrderByDescending(o => o.DateAdded).ToList();
                    break;
            }
        }

        public void Bestsellers()
        {
            foreach (var item in Products.Where(item => (DateTime.Now - item.DateAdded).Days < 14))
                item.Novelty = true;
        }

        public void NewProducts()
        {
            var bestsellerCounter = Math.Ceiling(Products.Count * 0.1);
            var i = 0;
            Products = Products.OrderByDescending(x => x.OrdersCount).ToList();
            foreach (var item in Products.TakeWhile(item => i != bestsellerCounter))
            {
                item.Bestseller = true;
                i++;
            }
        }
    }
}