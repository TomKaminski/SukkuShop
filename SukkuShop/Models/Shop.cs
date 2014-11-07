using System.Collections.Generic;
using System.Linq;
using SukkuShop.Infrastructure.Generic;

namespace SukkuShop.Models
{
    public class Shop:IShop
    {
        public IList<ProductModel> Products { get; set; }

        public IList<ProductModel> SortProducts(IList<ProductModel> listaProd, SortMethod method)
        {
            IList<ProductModel> products;
            switch (method)
            {
                case SortMethod.Popularność:
                    products = listaProd.OrderByDescending(o => o.OrdersCount).ToList();
                    break;
                case SortMethod.Promocja:
                    products = listaProd.OrderByDescending(o => o.Promotion).ToList();
                    break;
                case SortMethod.CenaMalejaco:
                    products = listaProd.OrderByDescending(o => o.Price).ToList();
                    break;
                case SortMethod.CenaRosnaco:
                    products = listaProd.OrderBy(o => o.Price).ToList();
                    break;
                default:
                    products = listaProd.OrderByDescending(o => o.DateAdded).ToList();
                    break;
            }
            return products;
        }
    }
}