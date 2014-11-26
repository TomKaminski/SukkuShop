using System.Web.Mvc;

namespace SukkuShop.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Blad404()
        {
            ActionResult result;

            object model = Request.Url.PathAndQuery;

            if (!Request.IsAjaxRequest())
                result = View(model);
            else
                result = PartialView("Blad404", model);

            return result;
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}