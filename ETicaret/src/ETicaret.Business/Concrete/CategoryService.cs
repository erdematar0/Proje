using ETicaret.Business.Abstract;
using ETicaret.Business.Common;
using ETicaret.Business.DTOs;
using ETicaret.DataAccess.UnitOfWork;
using ETicaret.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Business.Concrete;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _uow;
    public CategoryService(IUnitOfWork uow) => _uow = uow;

    public Task<List<CategoryDto>> GetAllAsync() =>
        _uow.Categories.Query().AsNoTracking()
            .Where(c => c.IsActive)
            .OrderBy(c => c.Name)
            .Select(c => new CategoryDto(c.Id, c.Name, c.Description, c.Products.Count(p => p.IsActive)))
            .ToListAsync();

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var c = await _uow.Categories.GetByIdAsync(id);
        return c is null ? null : new CategoryDto(c.Id, c.Name, c.Description, 0);
    }

    public async Task<ServiceResult<int>> CreateAsync(CategorySaveDto dto)
    {
        if (await _uow.Categories.AnyAsync(c => c.Name == dto.Name && c.IsActive))
            return ServiceResult<int>.Fail("Bu isimde bir kategori zaten var.");

        var category = new Category { Name = dto.Name.Trim(), Description = dto.Description };
        await _uow.Categories.AddAsync(category);
        await _uow.SaveChangesAsync();
        return ServiceResult<int>.Ok(category.Id, "Kategori eklendi.");
    }

    public async Task<ServiceResult> UpdateAsync(int id, CategorySaveDto dto)
    {
        var category = await _uow.Categories.GetByIdAsync(id);
        if (category is null) return ServiceResult.Fail("Kategori bulunamadı.");

        category.Name = dto.Name.Trim();
        category.Description = dto.Description;
        _uow.Categories.Update(category);
        await _uow.SaveChangesAsync();
        return ServiceResult.Ok("Kategori güncellendi.");
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var category = await _uow.Categories.GetByIdAsync(id);
        if (category is null) return ServiceResult.Fail("Kategori bulunamadı.");
        if (await _uow.Products.AnyAsync(p => p.CategoryId == id && p.IsActive))
            return ServiceResult.Fail("Bu kategoride aktif ürünler var, önce onları taşıyın.");

        _uow.Categories.SoftDelete(category);
        await _uow.SaveChangesAsync();
        return ServiceResult.Ok("Kategori silindi.");
    }
}
