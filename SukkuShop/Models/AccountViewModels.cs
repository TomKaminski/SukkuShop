using System.ComponentModel.DataAnnotations;

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
        [EmailAddress(ErrorMessage = "To nie jest adres Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string PasswordLogin { get; set; }

        [Display(Name = "Nie wylogowuj mnie")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
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
        [Compare("Password", ErrorMessage = "Podane hasła nie są takie same")]
        public string ConfirmPassword { get; set; }

        public bool KontoFirmowe { get; set; }
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
        [Compare("Password", ErrorMessage = "Podane hasła nie są takie same")]
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

    public class SetPasswordViewModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [StringLength(100, ErrorMessage = "{0} musi zawierać przynajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Nowe hasła nie są takie same")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeUserInfoViewModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [RegularExpression("[A-Za-z]*", ErrorMessage = "Imie nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [RegularExpression("[A-Za-z]*", ErrorMessage = "Nazwisko nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [RegularExpression("[A-Za-z]*", ErrorMessage = "Miasto nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Ulica nie może zawierać znaków specjalnych")]
        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [RegularExpression("^[1-9][0-9]{0,3}[A-Z]{0,1}$", ErrorMessage = "Numer domu jest niepoprawny")]
        [Display(Name = "Numer domu")]
        public string Number { get; set; }

        [Phone(ErrorMessage = "Telefon powinien składać się z 9 cyfr")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Display(Name = "Kod pocztowy")]
        [RegularExpression("[0-9]{2}-[0-9]{3}", ErrorMessage = "Kod pocztowy jest niepoprawny (xx-xxx)")]
        public string PostalCode { get; set; }
    }


    public class ChangeUserFirmaInfoViewModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Nazwa firmy nie może zawierać znaków specjalnych")]
        [Display(Name = "Nazwa firmy")]
        public string NazwaFirmy { get; set; }

        [RegularExpression("[0-9]{10}*", ErrorMessage = "Numer NIP to 10 cyfrowa liczba.")]
        [Display(Name = "NIP")]
        public string Nip { get; set; }

        [RegularExpression("[A-Za-z]*", ErrorMessage = "Miasto nie może zawierać cyfr ani znaków specjalnych")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Ulica nie może zawierać znaków specjalnych")]
        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [RegularExpression("^[1-9][0-9]{0,3}[A-Z]{0,1}$", ErrorMessage = "Numer domu jest niepoprawny")]
        [Display(Name = "Numer domu")]
        public string Number { get; set; }

        [Phone(ErrorMessage = "Telefon powinien składać się z 9 cyfr")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

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
        [Compare("NewPassword", ErrorMessage = "Nowe hasła nie są takie same.")]
        public string ConfirmPassword { get; set; }
    }
}