#region

using System;
using System.Web.Mvc;
using SukkuShop.Areas.Admin.Models;
using SukkuShop.Models;

#endregion

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
            //model.LastName = request.Form.Get("LastName");
            model.Password = request.Form.Get("Password");
            model.ConfirmPassword = request.Form.Get("ConfirmPassword");
            //model.Phone = (request.Form.Get("Phone") == "" ? "<Nie określono>":request.Form.Get("Phone"));
            //model.Name = request.Form.Get("Name");
            return model;
        }
    }

    public class ShippingPaymentModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            var model = (ShippingPaymentCreateModel)bindingContext.Model ?? new ShippingPaymentCreateModel();
            model.Description = TryGet(bindingContext, "description");
            model.Name = TryGet(bindingContext, "name");
            model.Price = TryGet(bindingContext, "price");
            model.MaxWeight = TryGet(bindingContext, "weight");
            return model;
        }

        private string TryGet(ModelBindingContext bindingContext, string key)
        {
            if (String.IsNullOrEmpty(key))
                return null;

            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + key);
            if (valueResult == null && bindingContext.FallbackToEmptyPrefix)
                valueResult = bindingContext.ValueProvider.GetValue(key);

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueResult);

            if (valueResult == null)
                return null;

            try
            {
                return (string)valueResult.ConvertTo(typeof(string));
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex);
                return null;
            }
        }
    }
}