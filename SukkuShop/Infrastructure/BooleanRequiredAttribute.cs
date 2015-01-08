using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SukkuShop.Infrastructure
{
    public class BooleanRequiredAttribute : RequiredAttribute 
    {
        public override bool IsValid(object value)
        {
            return value != null && (bool)value;
        }
    }

   
}