using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStoreMVC.Data;
using WebStoreMVC.Mapper;
using WebStoreMVC.Models;

namespace WebStoreMVC.Controllers
{
    public class HomeController(
        MyContextShopMVC myContext, 
        CategoryMapper categoryMapper, 
        ProductMapper productMapper) : Controller
    {

        public IActionResult Index()
        {
            var items = myContext.Categories.ToList();
            var model = categoryMapper.CategoriesToCategoryItems(items);
            return View(model);
        }

        [HttpGet]
        public IActionResult Products(string categorySlug)
        {
            var cat = myContext.Categories.SingleOrDefault(c => c.Slug == categorySlug);
            long catId = cat.Id;
            var items = myContext.Products
                .Include(x => x.Category)
                .Include(x => x.ProductImages)
                .Where(x => x.CategoryId == catId)
                .ToList();
            var modal = productMapper.ListProductEntityToItemModels(items);
            return View(modal);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
