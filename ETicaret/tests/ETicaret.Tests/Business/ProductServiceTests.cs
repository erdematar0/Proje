using ETicaret.Business.Concrete;
using ETicaret.Business.DTOs;

namespace ETicaret.Tests.Business;

public class ProductServiceTests
{
    private static ProductSaveDto Dto(int categoryId = 1) => new()
    {
        Name = "  Test Ürün  ", Price = 100m, Stock = 5, CategoryId = categoryId, IsActive = true
    };

    [Fact]
    public async Task GetFilteredAsync_ByCategory_ReturnsOnlyThatCategory()
    {
        var sut = new ProductService(TestDb.Uow(TestDb.Create()));

        var page = await sut.GetFilteredAsync(new ProductFilterDto { CategoryId = 2 });

        Assert.Equal(7, page.TotalCount);
        Assert.All(page.Items, p => Assert.Equal(2, p.CategoryId));
    }

    [Fact]
    public async Task GetFilteredAsync_PriceRangeAndSort_Works()
    {
        var sut = new ProductService(TestDb.Uow(TestDb.Create()));

        var page = await sut.GetFilteredAsync(new ProductFilterDto { MinPrice = 1400, MaxPrice = 1600, SortBy = "price_asc" });

        Assert.Equal(new[] { 1449.00m, 1599.00m }, page.Items.Select(p => p.Price));
    }

    [Fact]
    public async Task GetFilteredAsync_Search_MatchesNameOrDescription()
    {
        var sut = new ProductService(TestDb.Uow(TestDb.Create()));

        var page = await sut.GetFilteredAsync(new ProductFilterDto { Search = "pamuk" });

        Assert.Equal(new[] { "Pamuklu Nevresim Takımı", "Pamuklu Tişört" }, page.Items.Select(p => p.Name).Order());
    }

    [Fact]
    public async Task GetFilteredAsync_Paging_ReturnsRequestedPage()
    {
        var sut = new ProductService(TestDb.Uow(TestDb.Create()));

        var page = await sut.GetFilteredAsync(new ProductFilterDto { PageSize = 12, Page = 5, SortBy = "name" });

        Assert.Equal(50, page.TotalCount);
        Assert.Equal(5, page.TotalPages);
        Assert.Equal(2, page.Items.Count());
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(-5, 1)]
    [InlineData(10_000, ProductService.MaxPageSize)]
    public async Task GetFilteredAsync_InvalidPageSize_IsClamped(int requested, int expected)
    {
        var sut = new ProductService(TestDb.Uow(TestDb.Create()));

        var page = await sut.GetFilteredAsync(new ProductFilterDto { PageSize = requested });

        Assert.Equal(expected, page.PageSize);
        Assert.True(page.TotalPages >= 1);
    }

    [Fact]
    public async Task GetFilteredAsync_ExcludesInactiveProducts()
    {
        var db = TestDb.Create();
        db.Products.Find(1)!.IsActive = false;
        db.SaveChanges();
        var sut = new ProductService(TestDb.Uow(db));

        var page = await sut.GetFilteredAsync(new ProductFilterDto());

        Assert.Equal(49, page.TotalCount);
        Assert.DoesNotContain(page.Items, p => p.Id == 1);
    }

    [Fact]
    public async Task GetByIdAsync_IncludesRatingAndSpecifications()
    {
        var sut = new ProductService(TestDb.Uow(TestDb.Create()));

        var p = (await sut.GetByIdAsync(1))!;

        Assert.True(p.ReviewCount > 0);
        Assert.InRange(p.AverageRating, 1, 5);
        Assert.Contains("Bluetooth", p.Specifications);
    }

