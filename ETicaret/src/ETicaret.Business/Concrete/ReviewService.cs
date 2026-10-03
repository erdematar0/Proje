using ETicaret.Business.Abstract;
using ETicaret.Business.Common;
using ETicaret.Business.DTOs;
using ETicaret.DataAccess.UnitOfWork;
using ETicaret.Entities.Concrete;
using ETicaret.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Business.Concrete;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _uow;
    public ReviewService(IUnitOfWork uow) => _uow = uow;

    public async Task<ReviewSummaryDto> GetSummaryAsync(int productId)
    {
        var reviews = await _uow.Reviews.Query().AsNoTracking()
            .Where(r => r.ProductId == productId && r.IsActive)
            .OrderByDescending(r => r.CreatedDate).ThenByDescending(r => r.Id)
            .Select(r => new ReviewDto(r.Id, r.AuthorName, r.Rating, r.Comment, r.CreatedDate, r.IsVerifiedPurchase, r.UserId))
            .ToListAsync();

        var distribution = new int[5];
        foreach (var r in reviews) distribution[r.Rating - 1]++;
        var average = reviews.Count == 0 ? 0 : Math.Round(reviews.Average(r => r.Rating), 1);

        return new ReviewSummaryDto(average, reviews.Count, distribution, reviews);
    }

    public Task<bool> HasReviewedAsync(int productId, string userId) =>
        _uow.Reviews.AnyAsync(r => r.ProductId == productId && r.UserId == userId);

    public async Task<ServiceResult> CreateAsync(int productId, string userId, string authorName, ReviewSaveDto dto)
    {
        if (!await _uow.Products.AnyAsync(p => p.Id == productId && p.IsActive))
            return ServiceResult.Fail("Ürün bulunamadı.");
        if (await HasReviewedAsync(productId, userId))
            return ServiceResult.Fail("Bu ürünü zaten değerlendirdin.");

        var purchased = await _uow.Orders.AnyAsync(o => o.UserId == userId && o.Status != OrderStatus.Cancelled
                                                        && o.OrderItems.Any(i => i.ProductId == productId));

        await _uow.Reviews.AddAsync(new Review
        {
            ProductId = productId,
            UserId = userId,
            AuthorName = authorName,
            Rating = dto.Rating,
            Comment = dto.Comment.Trim(),
            IsVerifiedPurchase = purchased
        });
        try
        {
            await _uow.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return ServiceResult.Fail("Bu ürünü zaten değerlendirdin.");
        }
        return ServiceResult.Ok("Değerlendirmen için teşekkürler!");
    }

    public async Task<ServiceResult> DeleteAsync(int reviewId, string userId, bool isAdmin)
    {
        var review = await _uow.Reviews.GetByIdAsync(reviewId);
        if (review is null || (!isAdmin && review.UserId != userId))
            return ServiceResult.Fail("Değerlendirme bulunamadı.");

        _uow.Reviews.Remove(review);
        await _uow.SaveChangesAsync();
        return ServiceResult.Ok("Değerlendirme silindi.");
    }

    public static string ShortName(string firstName, string lastName)
    {
        var first = firstName.Trim();
        var last = lastName.Trim();
        return last.Length == 0 ? first : $"{first} {char.ToUpper(last[0], new System.Globalization.CultureInfo("tr"))}.";
    }
}
