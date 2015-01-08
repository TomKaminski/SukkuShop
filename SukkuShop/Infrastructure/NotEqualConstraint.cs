using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Routing;
using SukkuShop.Models;

namespace SukkuShop.Infrastructure
{
    //public class IsEnum : IRouteConstraint
    //{
    //    public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
    //        RouteDirection routeDirection)
    //    {
    //        return Enum.IsDefined(typeof (SortMethod), values[parameterName]);
    //    }
    //}

    public sealed class NotEqualTo : ValidationAttribute
    {
        private const string DefaultErrorMessage = "{0} nie może być równe {1}.";
        public string OtherProperty { get; private set; }

        public NotEqualTo(string otherProperty)
            : base(DefaultErrorMessage)
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException("otherProperty");
            }

            OtherProperty = otherProperty;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, OtherProperty);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.Equals(OtherProperty))
            {
                return new ValidationResult(
                    FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }
}