    [Fact]
    public async Task GetRelatedAsync_PrefersSameCategory_ExcludesSelfAndInactive()
    {
        var db = TestDb.Create();
        db.Products.Find(2)!.IsActive = false;
        db.SaveChanges();
        var sut = new ProductService(TestDb.Uow(db));

        var related = await sut.GetRelatedAsync(1, count: 4);

        Assert.Equal(4, related.Count);
        Assert.All(related, p => Assert.Equal(1, p.CategoryId));
        Assert.DoesNotContain(related, p => p.Id is 1 or 2);
    }

    [Fact]
    public async Task GetRelatedAsync_FillsFromOtherCategoriesWhenNeeded()
    {
        var sut = new ProductService(TestDb.Uow(TestDb.Create()));

        var related = await sut.GetRelatedAsync(1, count: 10);

        Assert.Equal(10, related.Count);
        Assert.Equal(7, related.Count(p => p.CategoryId == 1));
        Assert.All(related.Take(7), p => Assert.Equal(1, p.CategoryId));
    }

    [Fact]
    public async Task GetRelatedAsync_UnknownProduct_ReturnsEmpty()
        => Assert.Empty(await new ProductService(TestDb.Uow(TestDb.Create())).GetRelatedAsync(999));

    [Fact]
    public async Task GetFeaturedAsync_TopRatedInStockOnly()
    {
        var db = TestDb.Create();
        var best = db.Products.Find(5)!;
        best.Stock = 0;
        db.SaveChanges();
        var sut = new ProductService(TestDb.Uow(db));

        var featured = await sut.GetFeaturedAsync(6);

        Assert.Equal(6, featured.Count);
        Assert.DoesNotContain(featured, p => p.Id == 5);
        Assert.All(featured, p => Assert.True(p.Stock > 0));
        Assert.Equal(featured.OrderByDescending(p => p.AverageRating).Select(p => p.Id), featured.Select(p => p.Id));
    }

    [Fact]
    public async Task GetFilteredAsync_SortByRating_HighestFirst()
    {
        var sut = new ProductService(TestDb.Uow(TestDb.Create()));

        var items = (await sut.GetFilteredAsync(new ProductFilterDto { SortBy = "rating", PageSize = 50 })).Items.ToList();

        Assert.Equal(items.OrderByDescending(p => p.AverageRating).Select(p => p.AverageRating), items.Select(p => p.AverageRating));
    }

    [Fact]
    public async Task CreateAsync_TrimsNameAndSaves()
    {
        var sut = new ProductService(TestDb.Uow(TestDb.Create()));

        var result = await sut.CreateAsync(Dto());

        Assert.True(result.Success);
        Assert.Equal("Test Ürün", (await sut.GetByIdAsync(result.Data))!.Name);
    }

    [Fact]
    public async Task CreateAsync_UnknownCategory_Fails()
    {
        var sut = new ProductService(TestDb.Uow(TestDb.Create()));

        var result = await sut.CreateAsync(Dto(categoryId: 999));

        Assert.False(result.Success);
    }

    [Fact]
    public async Task UpdateAsync_UnknownCategory_Fails()
    {
        var sut = new ProductService(TestDb.Uow(TestDb.Create()));

        var result = await sut.UpdateAsync(1, Dto(categoryId: 999));

        Assert.False(result.Success);
        Assert.Equal("Kategori bulunamadı.", result.Message);
        Assert.Equal(1, (await sut.GetByIdAsync(1))!.CategoryId);
    }

    [Fact]
    public async Task UpdateAsync_UnknownProduct_Fails()
    {
        var sut = new ProductService(TestDb.Uow(TestDb.Create()));
        var result = await sut.UpdateAsync(999, Dto());
        Assert.False(result.Success);
    }

    [Fact]
    public async Task DeleteAsync_SoftDeletes()
    {
        var sut = new ProductService(TestDb.Uow(TestDb.Create()));

        var result = await sut.DeleteAsync(1);

        Assert.True(result.Success);
        var product = await sut.GetByIdAsync(1);
        Assert.NotNull(product);
        Assert.False(product.IsActive);
    }
}
