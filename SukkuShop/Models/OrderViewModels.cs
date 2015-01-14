using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SukkuShop.Infrastructure;

namespace SukkuShop.Models
{
    public class Krok1Model
    {
        [BooleanRequired(ErrorMessage = "Wybierz formę dostawy")]
        public bool shipping { get; set; }

        [BooleanRequired(ErrorMessage = "Wybierz formę płatności")]
        public bool payment { get; set; }
    }
    public class OrderViewModels
    {
        public List<OrderItem> OrderProductList { get; set; }
        public string TotalValue { get; set; }
    }

    public class OrderItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
        public string TotalValue { get; set; }
        public string Packing { get; set; }
    }

    public class OrderViewItemsTotal
    {
        public List<OrderItemSummary> OrderProductList { get; set; }
        public string TotalValue { get; set; }
    }

    public class OrderViewModelsSummary
    {
        public OrderViewItemsTotal OrderViewItemsTotal { get; set; }
        public CartAddressModel UserAddressModel { get; set; }
        public SharedShippingOrderSummaryModels OrderShipping { get; set; }
        public SharedShippingOrderSummaryModels OrderPayment { get; set; }
        public string TotalTotalValue { get; set; }
        public bool Firma { get; set; }
    }

    public class OrderItemSummary
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
        public string TotalValue { get; set; }
        public string Image { get; set; }
        public string Packing { get; set; }
    }


    public class UserAddressModel:SharedAddressModels
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("^[A-Ża-ż]*$", ErrorMessage = "Imie nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Imię")]
        [StringLength(25, ErrorMessage = "Imie musi mieć conajmniej 2 znaki.", MinimumLength = 2)]
        [NotEqualTo("Nie podano",ErrorMessage = "Zła wartość")]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$", ErrorMessage = "Nazwisko nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Nazwisko")]
        [StringLength(25, ErrorMessage = "Nazwisko musi mieć conajmniej 2 znaki.", MinimumLength = 2)]
        [NotEqualTo("Nie podano", ErrorMessage = "Zła wartość")]
        public string Nazwisko { get; set; }

        public bool Firma { get; set; }
        public bool newaddress { get; set; }

    }


    public class FirmaAddressModel:SharedAddressModels
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Nazwa firmy")]
        [StringLength(25, ErrorMessage = "Imie musi mieć przynajmniej 2 znaki.", MinimumLength = 2)]
        [NotEqualTo("Nie podano", ErrorMessage = "Zła wartość")]
        public string NazwaFirmy { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "NIP")]
        [StringLength(10, ErrorMessage = "NIP to 10 cyfrowa liczba.", MinimumLength = 10)]
        [NotEqualTo("Nie podano", ErrorMessage = "Zła wartość")]
        public string Nip { get; set; }

        public bool Firma { get; set; }
        public bool newaddress { get; set; }


    }

    public class NewOrderAddressModel:SharedAccountModels
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("^[A-Ża-ż]*$", ErrorMessage = "Imie nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Imię")]
        [StringLength(25,ErrorMessage = "Imie musi mieć przynajmniej 2 znaki.",MinimumLength = 2)]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("^[A-Ża-ż]*$", ErrorMessage = "Nazwisko nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Nazwisko")]
        [StringLength(25, ErrorMessage = "Nazwisko musi mieć przynajmniej 2 znaki.", MinimumLength = 2)]
        public string Nazwisko { get; set; }

        [BooleanRequired(ErrorMessage = "Proszę, zaakceptuj regulamin sklepu")]
        public bool Regulamin { get; set; }

        [BooleanRequired(ErrorMessage = "Proszę, zaakceptuj politykę prywatności sklepu")]
        public bool Daneosobowe { get; set; }
        public bool Firma { get; set; }
        public bool NewAccount { get; set; }
    }


    public class NewOrderAddressFirmaModel:SharedAccountModels
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Nazwa Firmy")]
        [StringLength(25, ErrorMessage = "Nazwa firmy musi mieć przynajmniej 2 znaki.", MinimumLength = 2)]
        public string NazwaFirmy { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "NIP")]
        [StringLength(10, ErrorMessage = "NIP to 10 cyfrowa liczba.", MinimumLength = 10)]
        public string Nip { get; set; }

        [BooleanRequired(ErrorMessage = "Proszę, zaakceptuj regulamin sklepu")]
        public bool Regulamin { get; set; }

        [BooleanRequired(ErrorMessage = "Proszę, zaakceptuj politykę prywatności sklepu")]
        public bool Daneosobowe { get; set; }
        public bool Firma { get; set; }
        public bool NewAccount { get; set; }
    }


    public class CartUserLogin
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
    }

    public class CartAddressModel:SharedAddressModels
    {
        public string NazwaFirmy { get; set; }
        public string Nip { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

    }

}