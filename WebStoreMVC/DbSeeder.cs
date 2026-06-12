using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebStoreMVC.Data.Entities;
using WebStoreMVC.Data.Entities.Catalog;
using WebStoreMVC.Data.Entities.Identity;
using WebStoreMVC.Interfaces;
using WebStoreMVC.Mapper;
using WebStoreMVC.Models.Seeder;

namespace WebStoreMVC;

public static class DbSeeder
{
    public static async Task SeedData(this WebApplication webApplication)
    {
        //Це для того, щоб запрости у Dependecy Injection потрібні сервіси для роботи з базою даних та ролями.
        using var scope = webApplication.Services.CreateScope();
        // Отримуємо сервіси з DI контейнера
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<Data.MyContextShopMVC>();
        await context.Database.MigrateAsync();
        var roleManager = services.GetRequiredService<RoleManager<RoleEntity>>();
        var userManager = services.GetRequiredService<UserManager<UserEntity>>();
        if (!context.Roles.Any()) // Якщо в БД не існує ролей
        {
            // Створення ролей
            foreach (var roleName in Constants.Roles.AllRoles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new RoleEntity { Name = roleName });
                }
            }
        }

        if (!context.Users.Any()) // Якщо в БД не існує користувачів
        {
            // Отримує інтерфейс дял роботи з зображеннями, щоб встановити аватар для користувача
            var imageService = services.GetRequiredService<IImageService>();
            var jsonFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData", "Users.json");
            if (File.Exists(jsonFile))
            {
                var jsonData = await File.ReadAllTextAsync(jsonFile);
                try
                {
                    var users = JsonSerializer.Deserialize<List<SeederUserModel>>(jsonData);
                    foreach (var user in users)
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
                            Console.WriteLine("Помилка стоврення користувача " + user.Email);
                            continue;
                        }
                        foreach (var role in user.Roles)
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
                    Console.WriteLine("Помилка читання даних із Json користувачів");
                }
            }
            else
            {
                Console.WriteLine("Помилка існування файлу Users.json");
            }
        }

        if (!context.Categories.Any()) // Якщо в БД не існує категорій
        {
            // Отримує інтерфейс дял роботи з зображеннями, щоб встановити аватар для користувача
            var imageService = services.GetRequiredService<IImageService>();
            var categoryMapper = services.GetRequiredService<CategoryMapper>();
            var jsonFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData", "Categories.json");
            if (File.Exists(jsonFile))
            {
                var jsonData = await File.ReadAllTextAsync(jsonFile);
                try
                {
                    var categories = JsonSerializer.Deserialize<List<SeederCategoryModel>>(jsonData);
                    foreach (var cat in categories)
                    {
                        var entity = categoryMapper.SeederCategoryToCategory(cat);
                        entity.Image = await imageService.SaveImageFromUrlAsync(cat.Image);
                        context.Categories.Add(entity);
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка читання даних із Json Категорій");
                }
            }
            else
            {
                Console.WriteLine("Помилка існування файлу Categories.json");
            }
        }

        if (!context.Products.Any()) // Якщо в БД не існує продукт
        {
            // Отримує інтерфейс дял роботи з зображеннями, щоб встановити аватар для користувача
            var imageService = services.GetRequiredService<IImageService>();
            var productMapper = services.GetRequiredService<ProductMapper>();
            var jsonFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData", "Products.json");
            if (File.Exists(jsonFile))
            {
                var jsonData = await File.ReadAllTextAsync(jsonFile);
                try
                {
                    var products = JsonSerializer.Deserialize<List<SeederProductModel>>(jsonData);

                    foreach (var prod in products)
                    {
                        var entity = productMapper.SeederProductModelToProductEntity(prod);
                        context.Products.Add(entity);
                        await context.SaveChangesAsync();
                        short priority = 1;
                        foreach (var img in prod.Images)
                        {
                            var entityImage = new ProductImageEntity();
                            entityImage.Priority = priority;
                            entityImage.Name = await imageService.SaveImageFromUrlAsync(img);
                            entityImage.Product = entity;
                            priority++;
                            context.ProductImages.Add(entityImage);
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка читання даних із Json Products");
                }
            }
            else
            {
                Console.WriteLine("Помилка існування файлу Categories.json");
            }
        }

    }
}
