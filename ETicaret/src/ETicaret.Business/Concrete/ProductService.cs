using ETicaret.Business.Abstract;
using ETicaret.Business.Common;
using ETicaret.Business.DTOs;
using ETicaret.DataAccess.UnitOfWork;
using ETicaret.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Business.Concrete;

public class ProductService : IProductService
{
    public const int MaxPageSize = 50;

    private readonly IUnitOfWork _uow;
    public ProductService(IUnitOfWork uow) => _uow = uow;

    public async Task<PagedResult<ProductDto>> GetFilteredAsync(ProductFilterDto f)
    {
        var query = _uow.Products.Query().AsNoTracking()
                        .Where(p => p.IsActive);

        if (!string.IsNullOrWhiteSpace(f.Search))
            query = query.Where(p => p.Name.Contains(f.Search) || (p.Description != null && p.Description.Contains(f.Search)));
        if (f.CategoryId.HasValue) query = query.Where(p => p.CategoryId == f.CategoryId);
        if (f.MinPrice.HasValue) query = query.Where(p => p.Price >= f.MinPrice);
        if (f.MaxPrice.HasValue) query = query.Where(p => p.Price <= f.MaxPrice);

        query = f.SortBy switch
        {
            "price_asc" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            "name" => query.OrderBy(p => p.Name),
            "rating" => query.OrderByDescending(p => p.Reviews.Average(r => (double?)r.Rating) ?? 0)
                             .ThenByDescending(p => p.Reviews.Count).ThenBy(p => p.Id),
            _ => query.OrderByDescending(p => p.CreatedDate).ThenByDescending(p => p.Id)
        };

        var page = Math.Max(1, f.Page);
        var pageSize = Math.Clamp(f.PageSize, 1, MaxPageSize);
        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize)
                               .Select(Projection)
                               .ToListAsync();

        return new PagedResult<ProductDto> { Items = items, TotalCount = total, Page = page, PageSize = pageSize };
    }

    public Task<List<ProductDto>> GetAllAsync(bool includeInactive = false)
    {
        var query = _uow.Products.Query().AsNoTracking();
        if (!includeInactive) query = query.Where(p => p.IsActive);
        return query.OrderByDescending(p => p.Id).Select(Projection).ToListAsync();
    }

    public Task<ProductDto?> GetByIdAsync(int id) =>
        _uow.Products.Query().AsNoTracking().Where(p => p.Id == id).Select(Projection).FirstOrDefaultAsync();

    public async Task<List<ProductDto>> GetRelatedAsync(int productId, int count = 4)
    {
        var categoryId = await _uow.Products.Query().Where(p => p.Id == productId)
                                   .Select(p => (int?)p.CategoryId).FirstOrDefaultAsync();
        if (categoryId is null) return [];

        return await _uow.Products.Query().AsNoTracking()
            .Where(p => p.IsActive && p.Id != productId)
            .OrderBy(p => p.CategoryId == categoryId ? 0 : 1)
            .ThenBy(p => p.Stock > 0 ? 0 : 1)
            .ThenByDescending(p => p.Reviews.Average(r => (double?)r.Rating) ?? 0)
            .ThenByDescending(p => p.Reviews.Count)
            .ThenBy(p => p.Id)
            .Take(count)
            .Select(Projection)
            .ToListAsync();
    }

    public async Task<ServiceResult<int>> CreateAsync(ProductSaveDto dto)
    {
        if (!await CategoryExistsAsync(dto.CategoryId))
            return ServiceResult<int>.Fail("Kategori bulunamadı.");

        var product = new Product();
        Map(dto, product);
        await _uow.Products.AddAsync(product);
        await _uow.SaveChangesAsync();
        return ServiceResult<int>.Ok(product.Id, "Ürün eklendi.");
    }

    public async Task<ServiceResult> UpdateAsync(int id, ProductSaveDto dto)
    {
        var product = await _uow.Products.GetByIdAsync(id);
        if (product is null) return ServiceResult.Fail("Ürün bulunamadı.");
        if (!await CategoryExistsAsync(dto.CategoryId))
            return ServiceResult.Fail("Kategori bulunamadı.");

        Map(dto, product);
        _uow.Products.Update(product);
        await _uow.SaveChangesAsync();
        return ServiceResult.Ok("Ürün güncellendi.");
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await _uow.Products.GetByIdAsync(id);
        if (product is null) return ServiceResult.Fail("Ürün bulunamadı.");

        _uow.Products.SoftDelete(product);
        await _uow.SaveChangesAsync();
        return ServiceResult.Ok("Ürün pasife alındı.");
    }

    public Task<List<ProductDto>> GetFeaturedAsync(int count = 6) =>
        _uow.Products.Query().AsNoTracking()
            .Where(p => p.IsActive && p.Stock > 0)
            .OrderByDescending(p => p.Reviews.Average(r => (double?)r.Rating) ?? 0)
            .ThenByDescending(p => p.Reviews.Count)
            .ThenBy(p => p.Id)
            .Take(count)
            .Select(Projection)
            .ToListAsync();

    private Task<bool> CategoryExistsAsync(int id) =>
        _uow.Categories.AnyAsync(c => c.Id == id && c.IsActive);

    private static void Map(ProductSaveDto dto, Product p)
    {
        p.Name = dto.Name.Trim();
        p.Description = dto.Description;
        p.Specifications = string.IsNullOrWhiteSpace(dto.Specifications) ? null : dto.Specifications.Trim();
        p.Price = dto.Price;
        p.Stock = dto.Stock;
        p.ImageUrl = dto.ImageUrl;
        p.CategoryId = dto.CategoryId;
        p.IsActive = dto.IsActive;
    }

    private static readonly System.Linq.Expressions.Expression<Func<Product, ProductDto>> Projection = p =>
        new ProductDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.ImageUrl, p.CategoryId,
                       p.Category.Name, p.IsActive, p.Specifications,
                       p.Reviews.Average(r => (double?)r.Rating) ?? 0, p.Reviews.Count);
}
