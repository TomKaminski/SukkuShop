using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SukkuShop.Infrastructure.Binders;

namespace SukkuShop.Models
{
    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Nie wylogowuj mnie")]
        public bool RememberMe { get; set; }
    }

    [ModelBinder(typeof(RegisterModelBinder))]
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Imie")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [StringLength(100, ErrorMessage = "{0} musi zawierać przynajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Podane hasła nie są takie same")]
        public string ConfirmPassword { get; set; }

        [RegularExpression("[1-9][0-9]{8}|[1-9][0-9]{2}\\s[0-9]{3}\\s[0-9]{3}",
            ErrorMessage = "Telefon powinien składać się z 9 cyfr")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [StringLength(100, ErrorMessage = "{0} musi zawierać przynajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Podane hasła nie są takie same")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public bool BrowserRemembered { get; set; }
        public ChangeUserInfoViewModel ChangeUserInfoViewModel { get; set; }
        public bool IsNull { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [StringLength(100, ErrorMessage = "{0} musi zawierać przynajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Nowe hasła nie są takie same")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeUserInfoViewModel
    {
        //[Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("[A-Za-z]*", ErrorMessage = "Imie nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("[A-Za-z]*", ErrorMessage = "Nazwisko nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

       // [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("[A-Za-z]*", ErrorMessage = "Miasto nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        //[Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("[A-Za-z]*", ErrorMessage = "Ulica nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Ulica")]
        public string Street { get; set; }

        //[Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("^[1-9][0-9]{0,3}[A-Z]{0,1}$", ErrorMessage = "Numer domu jest niepoprawny")]
        [Display(Name = "Numer domu")]
        public string Number { get; set; }

        //[Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression("[1-9][0-9]{8}|[1-9][0-9]{2}\\s[0-9]{3}\\s[0-9]{3}",
            ErrorMessage = "Telefon powinien składać się z 9 cyfr")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        //[Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Kod pocztowy")]
        [RegularExpression("[0-9]{2}-[0-9]{3}", ErrorMessage = "Kod pocztowy jest niepoprawny (xx-xxx)")]
        public string PostalCode { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [DataType(DataType.Password)]
        [Display(Name = "Stare hasło")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [StringLength(100, ErrorMessage = "{0} musi zawierać przynajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Nowe hasła nie są takie same.")]
        public string ConfirmPassword { get; set; }
    }
}