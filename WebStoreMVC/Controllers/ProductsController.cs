using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStoreMVC.Data;
using WebStoreMVC.Mapper;

namespace WebStoreMVC.Controllers
{
    public class ProductsController(MyContextShopMVC myContext, 
        ProductMapper productMapper) : Controller
    {
        public IActionResult Details(string slug)
        {
            // читаю із БД 1 товар і відображаю його 
            var items = myContext.Products
                .Include(x => x.Category)
                .Include(x => x.ProductImages)
                .SingleOrDefault(x => x.Slug == slug);
            var model = productMapper
                .ProductEntityToProductItemModel(items);

            return View(model);
        }
    }
}
