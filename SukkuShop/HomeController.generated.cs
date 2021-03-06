// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
#pragma warning disable 1591, 3008, 3009, 0108
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace SukkuShop.Controllers
{
    public partial class HomeController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public HomeController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected HomeController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }


        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public HomeController Actions { get { return MVC.Home; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Home";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Home";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string LoginPartial = "LoginPartial";
            public readonly string Regulamin = "Regulamin";
            public readonly string Reklamacje = "Reklamacje";
            public readonly string Platnosci = "Platnosci";
            public readonly string PolitykaPrywatnosci = "PolitykaPrywatnosci";
            public readonly string Gwarancje = "Gwarancje";
            public readonly string ZwrotTowarow = "ZwrotTowarow";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string LoginPartial = "LoginPartial";
            public const string Regulamin = "Regulamin";
            public const string Reklamacje = "Reklamacje";
            public const string Platnosci = "Platnosci";
            public const string PolitykaPrywatnosci = "PolitykaPrywatnosci";
            public const string Gwarancje = "Gwarancje";
            public const string ZwrotTowarow = "ZwrotTowarow";
        }


        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string Gwarancje = "Gwarancje";
                public readonly string Index = "Index";
                public readonly string Platnosci = "Platnosci";
                public readonly string PolitykaPrywatnosci = "PolitykaPrywatnosci";
                public readonly string Regulamin = "Regulamin";
                public readonly string Reklamacje = "Reklamacje";
                public readonly string ZwrotTowarow = "ZwrotTowarow";
            }
            public readonly string Gwarancje = "~/Views/Home/Gwarancje.cshtml";
            public readonly string Index = "~/Views/Home/Index.cshtml";
            public readonly string Platnosci = "~/Views/Home/Platnosci.cshtml";
            public readonly string PolitykaPrywatnosci = "~/Views/Home/PolitykaPrywatnosci.cshtml";
            public readonly string Regulamin = "~/Views/Home/Regulamin.cshtml";
            public readonly string Reklamacje = "~/Views/Home/Reklamacje.cshtml";
            public readonly string ZwrotTowarow = "~/Views/Home/ZwrotTowarow.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_HomeController : SukkuShop.Controllers.HomeController
    {
        public T4MVC_HomeController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ViewResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void LoginPartialOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.PartialViewResult LoginPartial()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.LoginPartial);
            LoginPartialOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void RegulaminOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ViewResult Regulamin()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Regulamin);
            RegulaminOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ReklamacjeOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ViewResult Reklamacje()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Reklamacje);
            ReklamacjeOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void PlatnosciOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ViewResult Platnosci()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Platnosci);
            PlatnosciOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void PolitykaPrywatnosciOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ViewResult PolitykaPrywatnosci()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.PolitykaPrywatnosci);
            PolitykaPrywatnosciOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void GwarancjeOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ViewResult Gwarancje()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Gwarancje);
            GwarancjeOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ZwrotTowarowOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ViewResult ZwrotTowarow()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.ZwrotTowarow);
            ZwrotTowarowOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108
