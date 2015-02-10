using System.Collections.Generic;
using SukkuShop.Models;

namespace SukkuShop.Areas.Admin.Models
{
    public class UserDetailsModel:SharedAddressModels
    {
        public string NameTitle { get; set; }
        public string NipUsername { get; set; }
        public int Rabat { get; set; }
        public int OrdersCount { get; set; }
        public bool KontoFirmowe { get; set; }
        public int Id { get; set; }
        public List<AccountOrderItemViewModel> AccountOrderItemViewModel { get; set; }
    }
}