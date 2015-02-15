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
    public partial class KontoController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected KontoController(Dummy d) { }

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

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> ResendActivationEmail()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ResendActivationEmail);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Zaloguj()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Zaloguj);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Aktywacja()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Aktywacja);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ResetujHaslo()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ResetujHaslo);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ZmianaEmaila()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ZmianaEmaila);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult ChangeEmailSuccess()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.ChangeEmailSuccess);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AnulujZamówienie()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AnulujZamówienie);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult SzczegółyZamówienia()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SzczegółyZamówienia);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public KontoController Actions { get { return MVC.Konto; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Konto";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Konto";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string ResendActivationEmail = "ResendActivationEmail";
            public readonly string Zaloguj = "Zaloguj";
            public readonly string Zarejestruj = "Zarejestruj";
            public readonly string Aktywacja = "Aktywacja";
            public readonly string ZapomnianeHaslo = "ZapomnianeHaslo";
            public readonly string ResetujHaslo = "ResetujHaslo";
            public readonly string Wyloguj = "Wyloguj";
            public readonly string DaneOsobowe = "DaneOsobowe";
            public readonly string ZmienHaslo = "ZmienHaslo";
            public readonly string ZmianaEmaila = "ZmianaEmaila";
            public readonly string ChangeEmailSuccess = "ChangeEmailSuccess";
            public readonly string ChangeUserInfoViewModel = "ChangeUserInfoViewModel";
            public readonly string ChangeUserFirmaInfoViewModel = "ChangeUserFirmaInfoViewModel";
            public readonly string HistoriaZamowien = "HistoriaZamowien";
            public readonly string AnulujZamówienie = "AnulujZamówienie";
            public readonly string SzczegółyZamówienia = "SzczegółyZamówienia";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string ResendActivationEmail = "ResendActivationEmail";
            public const string Zaloguj = "Zaloguj";
            public const string Zarejestruj = "Zarejestruj";
            public const string Aktywacja = "Aktywacja";
            public const string ZapomnianeHaslo = "ZapomnianeHaslo";
            public const string ResetujHaslo = "ResetujHaslo";
            public const string Wyloguj = "Wyloguj";
            public const string DaneOsobowe = "DaneOsobowe";
            public const string ZmienHaslo = "ZmienHaslo";
            public const string ZmianaEmaila = "ZmianaEmaila";
            public const string ChangeEmailSuccess = "ChangeEmailSuccess";
            public const string ChangeUserInfoViewModel = "ChangeUserInfoViewModel";
            public const string ChangeUserFirmaInfoViewModel = "ChangeUserFirmaInfoViewModel";
            public const string HistoriaZamowien = "HistoriaZamowien";
            public const string AnulujZamówienie = "AnulujZamówienie";
            public const string SzczegółyZamówienia = "SzczegółyZamówienia";
        }


        static readonly ActionParamsClass_ResendActivationEmail s_params_ResendActivationEmail = new ActionParamsClass_ResendActivationEmail();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ResendActivationEmail ResendActivationEmailParams { get { return s_params_ResendActivationEmail; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ResendActivationEmail
        {
            public readonly string email = "email";
        }
        static readonly ActionParamsClass_Zaloguj s_params_Zaloguj = new ActionParamsClass_Zaloguj();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Zaloguj ZalogujParams { get { return s_params_Zaloguj; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Zaloguj
        {
            public readonly string returnUrl = "returnUrl";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Zarejestruj s_params_Zarejestruj = new ActionParamsClass_Zarejestruj();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Zarejestruj ZarejestrujParams { get { return s_params_Zarejestruj; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Zarejestruj
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Aktywacja s_params_Aktywacja = new ActionParamsClass_Aktywacja();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Aktywacja AktywacjaParams { get { return s_params_Aktywacja; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Aktywacja
        {
            public readonly string userId = "userId";
            public readonly string code = "code";
        }
        static readonly ActionParamsClass_ZapomnianeHaslo s_params_ZapomnianeHaslo = new ActionParamsClass_ZapomnianeHaslo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ZapomnianeHaslo ZapomnianeHasloParams { get { return s_params_ZapomnianeHaslo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ZapomnianeHaslo
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ResetujHaslo s_params_ResetujHaslo = new ActionParamsClass_ResetujHaslo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ResetujHaslo ResetujHasloParams { get { return s_params_ResetujHaslo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ResetujHaslo
        {
            public readonly string code = "code";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ZmienHaslo s_params_ZmienHaslo = new ActionParamsClass_ZmienHaslo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ZmienHaslo ZmienHasloParams { get { return s_params_ZmienHaslo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ZmienHaslo
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ZmianaEmaila s_params_ZmianaEmaila = new ActionParamsClass_ZmianaEmaila();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ZmianaEmaila ZmianaEmailaParams { get { return s_params_ZmianaEmaila; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ZmianaEmaila
        {
            public readonly string userId = "userId";
            public readonly string code = "code";
            public readonly string newEmail = "newEmail";
        }
        static readonly ActionParamsClass_ChangeEmailSuccess s_params_ChangeEmailSuccess = new ActionParamsClass_ChangeEmailSuccess();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ChangeEmailSuccess ChangeEmailSuccessParams { get { return s_params_ChangeEmailSuccess; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ChangeEmailSuccess
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ChangeUserInfoViewModel s_params_ChangeUserInfoViewModel = new ActionParamsClass_ChangeUserInfoViewModel();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ChangeUserInfoViewModel ChangeUserInfoViewModelParams { get { return s_params_ChangeUserInfoViewModel; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ChangeUserInfoViewModel
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ChangeUserFirmaInfoViewModel s_params_ChangeUserFirmaInfoViewModel = new ActionParamsClass_ChangeUserFirmaInfoViewModel();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ChangeUserFirmaInfoViewModel ChangeUserFirmaInfoViewModelParams { get { return s_params_ChangeUserFirmaInfoViewModel; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ChangeUserFirmaInfoViewModel
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_AnulujZamówienie s_params_AnulujZamówienie = new ActionParamsClass_AnulujZamówienie();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AnulujZamówienie AnulujZamówienieParams { get { return s_params_AnulujZamówienie; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AnulujZamówienie
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_SzczegółyZamówienia s_params_SzczegółyZamówienia = new ActionParamsClass_SzczegółyZamówienia();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SzczegółyZamówienia SzczegółyZamówieniaParams { get { return s_params_SzczegółyZamówienia; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SzczegółyZamówienia
        {
            public readonly string id = "id";
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
                public readonly string _ChangePassword = "_ChangePassword";
                public readonly string _ChangeUserFirmaInfoViewModel = "_ChangeUserFirmaInfoViewModel";
                public readonly string _ChangeUserInfoViewModel = "_ChangeUserInfoViewModel";
                public readonly string _ResetujHasloPartial = "_ResetujHasloPartial";
                public readonly string _ZapomnianeHasloPartial = "_ZapomnianeHasloPartial";
                public readonly string ChangeEmailSuccess = "ChangeEmailSuccess";
                public readonly string HistoriaZamowien = "HistoriaZamowien";
                public readonly string Index = "Index";
                public readonly string KontoAktywne = "KontoAktywne";
                public readonly string KontoNieaktywne = "KontoNieaktywne";
                public readonly string RegisterSuccess = "RegisterSuccess";
                public readonly string ResetujHaslo = "ResetujHaslo";
                public readonly string SzczegółyZamówienia = "SzczegółyZamówienia";
                public readonly string Zaloguj = "Zaloguj";
                public readonly string ZapomnianeHaslo = "ZapomnianeHaslo";
                public readonly string ZapomnianeHasloPotwierdzenie = "ZapomnianeHasloPotwierdzenie";
                public readonly string Zarejestruj = "Zarejestruj";
            }
            public readonly string _ChangePassword = "~/Views/Konto/_ChangePassword.cshtml";
            public readonly string _ChangeUserFirmaInfoViewModel = "~/Views/Konto/_ChangeUserFirmaInfoViewModel.cshtml";
            public readonly string _ChangeUserInfoViewModel = "~/Views/Konto/_ChangeUserInfoViewModel.cshtml";
            public readonly string _ResetujHasloPartial = "~/Views/Konto/_ResetujHasloPartial.cshtml";
            public readonly string _ZapomnianeHasloPartial = "~/Views/Konto/_ZapomnianeHasloPartial.cshtml";
            public readonly string ChangeEmailSuccess = "~/Views/Konto/ChangeEmailSuccess.cshtml";
            public readonly string HistoriaZamowien = "~/Views/Konto/HistoriaZamowien.cshtml";
            public readonly string Index = "~/Views/Konto/Index.cshtml";
            public readonly string KontoAktywne = "~/Views/Konto/KontoAktywne.cshtml";
            public readonly string KontoNieaktywne = "~/Views/Konto/KontoNieaktywne.cshtml";
            public readonly string RegisterSuccess = "~/Views/Konto/RegisterSuccess.cshtml";
            public readonly string ResetujHaslo = "~/Views/Konto/ResetujHaslo.cshtml";
            public readonly string SzczegółyZamówienia = "~/Views/Konto/SzczegółyZamówienia.cshtml";
            public readonly string Zaloguj = "~/Views/Konto/Zaloguj.cshtml";
            public readonly string ZapomnianeHaslo = "~/Views/Konto/ZapomnianeHaslo.cshtml";
            public readonly string ZapomnianeHasloPotwierdzenie = "~/Views/Konto/ZapomnianeHasloPotwierdzenie.cshtml";
            public readonly string Zarejestruj = "~/Views/Konto/Zarejestruj.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_KontoController : SukkuShop.Controllers.KontoController
    {
        public T4MVC_KontoController() : base(Dummy.Instance) { }

        [NonAction]
        partial void ResendActivationEmailOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string email);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> ResendActivationEmail(string email)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ResendActivationEmail);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "email", email);
            ResendActivationEmailOverride(callInfo, email);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void ZalogujOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string returnUrl);

        [NonAction]
        public override System.Web.Mvc.ActionResult Zaloguj(string returnUrl)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Zaloguj);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "returnUrl", returnUrl);
            ZalogujOverride(callInfo, returnUrl);
            return callInfo;
        }

        [NonAction]
        partial void ZalogujOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, SukkuShop.Models.LoginViewModel model, string returnUrl);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Zaloguj(SukkuShop.Models.LoginViewModel model, string returnUrl)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Zaloguj);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "returnUrl", returnUrl);
            ZalogujOverride(callInfo, model, returnUrl);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void ZarejestrujOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Zarejestruj()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Zarejestruj);
            ZarejestrujOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ZarejestrujOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, SukkuShop.Models.RegisterViewModel model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Zarejestruj(SukkuShop.Models.RegisterViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Zarejestruj);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ZarejestrujOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void AktywacjaOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int userId, string code);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Aktywacja(int userId, string code)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Aktywacja);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "userId", userId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "code", code);
            AktywacjaOverride(callInfo, userId, code);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void ZapomnianeHasloOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ZapomnianeHaslo()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ZapomnianeHaslo);
            ZapomnianeHasloOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ZapomnianeHasloOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, SukkuShop.Models.ForgotPasswordViewModel model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> ZapomnianeHaslo(SukkuShop.Models.ForgotPasswordViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ZapomnianeHaslo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ZapomnianeHasloOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void ResetujHasloOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string code);

        [NonAction]
        public override System.Web.Mvc.ActionResult ResetujHaslo(string code)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ResetujHaslo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "code", code);
            ResetujHasloOverride(callInfo, code);
            return callInfo;
        }

        [NonAction]
        partial void ResetujHasloOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, SukkuShop.Models.ResetPasswordViewModel model);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> ResetujHaslo(SukkuShop.Models.ResetPasswordViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ResetujHaslo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ResetujHasloOverride(callInfo, model);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void WylogujOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Wyloguj()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Wyloguj);
            WylogujOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void DaneOsoboweOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult DaneOsobowe()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DaneOsobowe);
            DaneOsoboweOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ZmienHasloOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ZmienHaslo()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ZmienHaslo);
            ZmienHasloOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ZmienHasloOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, SukkuShop.Models.ChangePasswordViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult ZmienHaslo(SukkuShop.Models.ChangePasswordViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ZmienHaslo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ZmienHasloOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void ZmianaEmailaOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int userId, string code, string newEmail);

        [NonAction]
        public override System.Web.Mvc.ActionResult ZmianaEmaila(int userId, string code, string newEmail)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ZmianaEmaila);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "userId", userId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "code", code);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "newEmail", newEmail);
            ZmianaEmailaOverride(callInfo, userId, code, newEmail);
            return callInfo;
        }

        [NonAction]
        partial void ChangeEmailSuccessOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, string model);

        [NonAction]
        public override System.Web.Mvc.ViewResult ChangeEmailSuccess(string model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.ChangeEmailSuccess);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ChangeEmailSuccessOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void ChangeUserInfoViewModelOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ChangeUserInfoViewModel()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ChangeUserInfoViewModel);
            ChangeUserInfoViewModelOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ChangeUserInfoViewModelOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, SukkuShop.Models.ChangeUserInfoViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult ChangeUserInfoViewModel(SukkuShop.Models.ChangeUserInfoViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ChangeUserInfoViewModel);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ChangeUserInfoViewModelOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void ChangeUserFirmaInfoViewModelOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ChangeUserFirmaInfoViewModel()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ChangeUserFirmaInfoViewModel);
            ChangeUserFirmaInfoViewModelOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ChangeUserFirmaInfoViewModelOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, SukkuShop.Models.ChangeUserFirmaInfoViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult ChangeUserFirmaInfoViewModel(SukkuShop.Models.ChangeUserFirmaInfoViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ChangeUserFirmaInfoViewModel);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ChangeUserFirmaInfoViewModelOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void HistoriaZamowienOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult HistoriaZamowien()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.HistoriaZamowien);
            HistoriaZamowienOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void AnulujZamówienieOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult AnulujZamówienie(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AnulujZamówienie);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            AnulujZamówienieOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void SzczegółyZamówieniaOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult SzczegółyZamówienia(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SzczegółyZamówienia);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            SzczegółyZamówieniaOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108
