using System.ComponentModel.DataAnnotations;

namespace WebStoreMVC.Models.Account;

public class LoginViewModel
{
    [Display(Name = "Електронна пошта")]
    [Required(ErrorMessage = "Вкажіть Пошта")]
    [EmailAddress(ErrorMessage = "Не вірно вказали пошту")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Вкажіть Пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
