using System.Web.Mvc;

namespace SukkuShop.Areas.Admin.Models
{
    public class RegulaminModel
    {
        [AllowHtml]
        public MvcHtmlString Text { get; set; }
    }
}