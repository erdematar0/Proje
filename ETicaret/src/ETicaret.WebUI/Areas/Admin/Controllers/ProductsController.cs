using ETicaret.Business.Abstract;
using ETicaret.Business.DTOs;
using ETicaret.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETicaret.WebUI.Areas.Admin.Controllers;

public class ProductsController : AdminBaseController
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IImageStorage _images;

    public ProductsController(IProductService productService, ICategoryService categoryService, IImageStorage images)
    {
        _productService = productService;
        _categoryService = categoryService;
        _images = images;
    }

    public async Task<IActionResult> Index() => View(await _productService.GetAllAsync(includeInactive: true));

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadCategories();
        return View(new ProductSaveDto());
    }

    [HttpPost, ValidateAntiForgeryToken]
    [RequestSizeLimit(5 * 1024 * 1024)]
    public async Task<IActionResult> Create(ProductSaveDto dto, IFormFile? imageFile)
    {
        if (!ModelState.IsValid || !await TryUploadImageAsync(dto, imageFile))
        {
            await LoadCategories();
            return View(dto);
        }

        var result = await _productService.CreateAsync(dto);
        if (!result.Success && imageFile is not null) _images.Delete(dto.ImageUrl);

        Notify(result.Success, result.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var p = await _productService.GetByIdAsync(id);
        if (p is null) return NotFound();

        await LoadCategories();
        ViewBag.Id = id;
        return View(new ProductSaveDto
        {
            Name = p.Name, Description = p.Description, Specifications = p.Specifications, Price = p.Price, Stock = p.Stock,
            ImageUrl = p.ImageUrl, CategoryId = p.CategoryId, IsActive = p.IsActive
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    [RequestSizeLimit(5 * 1024 * 1024)]
    public async Task<IActionResult> Edit(int id, ProductSaveDto dto, IFormFile? imageFile)
    {
        var existing = await _productService.GetByIdAsync(id);
        if (existing is null) return NotFound();

        if (!ModelState.IsValid || !await TryUploadImageAsync(dto, imageFile))
        {
            await LoadCategories();
            ViewBag.Id = id;
            return View(dto);
        }

        var result = await _productService.UpdateAsync(id, dto);
        if (result.Success)
        {
            if (existing.ImageUrl != dto.ImageUrl) _images.Delete(existing.ImageUrl);
        }
        else if (imageFile is not null)
        {
            _images.Delete(dto.ImageUrl);
        }

        Notify(result.Success, result.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);
        Notify(result.Success, result.Message);
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> TryUploadImageAsync(ProductSaveDto dto, IFormFile? imageFile)
    {
        if (imageFile is null) return true;

        var upload = await _images.SaveAsync(imageFile, HttpContext.RequestAborted);
        if (!upload.Success)
        {
            ModelState.AddModelError("imageFile", upload.Message!);
            return false;
        }

        dto.ImageUrl = upload.Data;
        return true;
    }

    private async Task LoadCategories()
        => ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
}
