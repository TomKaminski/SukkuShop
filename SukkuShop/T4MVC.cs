﻿// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
#pragma warning disable 1591, 3008, 3009
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

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
public static partial class MVC
{
    static readonly AdminClass s_Admin = new AdminClass();
    public static AdminClass Admin { get { return s_Admin; } }
    public static SukkuShop.Controllers.ErrorController Error = new SukkuShop.Controllers.T4MVC_ErrorController();
    public static SukkuShop.Controllers.HomeController Home = new SukkuShop.Controllers.T4MVC_HomeController();
    public static SukkuShop.Controllers.KontoController Konto = new SukkuShop.Controllers.T4MVC_KontoController();
    public static SukkuShop.Controllers.KoszykController Koszyk = new SukkuShop.Controllers.T4MVC_KoszykController();
    public static SukkuShop.Controllers.NavController Nav = new SukkuShop.Controllers.T4MVC_NavController();
    public static SukkuShop.Controllers.PreviewController Preview = new SukkuShop.Controllers.T4MVC_PreviewController();
    public static SukkuShop.Controllers.SklepController Sklep = new SukkuShop.Controllers.T4MVC_SklepController();
    public static SukkuShop.Controllers.ZamowienieController Zamowienie = new SukkuShop.Controllers.T4MVC_ZamowienieController();
    public static T4MVC.EmailsController Emails = new T4MVC.EmailsController();
    public static T4MVC.SharedController Shared = new T4MVC.SharedController();
}

namespace T4MVC
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class AdminClass
    {
        public readonly string Name = "Admin";
        public SukkuShop.Areas.Admin.Controllers.AdminHomeController AdminHome = new SukkuShop.Areas.Admin.Controllers.T4MVC_AdminHomeController();
        public SukkuShop.Areas.Admin.Controllers.AdminProductController AdminProduct = new SukkuShop.Areas.Admin.Controllers.T4MVC_AdminProductController();
        public SukkuShop.Areas.Admin.Controllers.AdminRoleController AdminRole = new SukkuShop.Areas.Admin.Controllers.T4MVC_AdminRoleController();
        public SukkuShop.Areas.Admin.Controllers.AdminUserController AdminUser = new SukkuShop.Areas.Admin.Controllers.T4MVC_AdminUserController();
        public T4MVC.Admin.SharedController Shared = new T4MVC.Admin.SharedController();
    }
}

namespace T4MVC
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class Dummy
    {
        private Dummy() { }
        public static Dummy Instance = new Dummy();
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_ActionResult : System.Web.Mvc.ActionResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_ActionResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
     
    public override void ExecuteResult(System.Web.Mvc.ControllerContext context) { }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}
