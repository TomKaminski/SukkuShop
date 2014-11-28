using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SukkuShop.Areas.Admin.Controllers
{
    public partial class AdminRoleController : Controller
    {
        // GET: Admin/AdminRole
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}