using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using SukkuShop.Models;

namespace SukkuShop.Infrastructure.Helpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            var result = new StringBuilder();
            for (var i = 1; i <= pagingInfo.TotalPages; i++)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
                if (i == pagingInfo.CurrentPage)
                {
                    //tag.Attributes.Add("style",);
                }
                tag.AddCssClass("");
                result.Append(tag);
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}