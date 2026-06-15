using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStoreMVC.Constants;
using WebStoreMVC.Data;
using WebStoreMVC.Data.Entities.Identity;
using WebStoreMVC.Data.Entities.Order;
using WebStoreMVC.Infrastructure.Extensions;
using WebStoreMVC.Mapper;
using WebStoreMVC.Models.Cart;
using WebStoreMVC.Models.Order;

namespace WebStoreMVC.Controllers;

[Authorize]
public class OrdersController(MyContextShopMVC myContext,
    UserManager<UserEntity> userManager,
    OrderMapper orderMapper) : Controller
{
    // Сторінка оформлення замовлення
    public IActionResult Checkout()
    {
        var cart = HttpContext.Session
            .GetObject<List<CartItemModel>>("Cart")
            ?? [];

        if (!cart.Any())
        {
            return RedirectToAction("Index", "Cart");
        }

        var model = new CheckoutModel
        {
            CartItems = cart.Select(x => new OrderItemModel
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Image = x.Image,
                Price = x.Price,
                Quantity = x.Quantity
            }).ToList()
        };

        return View(model);
    }

    // Підтвердження оформлення замовлення
    [HttpPost]
    public async Task<IActionResult> Checkout(CheckoutModel model)
    {
        var cart = HttpContext.Session
            .GetObject<List<CartItemModel>>("Cart")
            ?? [];

        if (!cart.Any())
        {
            return RedirectToAction("Index", "Cart");
        }

        var userId = userManager.GetUserId(User);

        var newStatus = myContext.OrderStatuses
            .Single(x => x.Name == OrderStatuses.New);

        var order = new OrderEntity
        {
            UserId = long.Parse(userId!),
            OrderStatusId = newStatus.Id,
            CreatedAt = DateTime.UtcNow,
            Address = model.Address,
            Phone = model.Phone,
            TotalAmount = cart.Sum(x => x.Price * x.Quantity),
            OrderItems = cart.Select(x => new OrderItemEntity
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Price = x.Price
            }).ToList()
        };

        myContext.Orders.Add(order);
        await myContext.SaveChangesAsync();

        // Очищення кошика
        HttpContext.Session.Remove("Cart");

        return RedirectToAction("Details", new { id = order.Id });
    }

    // Список замовлень користувача
    public async Task<IActionResult> Index()
    {
        var userId = userManager.GetUserId(User);

        var orders = await myContext.Orders
            .Include(x => x.OrderStatus)
            .Where(x => x.UserId == long.Parse(userId!))
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        var model = orderMapper.ListOrderEntityToListItemModels(orders);

        return View(model);
    }

    // Деталі конкретного замовлення
    public async Task<IActionResult> Details(long id)
    {
        var userId = userManager.GetUserId(User);

        var order = await myContext.Orders
            .Include(x => x.OrderStatus)
            .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                    .ThenInclude(x => x!.ProductImages)
            .SingleOrDefaultAsync(x => x.Id == id && x.UserId == long.Parse(userId!));

        if (order == null)
        {
            return NotFound();
        }

        var model = orderMapper.OrderEntityToDetailsModel(order);

        return View(model);
    }
}