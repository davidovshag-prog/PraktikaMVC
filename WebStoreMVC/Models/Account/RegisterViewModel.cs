using System.ComponentModel.DataAnnotations;

namespace WebStoreMVC.Models.Account;

public class RegisterViewModel
{
    [Display(Name = "Прізвище")]
    [Required(ErrorMessage = "Вкажіть Прізвище")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Ім'я")]
    [Required(ErrorMessage = "Вкажіть Ім'я")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Електронна пошта")]
    [Required(ErrorMessage = "Вкажіть Пошта")]
    [EmailAddress(ErrorMessage = "Не вірно вказали пошту")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Вкажіть Пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Підтвердіть пароль")]
    [Required(ErrorMessage = "Вкажіть повтор пароля")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Паролі не співпадають")]
    public string ConfirmPassword { get; set; } = null!;

    [Display(Name = "Оберіть фото користувача")]
    [Required(ErrorMessage = "Вкажіть фото для користувача")]
    public IFormFile? FileImage { get; set; }
}
