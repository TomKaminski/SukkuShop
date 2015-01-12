using Postal;

namespace SukkuShop.Models
{
    public class ActivationEmail : Email
    {
        public string To { get; set; }
        public string CallbackUrl { get; set; }
    }

    public class ResetEmail : Email
    {
        public string To { get; set; }
        public string CallbackUrl { get; set; }
    }

    public class OrderSumEmail : Email
    {
        public string To { get; set; }
        public int Id { get; set; }
        public string CallbackUrl { get; set; }
        public OrderViewModelsSummary OrderViewModelsSummary { get; set; }
    }
}