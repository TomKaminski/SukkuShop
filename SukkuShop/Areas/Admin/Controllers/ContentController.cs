using System.IO;
using System.Web;
using System.Web.Mvc;
using SukkuShop.Areas.Admin.Models;

namespace SukkuShop.Areas.Admin.Controllers
{
    public partial class ContentController : Controller
    {
        // GET: Admin/Content
        [HttpGet]
        public virtual ActionResult Index()
        {
            ViewBag.SelectedOpt = 5;
            return View();
        }

        
        [HttpGet]
        public virtual ActionResult Regulamin()
        {
            ViewBag.SelectedOpt = 5;
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/regulamin.html");
            var text = new MvcHtmlString("");
            if(System.IO.File.Exists(path))
                text = new MvcHtmlString(System.IO.File.ReadAllText(path));
            var model = new RegulaminModel
            {
                Text = text
            };
            return View(model);
        }

        public virtual PartialViewResult RegulaminAjaxGet()
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/regulamin.html");
            var text = new MvcHtmlString("");
            if (System.IO.File.Exists(path))
                text = new MvcHtmlString(System.IO.File.ReadAllText(path));
            var model = new RegulaminModel
            {
                Text = text
            };
            return PartialView("_RegulaminPost",model);
        }

        public virtual PartialViewResult RegulaminAjaxPost()
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/regulamin.html");
            var text = new MvcHtmlString("");
            if (System.IO.File.Exists(path))
                text = new MvcHtmlString(System.IO.File.ReadAllText(path));
            var model = new RegulaminModel
            {
                Text = text
            };
            return PartialView("_RegulaminGet", model);
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult Regulamin(string text)
        {
            ViewBag.SelectedOpt = 5;
            var pathForSaving = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/regulamin.html");
            System.IO.File.WriteAllText(pathForSaving,text);
            return RedirectToAction(MVC.Admin.Content.Regulamin());
        }

        [HttpGet]
        public virtual ActionResult PolitykaPrywatnosci()
        {
            ViewBag.SelectedOpt = 5;
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/politykaprywatnosci.html");
            var text = new MvcHtmlString("");
            if (System.IO.File.Exists(path))
                text = new MvcHtmlString(System.IO.File.ReadAllText(path));
            var model = new RegulaminModel
            {
                Text = text
            };
            return View(model);
        }

