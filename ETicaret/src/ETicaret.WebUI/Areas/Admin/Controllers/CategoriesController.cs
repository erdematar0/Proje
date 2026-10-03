using ETicaret.Business.Abstract;
using ETicaret.Business.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Areas.Admin.Controllers;

public class CategoriesController : AdminBaseController
{
    private readonly ICategoryService _categoryService;
    public CategoriesController(ICategoryService categoryService) => _categoryService = categoryService;

    public async Task<IActionResult> Index() => View(await _categoryService.GetAllAsync());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategorySaveDto dto)
    {
        if (!ModelState.IsValid)
        {
            Notify(false, ModelState.Values.SelectMany(v => v.Errors).First().ErrorMessage);
            return RedirectToAction(nameof(Index));
        }
        var result = await _categoryService.CreateAsync(dto);
        Notify(result.Success, result.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var c = await _categoryService.GetByIdAsync(id);
        if (c is null) return NotFound();
        ViewBag.Id = id;
        return View(new CategorySaveDto { Name = c.Name, Description = c.Description });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CategorySaveDto dto)
    {
        if (!ModelState.IsValid) { ViewBag.Id = id; return View(dto); }
        var result = await _categoryService.UpdateAsync(id, dto);
        Notify(result.Success, result.Message);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _categoryService.DeleteAsync(id);
        Notify(result.Success, result.Message);
        return RedirectToAction(nameof(Index));
    }
}
