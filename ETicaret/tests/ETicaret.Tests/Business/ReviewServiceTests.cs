using ETicaret.Business.Concrete;
using ETicaret.Business.DTOs;
using ETicaret.DataAccess.Context;
using ETicaret.Entities.Concrete;
using ETicaret.Entities.Enums;

namespace ETicaret.Tests.Business;

public class ReviewServiceTests
{
    private const int ProductId = 1;
    private readonly AppDbContext _db = TestDb.Create();
    private readonly ReviewService _sut;

    public ReviewServiceTests()
    {
        _db.Reviews.RemoveRange(_db.Reviews.Where(r => r.ProductId == ProductId));
        _db.SaveChanges();
        _sut = new ReviewService(TestDb.Uow(_db));
    }

    private static ReviewSaveDto Dto(int rating = 5) => new() { Rating = rating, Comment = "  Çok beğendim, tavsiye ederim.  " };

    private void AddOrder(string userId, OrderStatus status)
    {
        _db.Orders.Add(new Order
        {
            OrderNumber = Guid.NewGuid().ToString("N")[..12], UserId = userId, TotalPrice = 10m, Status = status,
            FullName = "x", Address = "x", City = "x", Phone = "x",
            OrderItems = { new OrderItem { ProductId = ProductId, Quantity = 1, UnitPrice = 10m } }
        });
        _db.SaveChanges();
    }

    [Fact]
    public async Task CreateAsync_SavesTrimmedReview()
    {
        var result = await _sut.CreateAsync(ProductId, TestDb.UserId, "Ali Y.", Dto(4));

        Assert.True(result.Success);
        var review = Assert.Single((await _sut.GetSummaryAsync(ProductId)).Reviews);
        Assert.Equal(("Ali Y.", 4, "Çok beğendim, tavsiye ederim."), (review.AuthorName, review.Rating, review.Comment));
        Assert.False(review.IsVerifiedPurchase);
    }

    [Fact]
    public async Task CreateAsync_SecondReviewBySameUser_Fails()
    {
        await _sut.CreateAsync(ProductId, TestDb.UserId, "Ali Y.", Dto());

        var result = await _sut.CreateAsync(ProductId, TestDb.UserId, "Ali Y.", Dto(1));

        Assert.False(result.Success);
        Assert.Equal(1, (await _sut.GetSummaryAsync(ProductId)).Count);
    }

    [Theory]
    [InlineData(OrderStatus.Delivered, true)]
    [InlineData(OrderStatus.Paid, true)]
    [InlineData(OrderStatus.Cancelled, false)]
    public async Task CreateAsync_MarksVerifiedPurchase(OrderStatus status, bool expected)
    {
        AddOrder(TestDb.UserId, status);

        await _sut.CreateAsync(ProductId, TestDb.UserId, "Ali Y.", Dto());

        Assert.Equal(expected, Assert.Single((await _sut.GetSummaryAsync(ProductId)).Reviews).IsVerifiedPurchase);
    }

    [Fact]
    public async Task CreateAsync_InactiveProduct_Fails()
    {
        _db.Products.Find(ProductId)!.IsActive = false;
        _db.SaveChanges();

        Assert.False((await _sut.CreateAsync(ProductId, TestDb.UserId, "Ali Y.", Dto())).Success);
    }

    [Fact]
    public async Task GetSummaryAsync_ComputesAverageAndDistribution()
    {
        await _sut.CreateAsync(ProductId, TestDb.UserId, "Ali Y.", Dto(5));
        await _sut.CreateAsync(ProductId, TestDb.OtherUserId, "Ayşe D.", Dto(2));

        var summary = await _sut.GetSummaryAsync(ProductId);

        Assert.Equal(3.5, summary.Average);
        Assert.Equal(new[] { 0, 1, 0, 0, 1 }, summary.Distribution);
        Assert.Equal(50, summary.Percent(5));
    }

    [Fact]
    public async Task GetSummaryAsync_NoReviews_IsEmpty()
    {
        var summary = await _sut.GetSummaryAsync(ProductId);

        Assert.Equal((0.0, 0, 0), (summary.Average, summary.Count, summary.Percent(5)));
    }

    [Fact]
    public async Task DeleteAsync_OtherUsersReview_FailsUnlessAdmin()
    {
        await _sut.CreateAsync(ProductId, TestDb.UserId, "Ali Y.", Dto());
        var id = Assert.Single((await _sut.GetSummaryAsync(ProductId)).Reviews).Id;

        Assert.False((await _sut.DeleteAsync(id, TestDb.OtherUserId, isAdmin: false)).Success);
        Assert.True((await _sut.DeleteAsync(id, TestDb.OtherUserId, isAdmin: true)).Success);
        Assert.Equal(0, (await _sut.GetSummaryAsync(ProductId)).Count);
    }

    [Fact]
    public async Task DeleteAsync_OwnReview_Succeeds()
    {
        await _sut.CreateAsync(ProductId, TestDb.UserId, "Ali Y.", Dto());
        var id = Assert.Single((await _sut.GetSummaryAsync(ProductId)).Reviews).Id;

        Assert.True((await _sut.DeleteAsync(id, TestDb.UserId, isAdmin: false)).Success);
        Assert.False(await _sut.HasReviewedAsync(ProductId, TestDb.UserId));
    }

    [Theory]
    [InlineData("Ali", "Yılmaz", "Ali Y.")]
    [InlineData("  İpek ", "ışık", "İpek I.")]
    [InlineData("Can", "", "Can")]
    public void ShortName_HidesLastName(string first, string last, string expected)
        => Assert.Equal(expected, ReviewService.ShortName(first, last));
}
