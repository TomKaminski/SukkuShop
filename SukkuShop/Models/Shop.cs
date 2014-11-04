using System.Linq;
using SukkuShop.Infrastructure.Generic;

namespace SukkuShop.Models
{
    public class Shop:IShop
    {
        public IQueryable<Products> Products { get; set; }

        public IQueryable<Products> SortProducts(IQueryable<Products> listaProd, SortMethod method)
        {
            IQueryable<Products> products;
            switch (method)
            {
                case SortMethod.Popularność:
                    products = listaProd.OrderByDescending(o => o.OrdersCount);
                    break;
                case SortMethod.Promocje:
                    products = listaProd.OrderByDescending(o => o.Promotion);
                    break;
                case SortMethod.CenaMalejąco:
                    products = listaProd.OrderByDescending(o => o.Price);
                    break;
                case SortMethod.CenaRosnąco:
                    products = listaProd.OrderBy(o => o.Price);
                    break;
                default:
                    products = listaProd.OrderByDescending(o => o.DateAdded);
                    break;
            }
            return products;
        }
    }
}