[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_ViewResult : System.Web.Mvc.ViewResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_ViewResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}
[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_PartialViewResult : System.Web.Mvc.PartialViewResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_PartialViewResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}
[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal partial class T4MVC_System_Web_Mvc_JsonResult : System.Web.Mvc.JsonResult, IT4MVCActionResult
{
    public T4MVC_System_Web_Mvc_JsonResult(string area, string controller, string action, string protocol = null): base()
    {
        this.InitMVCT4Result(area, controller, action, protocol);
    }
    
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Protocol { get; set; }
    public RouteValueDictionary RouteValueDictionary { get; set; }
}



namespace Links
{
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Scripts {
        private const string URLPATH = "~/Scripts";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        public static readonly string _references_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/_references.min.js") ? Url("_references.min.js") : Url("_references.js");
        public static readonly string AccSzczegZam_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/AccSzczegZam.min.js") ? Url("AccSzczegZam.min.js") : Url("AccSzczegZam.js");
        public static readonly string angular_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/angular.min.js") ? Url("angular.min.js") : Url("angular.js");
        public static readonly string angular_min_js = Url("angular.min.js");
        public static readonly string CartSummary_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/CartSummary.min.js") ? Url("CartSummary.min.js") : Url("CartSummary.js");
        public static readonly string jquery_2_1_1_intellisense_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-2.1.1.intellisense.min.js") ? Url("jquery-2.1.1.intellisense.min.js") : Url("jquery-2.1.1.intellisense.js");
        public static readonly string jquery_2_1_1_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-2.1.1.min.js") ? Url("jquery-2.1.1.min.js") : Url("jquery-2.1.1.js");
        public static readonly string jquery_2_1_1_min_js = Url("jquery-2.1.1.min.js");
        public static readonly string jquery_2_1_1_min_map = Url("jquery-2.1.1.min.map");
        public static readonly string jquery_ui_1_9_0_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery-ui-1.9.0.min.js") ? Url("jquery-ui-1.9.0.min.js") : Url("jquery-ui-1.9.0.js");
        public static readonly string jquery_ui_1_9_0_min_js = Url("jquery-ui-1.9.0.min.js");
        public static readonly string jquery_elevatezoom_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.elevatezoom.min.js") ? Url("jquery.elevatezoom.min.js") : Url("jquery.elevatezoom.js");
        public static readonly string jquery_unobtrusive_ajax_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.unobtrusive-ajax.min.js") ? Url("jquery.unobtrusive-ajax.min.js") : Url("jquery.unobtrusive-ajax.js");
        public static readonly string jquery_unobtrusive_ajax_min_js = Url("jquery.unobtrusive-ajax.min.js");
        public static readonly string jquery_validate_vsdoc_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.validate-vsdoc.min.js") ? Url("jquery.validate-vsdoc.min.js") : Url("jquery.validate-vsdoc.js");
        public static readonly string jquery_validate_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.validate.min.js") ? Url("jquery.validate.min.js") : Url("jquery.validate.js");
        public static readonly string jquery_validate_min_js = Url("jquery.validate.min.js");
        public static readonly string jquery_validate_unobtrusive_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/jquery.validate.unobtrusive.min.js") ? Url("jquery.validate.unobtrusive.min.js") : Url("jquery.validate.unobtrusive.js");
        public static readonly string jquery_validate_unobtrusive_min_js = Url("jquery.validate.unobtrusive.min.js");
        public static readonly string Konto_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Konto.min.js") ? Url("Konto.min.js") : Url("Konto.js");
        public static readonly string KontoNieaktywne_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/KontoNieaktywne.min.js") ? Url("KontoNieaktywne.min.js") : Url("KontoNieaktywne.js");
        public static readonly string Login_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Login.min.js") ? Url("Login.min.js") : Url("Login.js");
        public static readonly string main_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/main.min.js") ? Url("main.min.js") : Url("main.js");
        public static readonly string modernizr_2_8_3_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/modernizr-2.8.3.min.js") ? Url("modernizr-2.8.3.min.js") : Url("modernizr-2.8.3.js");
        public static readonly string OrderKrok1_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/OrderKrok1.min.js") ? Url("OrderKrok1.min.js") : Url("OrderKrok1.js");
        public static readonly string OrderKrok2_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/OrderKrok2.min.js") ? Url("OrderKrok2.min.js") : Url("OrderKrok2.js");
        public static readonly string podsumowanie_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/podsumowanie.min.js") ? Url("podsumowanie.min.js") : Url("podsumowanie.js");
        public static readonly string Register_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Register.min.js") ? Url("Register.min.js") : Url("Register.js");
        public static readonly string Resetuj_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Resetuj.min.js") ? Url("Resetuj.min.js") : Url("Resetuj.js");
        public static readonly string shopAngular_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/shopAngular.min.js") ? Url("shopAngular.min.js") : Url("shopAngular.js");
        public static readonly string shopdetails_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/shopdetails.min.js") ? Url("shopdetails.min.js") : Url("shopdetails.js");
        public static readonly string ZapomnianeHaslo_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/ZapomnianeHaslo.min.js") ? Url("ZapomnianeHaslo.min.js") : Url("ZapomnianeHaslo.js");
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static class Content {
        private const string URLPATH = "~/Content";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class css {
            private const string URLPATH = "~/Content/css";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class Error {
                private const string URLPATH = "~/Content/css/Error";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string Error_scss = Url("Error.scss");
                public static readonly string Error_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Error.min.css") ? Url("Error.min.css") : Url("Error.css");
                     
                public static readonly string Error_css_map = Url("Error.css.map");
                public static readonly string Error_min_css = Url("Error.min.css");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class Home {
                private const string URLPATH = "~/Content/css/Home";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string Home_scss = Url("Home.scss");
                public static readonly string Home_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Home.min.css") ? Url("Home.min.css") : Url("Home.css");
                     
                public static readonly string Home_css_map = Url("Home.css.map");
                public static readonly string Home_min_css = Url("Home.min.css");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class Konto {
                private const string URLPATH = "~/Content/css/Konto";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string Details_scss = Url("Details.scss");
                public static readonly string Details_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Details.min.css") ? Url("Details.min.css") : Url("Details.css");
                     
                public static readonly string Details_css_map = Url("Details.css.map");
                public static readonly string Details_min_css = Url("Details.min.css");
                public static readonly string KontoNieaktywne_scss = Url("KontoNieaktywne.scss");
                public static readonly string KontoNieaktywne_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/KontoNieaktywne.min.css") ? Url("KontoNieaktywne.min.css") : Url("KontoNieaktywne.css");
                     
                public static readonly string KontoNieaktywne_css_map = Url("KontoNieaktywne.css.map");
                public static readonly string KontoNieaktywne_min_css = Url("KontoNieaktywne.min.css");
                public static readonly string Login_scss = Url("Login.scss");
                public static readonly string Login_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Login.min.css") ? Url("Login.min.css") : Url("Login.css");
                     
                public static readonly string Login_css_map = Url("Login.css.map");
                public static readonly string Login_min_css = Url("Login.min.css");
                public static readonly string OrderHistory_scss = Url("OrderHistory.scss");
                public static readonly string OrderHistory_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/OrderHistory.min.css") ? Url("OrderHistory.min.css") : Url("OrderHistory.css");
                     
                public static readonly string OrderHistory_css_map = Url("OrderHistory.css.map");
                public static readonly string OrderHistory_min_css = Url("OrderHistory.min.css");
                public static readonly string OrderPreview_scss = Url("OrderPreview.scss");
                public static readonly string OrderPreview_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/OrderPreview.min.css") ? Url("OrderPreview.min.css") : Url("OrderPreview.css");
                     
                public static readonly string OrderPreview_css_map = Url("OrderPreview.css.map");
                public static readonly string OrderPreview_min_css = Url("OrderPreview.min.css");
                public static readonly string Register_scss = Url("Register.scss");
                public static readonly string Register_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Register.min.css") ? Url("Register.min.css") : Url("Register.css");
                     
                public static readonly string Register_css_map = Url("Register.css.map");
                public static readonly string Register_min_css = Url("Register.min.css");
                public static readonly string RegisterSuccess_scss = Url("RegisterSuccess.scss");
                public static readonly string RegisterSuccess_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/RegisterSuccess.min.css") ? Url("RegisterSuccess.min.css") : Url("RegisterSuccess.css");
                     
                public static readonly string RegisterSuccess_css_map = Url("RegisterSuccess.css.map");
                public static readonly string RegisterSuccess_min_css = Url("RegisterSuccess.min.css");
                public static readonly string Resetuj_scss = Url("Resetuj.scss");
                public static readonly string Resetuj_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Resetuj.min.css") ? Url("Resetuj.min.css") : Url("Resetuj.css");
                     
                public static readonly string Resetuj_css_map = Url("Resetuj.css.map");
                public static readonly string Resetuj_min_css = Url("Resetuj.min.css");
                public static readonly string ZapomnianeHaslo_scss = Url("ZapomnianeHaslo.scss");
                public static readonly string ZapomnianeHaslo_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/ZapomnianeHaslo.min.css") ? Url("ZapomnianeHaslo.min.css") : Url("ZapomnianeHaslo.css");
                     
                public static readonly string ZapomnianeHaslo_css_map = Url("ZapomnianeHaslo.css.map");
                public static readonly string ZapomnianeHaslo_min_css = Url("ZapomnianeHaslo.min.css");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class Koszyk {
                private const string URLPATH = "~/Content/css/Koszyk";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string Cart_scss = Url("Cart.scss");
                public static readonly string Cart_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Cart.min.css") ? Url("Cart.min.css") : Url("Cart.css");
                     
                public static readonly string Cart_css_map = Url("Cart.css.map");
                public static readonly string Cart_min_css = Url("Cart.min.css");
            }
        
            public static readonly string main_scss = Url("main.scss");
            public static readonly string main_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/main.min.css") ? Url("main.min.css") : Url("main.css");
                 
            public static readonly string main_css_map = Url("main.css.map");
            public static readonly string main_min_css = Url("main.min.css");
            public static readonly string reset_scss = Url("reset.scss");
            public static readonly string reset_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/reset.min.css") ? Url("reset.min.css") : Url("reset.css");
                 
            public static readonly string reset_css_map = Url("reset.css.map");
            public static readonly string reset_min_css = Url("reset.min.css");
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class Sklep {
                private const string URLPATH = "~/Content/css/Sklep";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string Shop_scss = Url("Shop.scss");
                public static readonly string Shop_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Shop.min.css") ? Url("Shop.min.css") : Url("Shop.css");
                     
                public static readonly string Shop_css_map = Url("Shop.css.map");
                public static readonly string Shop_min_css = Url("Shop.min.css");
                public static readonly string ShopDetails_scss = Url("ShopDetails.scss");
                public static readonly string ShopDetails_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/ShopDetails.min.css") ? Url("ShopDetails.min.css") : Url("ShopDetails.css");
                     
                public static readonly string ShopDetails_css_map = Url("ShopDetails.css.map");
                public static readonly string ShopDetails_min_css = Url("ShopDetails.min.css");
            }
        
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class Zamowienie {
                private const string URLPATH = "~/Content/css/Zamowienie";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string ClientData_scss = Url("ClientData.scss");
                public static readonly string ClientData_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/ClientData.min.css") ? Url("ClientData.min.css") : Url("ClientData.css");
                     
                public static readonly string ClientData_css_map = Url("ClientData.css.map");
                public static readonly string ClientData_min_css = Url("ClientData.min.css");
                public static readonly string Krok1_scss = Url("Krok1.scss");
                public static readonly string Krok1_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/Krok1.min.css") ? Url("Krok1.min.css") : Url("Krok1.css");
                     
                public static readonly string Krok1_css_map = Url("Krok1.css.map");
                public static readonly string Krok1_min_css = Url("Krok1.min.css");
                public static readonly string OrderSubmitted_scss = Url("OrderSubmitted.scss");
                public static readonly string OrderSubmitted_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/OrderSubmitted.min.css") ? Url("OrderSubmitted.min.css") : Url("OrderSubmitted.css");
                     
                public static readonly string OrderSubmitted_css_map = Url("OrderSubmitted.css.map");
                public static readonly string OrderSubmitted_min_css = Url("OrderSubmitted.min.css");
                public static readonly string podsumowanie_scss = Url("podsumowanie.scss");
                public static readonly string podsumowanie_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/podsumowanie.min.css") ? Url("podsumowanie.min.css") : Url("podsumowanie.css");
                     
                public static readonly string podsumowanie_css_map = Url("podsumowanie.css.map");
                public static readonly string podsumowanie_min_css = Url("podsumowanie.min.css");
            }
        
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class fonts {
            private const string URLPATH = "~/Content/fonts";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string FoglihtenPCS_otf = Url("FoglihtenPCS.otf");
            public static readonly string FTLTLT_TTF = Url("FTLTLT.TTF");
            public static readonly string lucida_bright_eot = Url("lucida_bright.eot");
            public static readonly string lucida_bright_svg = Url("lucida_bright.svg");
            public static readonly string lucida_bright_ttf = Url("lucida_bright.ttf");
            public static readonly string lucida_bright_woff = Url("lucida_bright.woff");
            public static readonly string lucida_console_eot = Url("lucida_console.eot");
            public static readonly string lucida_console_svg = Url("lucida_console.svg");
            public static readonly string lucida_console_ttf = Url("lucida_console.ttf");
            public static readonly string lucida_console_woff = Url("lucida_console.woff");
            public static readonly string phagspa_ttf = Url("phagspa.ttf");
            public static readonly string SegoeSemibold_ttf = Url("SegoeSemibold.ttf");
            public static readonly string SegoeSemiboldIE_eot = Url("SegoeSemiboldIE.eot");
            public static readonly string segoeui_ttf = Url("segoeui.ttf");
            public static readonly string segoeuibold_ttf = Url("segoeuibold.ttf");
            public static readonly string segoeuil_ttf = Url("segoeuil.ttf");
            public static readonly string segoeuisl_ttf = Url("segoeuisl.ttf");
            public static readonly string seguiblack_ttf = Url("seguiblack.ttf");
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class Images {
            private const string URLPATH = "~/Content/Images";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string checkbox_nonactive_png = Url("checkbox-nonactive.png");
            public static readonly string checkbox_png = Url("checkbox.png");
            public static readonly string cinnamon_sticks_jpg = Url("cinnamon-sticks.jpg");
            public static readonly string content_pic1_png = Url("content-pic1.png");
            public static readonly string content_pic2_png = Url("content-pic2.png");
            public static readonly string content_pic3_png = Url("content-pic3.png");
            public static readonly string footer_cart_png = Url("footer-cart.png");
            public static readonly string footer_sukku_png = Url("footer-sukku.png");
            public static readonly string footer_user_png = Url("footer-user.png");
            public static readonly string logo_header_png = Url("logo-header.png");
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class Panels {
                private const string URLPATH = "~/Content/Images/Panels";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string eye_png = Url("eye.png");
                public static readonly string left_jpg = Url("left.jpg");
                public static readonly string pepper_jpg = Url("pepper.jpg");
            }
        
            public static readonly string processing_gif = Url("processing.gif");
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class Shop {
                private const string URLPATH = "~/Content/Images/Shop";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string _1_jpg = Url("1.jpg");
                public static readonly string _1_png = Url("1.png");
                public static readonly string _2_jpg = Url("2.jpg");
                public static readonly string _2_png = Url("2.png");
                public static readonly string _4_jpg = Url("4.jpg");
                public static readonly string _4_png = Url("4.png");
                public static readonly string basket_png = Url("basket.png");
                public static readonly string carticon_png = Url("carticon.png");
                public static readonly string carticonwhite_png = Url("carticonwhite.png");
                public static readonly string info_icon_png = Url("info_icon.png");
                public static readonly string kosmetyki_jpg = Url("kosmetyki.jpg");
                public static readonly string lupa_png = Url("lupa.png");
            }
        
        }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static class web {
            private const string URLPATH = "~/Content/web";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            public static readonly string DaneKlientaKoszyk_html = Url("DaneKlientaKoszyk.html");
            public static readonly string OrderDetails_html = Url("OrderDetails.html");
            public static readonly string OrderHistory_html = Url("OrderHistory.html");
            public static readonly string OrderPreview_html = Url("OrderPreview.html");
            public static readonly string OrderSummary_html = Url("OrderSummary.html");
        }
    
    }


    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static partial class Areas {
        private const string URLPATH = "~/Areas";
        public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
        public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
    
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static partial class Admin {
            private const string URLPATH = "~/Areas/Admin";
            public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
            public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class Scripts {
                private const string URLPATH = "~/Areas/Admin/Scripts";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                public static readonly string AdminProduct_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/AdminProduct.min.js") ? Url("AdminProduct.min.js") : Url("AdminProduct.js");
                public static readonly string productListAngular_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/productListAngular.min.js") ? Url("productListAngular.min.js") : Url("productListAngular.js");
                public static readonly string userListAngular_js = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/userListAngular.min.js") ? Url("userListAngular.min.js") : Url("userListAngular.js");
            }
        
        }
    }

    public static partial class Areas {
    
        public static partial class Admin {
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public static class Content {
                private const string URLPATH = "~/Areas/Admin/Content";
                public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
                public static class css {
                    private const string URLPATH = "~/Areas/Admin/Content/css";
                    public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                    public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                    public static readonly string AdminProductsList_scss = Url("AdminProductsList.scss");
                    public static readonly string AdminProductsList_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/AdminProductsList.min.css") ? Url("AdminProductsList.min.css") : Url("AdminProductsList.css");
                         
                    public static readonly string AdminProductsList_css_map = Url("AdminProductsList.css.map");
                    public static readonly string AdminProductsList_min_css = Url("AdminProductsList.min.css");
                    public static readonly string CreateProduct_scss = Url("CreateProduct.scss");
                    public static readonly string CreateProduct_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/CreateProduct.min.css") ? Url("CreateProduct.min.css") : Url("CreateProduct.css");
                         
                    public static readonly string CreateProduct_css_map = Url("CreateProduct.css.map");
                    public static readonly string CreateProduct_min_css = Url("CreateProduct.min.css");
                    public static readonly string main_scss = Url("main.scss");
                    public static readonly string main_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/main.min.css") ? Url("main.min.css") : Url("main.css");
                         
                    public static readonly string main_css_map = Url("main.css.map");
                    public static readonly string main_min_css = Url("main.min.css");
                    public static readonly string reset_scss = Url("reset.scss");
                    public static readonly string reset_css = T4MVCHelpers.IsProduction() && T4Extensions.FileExists(URLPATH + "/reset.min.css") ? Url("reset.min.css") : Url("reset.css");
                         
                    public static readonly string reset_css_map = Url("reset.css.map");
                    public static readonly string reset_min_css = Url("reset.min.css");
                }
            
                [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
                public static class web {
                    private const string URLPATH = "~/Areas/Admin/Content/web";
                    public static string Url() { return T4MVCHelpers.ProcessVirtualPath(URLPATH); }
                    public static string Url(string fileName) { return T4MVCHelpers.ProcessVirtualPath(URLPATH + "/" + fileName); }
                    public static readonly string AdminProductsList_html = Url("AdminProductsList.html");
                }
            
            }
        
        }
    }
    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public static partial class Bundles
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static partial class Scripts {}
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public static partial class Styles {}
    }
}

[GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
internal static class T4MVCHelpers {
    // You can change the ProcessVirtualPath method to modify the path that gets returned to the client.
    // e.g. you can prepend a domain, or append a query string:
    //      return "http://localhost" + path + "?foo=bar";
    private static string ProcessVirtualPathDefault(string virtualPath) {
        // The path that comes in starts with ~/ and must first be made absolute
        string path = VirtualPathUtility.ToAbsolute(virtualPath);
        
        // Add your own modifications here before returning the path
        return path;
    }

    // Calling ProcessVirtualPath through delegate to allow it to be replaced for unit testing
    public static Func<string, string> ProcessVirtualPath = ProcessVirtualPathDefault;

    // Calling T4Extension.TimestampString through delegate to allow it to be replaced for unit testing and other purposes
    public static Func<string, string> TimestampString = System.Web.Mvc.T4Extensions.TimestampString;

    // Logic to determine if the app is running in production or dev environment
    public static bool IsProduction() { 
        return (HttpContext.Current != null && !HttpContext.Current.IsDebuggingEnabled); 
    }
}





#endregion T4MVC
#pragma warning restore 1591, 3008, 3009


