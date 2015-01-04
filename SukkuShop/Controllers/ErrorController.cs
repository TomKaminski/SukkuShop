using System.Web.Mvc;

namespace SukkuShop.Controllers
{
    public partial class ErrorController : Controller
    {
        public virtual ActionResult Blad404()
        {
            ActionResult result;

            if (!Request.IsAjaxRequest())
                result = View();
            else
                result = PartialView("Blad404");

            return result;
        }

        public virtual ActionResult Error()
        {
            return View();
        }
    }
}