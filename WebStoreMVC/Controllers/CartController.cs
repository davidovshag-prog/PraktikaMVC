using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStoreMVC.Data;
using WebStoreMVC.Infrastructure.Extensions;
using WebStoreMVC.Models.Cart;

namespace WebStoreMVC.Controllers;

public class CartController(MyContextShopMVC myContext) : Controller
{
    public IActionResult Index()
    {
        var cart = HttpContext.Session
            .GetObject<List<CartItemModel>>("Cart") ?? [];
        return View(cart);
    }
        [HttpPost]
    public IActionResult AddToCart(long productId, int quantity = 1)
    {
        // TODO: Додати товар у Session або БД
        var cart = HttpContext.Session
             .GetObject<List<CartItemModel>>("Cart") 
             ?? [];
        var item = cart.FirstOrDefault(x=>x.ProductId == productId);
        if(item != null)
        {
            item.Quantity += quantity;
        }
        else
        {
            var prod = myContext.Products
                .Include(x => x.ProductImages)
                .SingleOrDefault(x => x.Id == productId);
            cart.Add(new CartItemModel
            {
                ProductId = prod.Id,
                Name = prod.Name,
                Price = prod.Price,
                Quantity = quantity,
                Image = prod.ProductImages
                .OrderBy(x => x.Priority)
                .FirstOrDefault()?.Name ?? "no_image.png",
            });
        }

        HttpContext.Session.SetObject("Cart", cart);

        return RedirectToAction("Index");
    }
}
