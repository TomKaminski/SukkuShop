using System.Data.Entity;
using SukkuShop.Models;

namespace SukkuShop.Infrastructure.Generic
{
    public interface IAppRepository
    {
        DbSet<Products> Products { get; set; }
        DbSet<Categories> Categories { get; set; }
        DbSet<OrderDetails> OrderDetails { get; set; }
        DbSet<Orders> Orders { get; set; }
    }
}
