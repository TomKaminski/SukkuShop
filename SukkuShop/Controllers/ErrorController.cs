using System.Web.Mvc;

namespace SukkuShop.Controllers
{
    public partial class ErrorController : Controller
    {
        public virtual ActionResult Blad404()
        {
            ActionResult result;

            object model = Request.Url.PathAndQuery;

            if (!Request.IsAjaxRequest())
                result = View(model);
            else
                result = PartialView("Blad404", model);

            return result;
        }

        public virtual ActionResult Error()
        {
            return View();
        }
    }
}