using System.Linq;
using SukkuShop.Infrastructure.Generic;

namespace SukkuShop.Models
{
    public class Shop:IShop
    {
        public IQueryable<Products> Products { get; set; }

        public IQueryable<Products> SortProducts(IQueryable<Products> listaProd, SortMethod method)
        {
            switch (method)
            {
                case SortMethod.Popularność:
                    listaProd = listaProd.OrderByDescending(o => o.OrdersCount);
                    break;
                case SortMethod.Promocje:
                    listaProd = listaProd.OrderByDescending(o => o.Promotion);
                    break;
                case SortMethod.CenaMalejąco:
                    listaProd = listaProd.OrderByDescending(o => o.Price);
                    break;
                case SortMethod.CenaRosnąco:
                    listaProd = listaProd.OrderBy(o => o.Price);
                    break;
                default:
                    listaProd = listaProd.OrderByDescending(o => o.DateAdded);
                    break;
            }
            return listaProd;
        }
    }
}