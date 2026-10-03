using ETicaret.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Areas.Admin.Controllers;

public class DashboardController : AdminBaseController
{
    private readonly IProductService _products;
    private readonly ICategoryService _categories;
    private readonly IOrderService _orders;

    public DashboardController(IProductService products, ICategoryService categories, IOrderService orders)
    {
        _products = products;
        _categories = categories;
        _orders = orders;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _orders.GetAllAsync();
        var products = await _products.GetAllAsync();
        ViewBag.ProductCount = products.Count;
        ViewBag.LowStock = products.Where(p => p.Stock < 5).ToList();
        ViewBag.CategoryCount = (await _categories.GetAllAsync()).Count;
        ViewBag.OrderCount = orders.Count;
        ViewBag.Revenue = orders.Where(o => o.Status != Entities.Enums.OrderStatus.Cancelled).Sum(o => o.TotalPrice);
        return View(orders.Take(5).ToList());
    }
}