        public virtual PartialViewResult PolitykaPrywatnosciAjaxGet()
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/politykaprywatnosci.html");
            var text = new MvcHtmlString("");
            if (System.IO.File.Exists(path))
                text = new MvcHtmlString(System.IO.File.ReadAllText(path));
            var model = new RegulaminModel
            {
                Text = text
            };
            return PartialView("_PolitykaPost", model);
        }

        public virtual PartialViewResult PolitykaPrywatnosciAjaxPost()
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/politykaprywatnosci.html");
            var text = new MvcHtmlString("");
            if (System.IO.File.Exists(path))
                text = new MvcHtmlString(System.IO.File.ReadAllText(path));
            var model = new RegulaminModel
            {
                Text = text
            };
            return PartialView("_PolitykaGet", model);
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult PolitykaPrywatnosci(string text)
        {
            ViewBag.SelectedOpt = 5;
            var pathForSaving = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/politykaprywatnosci.html");
            System.IO.File.WriteAllText(pathForSaving, text);
            return RedirectToAction(MVC.Admin.Content.PolitykaPrywatnosci());
        }

        [HttpGet]
        public virtual ActionResult Gwarancja()
        {
            ViewBag.SelectedOpt = 5;
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/gwarancja.html");
            var text = new MvcHtmlString("");
            if (System.IO.File.Exists(path))
                text = new MvcHtmlString(System.IO.File.ReadAllText(path));
            var model = new RegulaminModel
            {
                Text = text
            };
            return View(model);
        }

        public virtual PartialViewResult GwarancjaAjaxGet()
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/gwarancja.html");
            var text = new MvcHtmlString("");
            if (System.IO.File.Exists(path))
                text = new MvcHtmlString(System.IO.File.ReadAllText(path));
            var model = new RegulaminModel
            {
                Text = text
            };
            return PartialView("_GwarancjaPost", model);
        }

        public virtual PartialViewResult GwarancjaAjaxPost()
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/gwarancja.html");
            var text = new MvcHtmlString("");
            if (System.IO.File.Exists(path))
                text = new MvcHtmlString(System.IO.File.ReadAllText(path));
            var model = new RegulaminModel
            {
                Text = text
            };
            return PartialView("_GwarancjaGet", model);
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult Gwarancja(string text)
        {
            ViewBag.SelectedOpt = 5;
            var pathForSaving = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/gwarancja.html");
            System.IO.File.WriteAllText(pathForSaving, text);
            return RedirectToAction(MVC.Admin.Content.Gwarancja());
        }

        [HttpGet]
        public virtual ActionResult Reklamacje()
        {
            ViewBag.SelectedOpt = 5;
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/reklamacje.html");
            var text = new MvcHtmlString("");
            if (System.IO.File.Exists(path))
                text = new MvcHtmlString(System.IO.File.ReadAllText(path));
            var model = new RegulaminModel
            {
                Text = text
            };
            return View(model);
        }

        public virtual PartialViewResult ReklamacjeAjaxGet()
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/reklamacje.html");
            var text = new MvcHtmlString("");
            if (System.IO.File.Exists(path))
                text = new MvcHtmlString(System.IO.File.ReadAllText(path));
            var model = new RegulaminModel
            {
                Text = text
            };
            return PartialView("_ReklamacjePost", model);
        }

        public virtual PartialViewResult ReklamacjeAjaxPost()
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/reklamacje.html");
            var text = new MvcHtmlString("");
            if (System.IO.File.Exists(path))
                text = new MvcHtmlString(System.IO.File.ReadAllText(path));
            var model = new RegulaminModel
            {
                Text = text
            };
            return PartialView("_ReklamacjeGet", model);
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult Reklamacje(string text)
        {
            ViewBag.SelectedOpt = 5;
            var pathForSaving=Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/reklamacje.html");
            System.IO.File.WriteAllText(pathForSaving, text);
            return RedirectToAction(MVC.Admin.Content.Reklamacje());
        }

        [HttpGet]
        public virtual ActionResult ZwrotTowarow()
        {
            ViewBag.SelectedOpt = 5;
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/zwrottowarow.html");
            var text = new MvcHtmlString("");
            if (System.IO.File.Exists(path))
                text = new MvcHtmlString(System.IO.File.ReadAllText(path));
            var model = new RegulaminModel
            {
                Text = text
            };
            return View(model);
        }

        public virtual PartialViewResult ZwrotTowarowAjaxGet()
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/zwrottowarow.html");
            var text = new MvcHtmlString("");
            if (System.IO.File.Exists(path))
                text = new MvcHtmlString(System.IO.File.ReadAllText(path));
            var model = new RegulaminModel
            {
                Text = text
            };
            return PartialView("_ZwrotTowarowPost", model);
        }

        public virtual PartialViewResult ZwrotTowarowAjaxPost()
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/zwrottowarow.html");
            var text = new MvcHtmlString("");
            if (System.IO.File.Exists(path))
                text = new MvcHtmlString(System.IO.File.ReadAllText(path));
            var model = new RegulaminModel
            {
                Text = text
            };
            return PartialView("_ZwrotTowarowGet", model);
        }

        [HttpPost, ValidateInput(false)]
        public virtual ActionResult ZwrotTowarow(string text)
        {
            ViewBag.SelectedOpt = 5;
            var pathForSaving = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/web/zwrottowarow.html");
            System.IO.File.WriteAllText(pathForSaving, text);
            return RedirectToAction(MVC.Admin.Content.ZwrotTowarow());
        }
    }
}