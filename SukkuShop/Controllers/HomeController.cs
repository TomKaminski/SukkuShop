using System.Web.Mvc;

namespace SukkuShop.Controllers
{

    public partial class HomeController : Controller
    {

        public virtual ViewResult Index()
        {
            return View();
        }


        public virtual ViewResult Regulamin()
        {
            return View();
        }

        public virtual ViewResult Dostawa()
        {
            return View();
        }


        public virtual ViewResult Platnosci()
        {
            return View();
        }

        public virtual ViewResult PolitykaPrywatnosci()
        {
            return View();
        }
    }
}