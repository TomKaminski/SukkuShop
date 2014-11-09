using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using Microsoft.Practices.ObjectBuilder2;
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
                    if(i==1 || i==pagingInfo.TotalPages)
                        TagCreator(pagingInfo, pageUrl, i, result,false);
                    if(i==pagingInfo.CurrentPage && (pagingInfo.CurrentPage!=1 && pagingInfo.CurrentPage!=pagingInfo.TotalPages))
                        TagCreator(pagingInfo, pageUrl, i, result,false);
                    if((i== pagingInfo.CurrentPage-1 || i== pagingInfo.CurrentPage+1) && (i!=pagingInfo.TotalPages && i!=1))
                        TagCreator(pagingInfo, pageUrl, i, result,false);
                    if ((i == pagingInfo.CurrentPage - 2 || i == pagingInfo.CurrentPage + 2) && (i != pagingInfo.TotalPages && i != 1))
                        TagCreator(pagingInfo, pageUrl, i, result,true);
                }
            return MvcHtmlString.Create(result.ToString());
        }

        private static void TagCreator(PagingInfo pagingInfo, Func<int, string> pageUrl, int i, StringBuilder result,bool dots)
        {
            var tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl(i));
            tag.Attributes.Add("style","width:21px;height:21px;");
            if(dots)
                tag.InnerHtml = "<div class=" + "paginator-box" + ">" + "<div class=" + "square-paginator" + ">" + "</div>" + "<div class=" + "square-paginator" + ">" + "</div>" + "<div class=" + "square-paginator" + ">" + "</div>"+"<div class="+"clear"+">&nbsp;</div>" + "</div>";
            else
            {
                tag.InnerHtml = "<div class=" + "paginator-box" + " style=" +
                            (pagingInfo.CurrentPage == i ? "font-weight:bold;" : "") + ">" + i.ToString(CultureInfo.InvariantCulture) + "</div>";
            }
            result.Append("<li class=" + (dots == false ?
                            "inner-paginator" : "") + ">");
            result.Append(tag);
            result.Append("</li>");
        }
    }
}

            