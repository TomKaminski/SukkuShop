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
    }

    public class OrderViewItemsTotal
    {
        public List<OrderItemSummary> OrderProductList { get; set; }
        public string TotalValue { get; set; }
    }

    public class OrderViewModelsSummary
    {
        public OrderViewItemsTotal OrderViewItemsTotal { get; set; }
        public UserAddressModel UserAddressModel { get; set; }
        public OrderShippingSummary OrderShipping { get; set; }
        public OrderPaymentSummary OrderPayment { get; set; }
        public string TotalTotalValue { get; set; }
    }

    public class OrderShippingSummary
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }

    public class OrderPaymentSummary
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }


    public class OrderItemSummary
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
        public string TotalValue { get; set; }
        public string Image { get; set; }
    }


    public class UserAddressModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("^[A-Ża-ż]*$", ErrorMessage = "Imie nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Imię")]
        [StringLength(25, ErrorMessage = "Imie musi mieć przynajmniej 2 znaki.", MinimumLength = 2)]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$", ErrorMessage = "Nazwisko nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Nazwisko")]
        [StringLength(25, ErrorMessage = "Nazwisko musi mieć przynajmniej 2 znaki.", MinimumLength = 2)]
        public string Nazwisko { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$", ErrorMessage = "Ulica nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Ulica")]
        [StringLength(25, ErrorMessage = "Ulica musi mieć przynajmniej 2 znaki.", MinimumLength = 2)]
        public string Ulica { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$", ErrorMessage = "Ulica nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Miasto")]
        [StringLength(25, ErrorMessage = "Miasto musi mieć przynajmniej 2 znaki.", MinimumLength = 2)]
        public string Miasto { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Kod pocztowy")]
        [RegularExpression("^[0-9]{2}-[0-9]{3}$", ErrorMessage = "Kod pocztowy jest niepoprawny.")]
        public string KodPocztowy { get; set; }

        [RegularExpression("^[1-9][0-9]{8}|[1-9][0-9]{2}\\s[0-9]{3}\\s[0-9]{3}$",
            ErrorMessage = "Telefon powinien składać się z 9 cyfr")]
        [Display(Name = "Telefon")]
        public string Telefon { get; set; }


        public string Numer { get; set; }

        public bool newaddress { get; set; }
    }

    public class NewOrderAddressModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [StringLength(100, ErrorMessage = "Hasło musi mieć przynajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Hasła muszą być takie same.")]
        public string ConfirmPassword { get; set; }

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

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Ulica")]
        [RegularExpression("^[A-Ża-ż]*$", ErrorMessage = "Ulica nie może zawierać cyfr ani znaków specjalnych")]
        [StringLength(25, ErrorMessage = "Ulica musi mieć przynajmniej 2 znaki.", MinimumLength = 2)]
        public string Ulica { get; set; }

        public string Numer { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Miasto")]
        [RegularExpression("^[A-Ża-ż]*$", ErrorMessage = "Miasto nie może zawierać cyfr ani znaków specjalnych")]
        [StringLength(25, ErrorMessage = "Miasto musi mieć przynajmniej 2 znaki.", MinimumLength = 2)]
        public string Miasto { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Kod pocztowy")]
        [RegularExpression("^[0-9]{2}-[0-9]{3}$", ErrorMessage = "Kod pocztowy jest niepoprawny.")]
        public string KodPocztowy { get; set; }

        [RegularExpression("^[1-9][0-9]{8}|[1-9][0-9]{2}\\s[0-9]{3}\\s[0-9]{3}$",
            ErrorMessage = "Telefon powinien składać się z 9 cyfr")]
        [Display(Name = "Telefon")]
        public string Telefon { get; set; }

        [BooleanRequired(ErrorMessage = "Proszę, zaakceptuj regulamin sklepu")]
        public bool regulamin { get; set; }

        [BooleanRequired(ErrorMessage = "Proszę, zaakceptuj politykę prywatności sklepu")]
        public bool daneosobowe { get; set; }
    }

    public class ChangeOrderAddressModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Ulica")]
        [RegularExpression("[A-Za-z]*", ErrorMessage = "Ulica nie może zawierać cyfr ani znaków specjalnych")]
        [StringLength(25, ErrorMessage = "Ulica musi mieć przynajmniej 2 znaki.", MinimumLength = 2)]
        public string Ulica { get; set; }


        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("^[1-9][0-9]{0,3}[A-Z]{0,1}$", ErrorMessage = "Numer domu jest niepoprawny")]
        [Display(Name = "Numer domu")]
        public string NrDomu { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Miasto")]
        [RegularExpression("[A-Za-z]*", ErrorMessage = "Miasto nie może zawierać cyfr ani znaków specjalnych")]
        [StringLength(25, ErrorMessage = "Miasto musi mieć przynajmniej 2 znaki.", MinimumLength = 2)]
        public string Miasto { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Kod pocztowy")]
        [RegularExpression("^[0-9]{2}-[0-9]{3}$", ErrorMessage = "Kod pocztowy jest niepoprawny.")]
        public string KodPocztowy { get; set; }
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
}