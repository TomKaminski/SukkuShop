using System.ComponentModel.DataAnnotations;
using SukkuShop.Infrastructure;

namespace SukkuShop.Models
{
    public class SharedAddressModels
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("^[A-Ża-ż0-9]*[-, ]{0,1}[A-Ża-ż0-9]*[-, ]{0,1}[A-Ża-ż0-9]*$", ErrorMessage = "Ulica nie może zawierać znaków specjalnych")]
        [Display(Name = "Ulica")]
        [StringLength(25, ErrorMessage = "Ulica musi mieć conajmniej 2 znaki.", MinimumLength = 2)]
        [NotEqualTo("Nie podano", ErrorMessage = "Zła wartość")]
        public string Ulica { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("^[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*[-, ]{0,1}[A-Ża-ż]*$", ErrorMessage = "Miasto nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Miasto")]
        [StringLength(25, ErrorMessage = "Miasto musi mieć conajmniej 2 znaki.", MinimumLength = 2)]
        [NotEqualTo("Nie podano", ErrorMessage = "Zła wartość")]
        public string Miasto { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Kod pocztowy")]
        [RegularExpression("^[0-9]{2}-[0-9]{3}$", ErrorMessage = "Kod pocztowy jest niepoprawny.")]
        [NotEqualTo("Nie podano", ErrorMessage = "Zła wartość")]
        public string KodPocztowy { get; set; }

        [Display(Name = "Telefon")]
        [NotEqualTo("Nie podano", ErrorMessage = "Zła wartość")]
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Phone(ErrorMessage = "Numer telefonu jest niepoprawny")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [NotEqualTo("Nie podano", ErrorMessage = "Zła wartość")]
        [RegularExpression("^[1-9][0-9]{0,4}[A-Ża-ż]{0,1}[/]?[0-9]*$", ErrorMessage = "Podaj poprawny numer domu.")]
        public string Numer { get; set; }

    }

    public class SharedAccountModels:SharedAddressModels
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "To nie jest adres Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [StringLength(100, ErrorMessage = "Hasło musi mieć przynajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Hasła muszą być takie same.")]
        public string ConfirmPassword { get; set; }   
    }

    public class SharedShippingOrderSummaryModels
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }
    }

    public class SharedProductModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageName { get; set; }
        public decimal PriceAfterDiscount { get; set; }
    }
}
