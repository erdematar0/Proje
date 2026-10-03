using ETicaret.Business.Abstract;
using ETicaret.Business.DTOs;
using ETicaret.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public HomeController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index([FromQuery] ProductFilterDto filter)
    {
        filter.PageSize = 12;
        var vm = new HomeIndexViewModel
        {
            Filter = filter,
            Products = await _productService.GetFilteredAsync(filter),
            Categories = await _categoryService.GetAllAsync()
        };

        var unfiltered = string.IsNullOrWhiteSpace(filter.Search) && filter.CategoryId is null
                         && filter.MinPrice is null && filter.MaxPrice is null && filter.Page <= 1;
        if (unfiltered) vm.Featured = await _productService.GetFeaturedAsync();

        return View(vm);
    }

    public IActionResult Error() => View();
}
