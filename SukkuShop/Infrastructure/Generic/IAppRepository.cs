using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
