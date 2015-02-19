using Postal;

namespace SukkuShop.Models
{
    public class ActivationEmail : Email
    {
        public string To { get; set; }
        public string CallbackUrl { get; set; }
    }

    public class ProductDemandEmail : Email
    {
        public string To { get; set; }
        public string CallbackUrl { get; set; }
        public string Name { get; set; }
        public string IconName { get; set; }
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

    public class OrderCancelEmail : Email
    {
        public string To { get; set; }
        public int Id { get; set; }
    }

    public class ChangeEmail : Email
    {
        public string To { get; set; }
        public int Id { get; set; }
        public string OldEmail { get; set; }
        public string CallbackUrl { get; set; }
    }

    public class ChangeOrderStateEmail : Email
    {
        public string To { get; set; }
        public int Id { get; set; }
        public string State { get; set; }
        public string StateDescription { get; set; }
        public string PackageName { get; set; }
        public OrderViewModelsSummary OrderViewModelsSummary { get; set; }
    }
}