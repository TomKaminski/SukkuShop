using System.Collections.Generic;
using SukkuShop.Models;

namespace SukkuShop.Infrastructure.Generic
{
    public interface IShop
    {
        IList<ProductModel> Products { get; set; }

        void SortProducts( SortMethod method);

        void Bestsellers();

        void NewProducts();



    }
}
