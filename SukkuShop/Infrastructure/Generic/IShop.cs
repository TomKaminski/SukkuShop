using System.Linq;
using SukkuShop.Models;

namespace SukkuShop.Infrastructure.Generic
{
    public interface IShop
    {
        IQueryable<Products> Products { get; set; }

        IQueryable<Products> SortProducts(IQueryable<Products> products, SortMethod method);

    }
}
