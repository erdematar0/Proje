namespace ETicaret.Business.DTOs;

public record ProductDto(int Id, string Name, string? Description, decimal Price, int Stock,
                         string? ImageUrl, int CategoryId, string CategoryName, bool IsActive,
                         string? Specifications = null, double AverageRating = 0, int ReviewCount = 0);

public class ProductSaveDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public string? Specifications { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public bool IsActive { get; set; } = true;
}

public class ProductFilterDto
{
    public string? Search { get; set; }
    public int? CategoryId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? SortBy { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 9;
}
