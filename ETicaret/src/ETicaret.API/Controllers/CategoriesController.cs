using ETicaret.Business.Abstract;
using ETicaret.Business.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoriesController(ICategoryService categoryService) => _categoryService = categoryService;

    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> GetAll() => Ok(await _categoryService.GetAllAsync());

    [HttpGet("{id:int}"), AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var c = await _categoryService.GetByIdAsync(id);
        return c is null ? NotFound() : Ok(c);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategorySaveDto dto)
    {
        var result = await _categoryService.CreateAsync(dto);
        return result.Success
            ? CreatedAtAction(nameof(GetById), new { id = result.Data }, new { id = result.Data })
            : BadRequest(new { result.Message });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategorySaveDto dto)
    {
        var result = await _categoryService.UpdateAsync(id, dto);
        return result.Success ? NoContent() : NotFound(new { result.Message });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _categoryService.DeleteAsync(id);
        return result.Success ? NoContent() : BadRequest(new { result.Message });
    }
}
