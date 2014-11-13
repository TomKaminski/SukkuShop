using System.Web.Mvc;
using SukkuShop.Models;


namespace SukkuShop.Infrastructure.Binders
{
    public class RegisterModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            var model = (RegisterViewModel) bindingContext.Model ?? new RegisterViewModel();
            model.Email = request.Form.Get("Email");
            model.LastName = request.Form.Get("LastName");
            model.Password = request.Form.Get("Password");
            model.ConfirmPassword = request.Form.Get("ConfirmPassword");
            model.Phone = (request.Form.Get("Phone") == "" ? "<Nie określono>":request.Form.Get("Phone"));
            model.Name = request.Form.Get("Name");
            return model;
        }
    }
}