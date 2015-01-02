using System.ComponentModel.DataAnnotations;

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