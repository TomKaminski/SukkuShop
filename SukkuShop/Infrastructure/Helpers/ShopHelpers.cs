using System.Text;
using System.Web;
using System.Web.Mvc;
using SukkuShop.Models;

namespace SukkuShop.Infrastructure.Helpers
{
    public static class ShopHelpers
    {

        public static MvcHtmlString BackgroundImage(this HtmlHelper html, string category=null)
        {
            var imgTag = new TagBuilder("img");
            var src = category != null ? VirtualPathUtility.ToAbsolute("~/Content/Images/Shop/" + category.ToLower() + ".jpg") : VirtualPathUtility.ToAbsolute("~/Content/Images/Shop/allproducts.png");
            imgTag.MergeAttribute("src",src);
            imgTag.Attributes.Add("style","z-index:-1000");
            return MvcHtmlString.Create(imgTag.ToString());
        }
        public static MvcHtmlString SortList(this HtmlHelper html, string pageUrl, SortMethod currentSortMethod, SortMethod sortMethod = SortMethod.Nowość)
        {
            var result = new StringBuilder();
            var linkTag = new TagBuilder("a");
            linkTag.MergeAttribute("href", pageUrl);
            var innerlinkTag = new TagBuilder("div");
            switch (sortMethod)
            {
                case SortMethod.CenaMalejaco:
                    innerlinkTag.AddCssClass("sort-arrow-down");
                    break;
                case SortMethod.CenaRosnaco:
                    innerlinkTag.AddCssClass("sort-arrow-up");
                    break;
                default:
                    innerlinkTag.AddCssClass("sort-arrow");
                    break;
            }
            if (sortMethod == currentSortMethod)
            {
                innerlinkTag.Attributes.Add("style", "opacity:1;");
            }
            innerlinkTag.AddCssClass("sortarrow");
            linkTag.InnerHtml = innerlinkTag.ToString();
            result.Append(linkTag);
            return MvcHtmlString.Create(result.ToString());
        }
        public static MvcHtmlString TopPanelBoxes(this HtmlHelper html, bool novelty, bool bestseller, int promotion)
        {
            var toppaneltext = new TagBuilder("div");
            toppaneltext.AddCssClass("top-panel-text");
            var toppanelcontainer = new TagBuilder("div");
            toppanelcontainer.AddCssClass("top-panel-text-container");
            var tag = new TagBuilder("li");

            var result = new StringBuilder();
            if (bestseller)
            {
                toppaneltext.SetInnerText("Bestseller");
                toppaneltext.Attributes.Remove("style");
                toppaneltext.Attributes.Add("style", "color: #875546");
                toppanelcontainer.InnerHtml = toppaneltext.ToString();
                tag.Attributes.Clear();
                tag.InnerHtml = toppanelcontainer.ToString();
                result.Append(tag);
            }
            if (novelty)
            {
                toppaneltext.SetInnerText("Nowość");
                toppaneltext.Attributes.Remove("style");
                toppaneltext.Attributes.Add("style", "color: #f36f21");
                toppanelcontainer.InnerHtml = toppaneltext.ToString();
                tag.Attributes.Clear();
                tag.InnerHtml = toppanelcontainer.ToString();
                result.Append(tag);
            }

            if (promotion <= 0) return MvcHtmlString.Create(result.ToString());

            toppaneltext.SetInnerText("Promocja");
            toppaneltext.Attributes.Remove("style");
            toppaneltext.Attributes.Add("style", "color: green");
            toppanelcontainer.InnerHtml = toppaneltext.ToString();
            tag.Attributes.Clear();
            tag.InnerHtml = toppanelcontainer.ToString();
            result.Append(tag);
            return MvcHtmlString.Create(result.ToString());
        }
    }
}