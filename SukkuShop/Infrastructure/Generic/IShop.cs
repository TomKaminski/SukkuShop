using System.Collections.Generic;
using SukkuShop.Models;

namespace SukkuShop.Infrastructure.Generic
{
    public interface IShop
    {
        IList<ProductModel> Products { get; set; }

        IList<ProductModel> SortProducts(IList<ProductModel> products, SortMethod method);

    }
}
