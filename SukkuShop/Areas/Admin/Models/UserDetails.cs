using System.Collections.Generic;
using SukkuShop.Models;

namespace SukkuShop.Areas.Admin.Models
{
    public class UserDetailsModel:SharedAddressModels
    {
        public string NameTitle { get; set; }
        public string NipUsername { get; set; }
        public int Rabat { get; set; }
        public List<AccountOrderItemViewModel> AccountOrderItemViewModel { get; set; }
    }
}