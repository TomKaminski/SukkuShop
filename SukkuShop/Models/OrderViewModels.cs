using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SukkuShop.Models
{
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

    public class OrderDataViewModel
    {
        public CartUserLogin CartUserLogin { get; set; }
        public NewOrderAddressModel NewOrderAddressModel { get; set; }
        public ChangeOrderAddressModel ChangeOrderAddressModel { get; set; }
    }

    public class UserAddressModel
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Ulica { get; set; }
        public string Miasto { get; set; }
        public string KodPocztowy { get; set; }
        public string Telefon { get; set; }
        public string Numer { get; set; }
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


        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("^[1-9][0-9]{0,3}[A-Z]{0,1}$", ErrorMessage = "Numer domu jest niepoprawny")]
        [Display(Name = "Numer domu")]
        public string Numer { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Miasto")]
        [RegularExpression("^[A-Ża-ż]*$", ErrorMessage = "Miasto nie może zawierać cyfr ani znaków specjalnych")]
        [StringLength(25, ErrorMessage = "Miasto musi mieć przynajmniej 2 znaki.", MinimumLength = 2)]
        public string Miasto { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Kod pocztowy")]
        [RegularExpression("[0-9]{2}-[0-9]{3}", ErrorMessage = "Kod pocztowy jest niepoprawny.")]
        public string KodPocztowy { get; set; }

        [RegularExpression("[1-9][0-9]{8}|[1-9][0-9]{2}\\s[0-9]{3}\\s[0-9]{3}",
            ErrorMessage = "Telefon powinien składać się z 9 cyfr")]
        [Display(Name = "Telefon")]
        public string Telefon { get; set; }
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
        [RegularExpression("[0-9]{2}-[0-9]{3}", ErrorMessage = "Kod pocztowy jest niepoprawny.")]
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