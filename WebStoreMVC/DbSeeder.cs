using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Text.Json;
using WebStoreMVC.Data.Entities.Identity;
using WebStoreMVC.Interfaces;
using WebStoreMVC.Models.Seeder;

namespace WebStoreMVC;

public static class DbSeeder
{
    public static async Task SeedData(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<Data.MyContextShopMVC>();
        await context.Database.MigrateAsync();
        var roleManager = services.GetRequiredService<RoleManager<Data.Entities.Identity.RoleEntity>>();
        var userManager = services.GetRequiredService<UserManager<UserEntity>>();
        if (!context.Roles.Any())
        {
            // Створення ролей
            foreach (var roleName in Constants.Roles.AllRoles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new Data.Entities.Identity.RoleEntity { Name = roleName });
                }
            }
        }

        if (!context.Users.Any()) // для того щоб працювати з зображеннями та додати аватар
        {
            var imageService = services.GetRequiredService<IImageService>();
            var jsonFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData", "Users.json");
            if (File.Exists(jsonFile))
            {
                var jsonData = await File.ReadAllTextAsync(jsonFile);
                try
                {
                    var users = JsonSerializer.Deserialize<List<SeederUserModel>>(jsonData)
                    foreach(var user in users)
                    {
                        var entity = new UserEntity
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            UserName = user.Email
                        };
                        entity.Image = await imageService.SaveImageFromUrlAsync(user.Image);
                        var result = await userManager.CreateAsync(entity, user.Password);
                        if (!result.Succeeded)
                        {
                            Console.WriteLine("Помилка створення користувача " + user.Email);
                        }
                        foreach(var role in user.Roles)
                        {
                            if (await roleManager.RoleExistsAsync(role))
                                await userManager.AddToRoleAsync(entity, role);
                            else
                                Console.WriteLine("Не вдалося знайти роль " + role);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка читання даних Json користувачів: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Помилка існування файлу Users.json");
            }
            }
        } 
    }
}
