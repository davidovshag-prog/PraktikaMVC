using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStoreMVC.Constants;
using WebStoreMVC.Data.Entities.Identity;
using WebStoreMVC.Interfaces;
using WebStoreMVC.Models.Account;

namespace WebStoreMVC.Controllers;

public class AccountController(
    UserManager<UserEntity> userManager,
    SignInManager<UserEntity> signInManager,
    IImageService imageService
    ) : Controller
{
    [HttpGet] //Вхід нового користувача
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost] //Вхід нового користувача
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid) //Зберігаємо категорію в БД, якщо модель валідна
        {
            //Шукаємо користувача по email
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                //пошук користувача по паролю
                var res = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (res.Succeeded)
                {
                    await signInManager.SignInAsync(user, false); // залогінюємо користувача
                    return Redirect("/");
                }
            }
            ModelState.AddModelError("", "Користувач з таким email не знайдений");
        }
        return View(model); // Якщо модель не валідна, повертаємо її назад на форму для виправлення помилок
    }

    [HttpGet] //Реєстрація нового користувача
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost] //Реєстрація нового користувача
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid) //Зберігаємо категорію в БД, якщо модель валідна
        {
            string fileName = "default.jpg";
            //Як зберегти фото
            if (model.FileImage != null)
            {
                fileName = await imageService.SaveImageAsync(model.FileImage);
            }
            //Заповнюю таблицю коритувачів в БД
            var user = new UserEntity
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                Image = fileName
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                result = await userManager.AddToRoleAsync(user, Roles.User);
                await signInManager.SignInAsync(user, false); // залогінюємо користувача
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Помилка при реєстрації користувача");
                return View(model);
            }
        }
        return View(model); // Якщо модель не валідна, повертаємо її назад на форму для виправлення помилок
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync(); // розлогінюємо користувача
        return Redirect("/");
    }
}
