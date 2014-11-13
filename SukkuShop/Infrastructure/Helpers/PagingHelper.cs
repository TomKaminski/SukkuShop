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
            if (pagingInfo.TotalPages <= 1) return MvcHtmlString.Create(result.ToString());
            if (pagingInfo.CurrentPage != 1)
                TagCreator(pagingInfo, pageUrl, pagingInfo.CurrentPage - 1, result, false, true, false);

            for (var i = 1; i <= pagingInfo.TotalPages; i++)
            {
                if (i == 1 || i == pagingInfo.TotalPages)
                    TagCreator(pagingInfo, pageUrl, i, result, false, false, false);
                if (i == pagingInfo.CurrentPage &&
                    (pagingInfo.CurrentPage != 1 && pagingInfo.CurrentPage != pagingInfo.TotalPages))
                    TagCreator(pagingInfo, pageUrl, i, result, false, false, false);
                if ((i == pagingInfo.CurrentPage - 1 || i == pagingInfo.CurrentPage + 1) &&
                    (i != pagingInfo.TotalPages && i != 1))
                    TagCreator(pagingInfo, pageUrl, i, result, false, false, false);
                if ((i == pagingInfo.CurrentPage - 2 || i == pagingInfo.CurrentPage + 2) &&
                    (i != pagingInfo.TotalPages && i != 1))
                    TagCreator(pagingInfo, pageUrl, i, result, true, false, false);
            }

            if (pagingInfo.CurrentPage != pagingInfo.TotalPages)
                TagCreator(pagingInfo, pageUrl, pagingInfo.CurrentPage + 1, result, false, false, true);
            return MvcHtmlString.Create(result.ToString());
        }

        private static void TagCreator(PagingInfo pagingInfo, Func<int, string> pageUrl, int i, StringBuilder result,bool dots,bool leftarrow,bool rightarrow)
        {
            var tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl(i));
            if (leftarrow)
                tag.InnerHtml = "<div class="+"paging-arrow-left"+"></div>";
            else if (rightarrow)
                tag.InnerHtml = "<div class=" + "paging-arrow-right" + "></div>";
            else if (dots)
                tag.InnerHtml = "<div class=" + "paginator-box" + ">" + "<div class=" + "square-paginator" + ">" + "</div>" + "<div class=" + "square-paginator" + ">" + "</div>" + "<div class=" + "square-paginator" + ">" + "</div>"+"<div class="+"clear"+"></div>" + "</div>";
            else
                tag.InnerHtml = "<div class=" + "paginator-box" + " style=" +
                            (pagingInfo.CurrentPage == i ? "font-weight:bold;" : "") + ">" + i.ToString(CultureInfo.InvariantCulture) + "</div>";
            if(!(dots || rightarrow || leftarrow))
                result.Append("<li class="+"inner-paginator" + ">");
            else
                result.Append("<li>");
            result.Append(tag);
            result.Append("</li>");
        }
    }
}

            