using ETicaret.Business.Abstract;
using ETicaret.Business.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService) => _productService = productService;

    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] ProductFilterDto filter)
        => Ok(await _productService.GetFilteredAsync(filter));

    [HttpGet("{id:int}"), AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductSaveDto dto)
    {
        var result = await _productService.CreateAsync(dto);
        if (!result.Success) return BadRequest(new { result.Message });

        var created = await _productService.GetByIdAsync(result.Data);
        return CreatedAtAction(nameof(GetById), new { id = result.Data }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductSaveDto dto)
    {
        var result = await _productService.UpdateAsync(id, dto);
        return result.Success ? NoContent() : NotFound(new { result.Message });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);
        return result.Success ? NoContent() : NotFound(new { result.Message });
    }
}
