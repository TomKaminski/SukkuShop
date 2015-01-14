using System;
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

        public string result { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [EmailAddress(ErrorMessage = "To nie jest adres email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string result { get; set; }
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

    public class ChangeUserInfoViewModel:SharedAddressModels
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [RegularExpression("^[A-Ża-ż]*$|^Nie podano$", ErrorMessage = "Nieprawidłowa wartość")]
        [StringLength(20, ErrorMessage = "{0} musi zawierać przynajmniej {2} znaków.", MinimumLength = 2)]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [RegularExpression("^[A-Ża-ż -]*$|^Nie podano$", ErrorMessage = "Nieprawidłowa wartość")]
        [StringLength(30, ErrorMessage = "{0} musi zawierać przynajmniej {2} znaków.", MinimumLength = 2)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        public bool? Success { get; set; }
        public bool CzyFirmowe { get; set; }
    }


    public class ChangeUserFirmaInfoViewModel:SharedAddressModels
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [RegularExpression("^[A-Ża-ż 0-9-]*$|^Nie podano$", ErrorMessage = "Nazwa nie może zawierać znaków specjalnych")]
        [StringLength(50, ErrorMessage = "{0} musi zawierać przynajmniej {2} znaków.", MinimumLength = 2)]
        [Display(Name = "Nazwa firmy")]
        public string NazwaFirmy { get; set; }

        [RegularExpression("^[0-9]{10}$|^Nie podano$", ErrorMessage = "Numer NIP to 10 cyfrowa liczba.")]
        [StringLength(10, ErrorMessage = "{0} musi zawierać przynajmniej {2} znaków.", MinimumLength = 10)]
        [Display(Name = "NIP")]
        public string Nip { get; set; }
        public bool? Success { get; set; }
        public bool CzyFirmowe { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [StringLength(100, ErrorMessage = "{0} musi zawierać przynajmniej {2} znaków.", MinimumLength = 6)]
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

        public bool? Success { get; set; }
    }

    public class AccountOrderItemModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string ActualState { get; set; }
    }

    public class AccountOrderItemViewModel
    {
        public int Id { get; set; }
        public string OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string ActualState { get; set; }
    }

    public class AccountOrderViewModelsSummary
    {
        public OrderViewItemsTotal OrderViewItemsTotal { get; set; }
        public CartAddressModel UserAddressModel { get; set; }
        public SharedShippingOrderSummaryModels OrderShipping { get; set; }
        public SharedShippingOrderSummaryModels OrderPayment { get; set; }
        public string OrderDat { get; set; }
        public string TotalTotalValue { get; set; }
        public string OrderInfo { get; set; }
        public bool Firma { get; set; }
        public int Id { get; set; }
    }
}