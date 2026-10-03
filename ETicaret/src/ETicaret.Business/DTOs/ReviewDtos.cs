namespace ETicaret.Business.DTOs;

public record ReviewDto(int Id, string AuthorName, int Rating, string Comment, DateTime CreatedDate,
                        bool IsVerifiedPurchase, string? UserId);

public record ReviewSummaryDto(double Average, int Count, int[] Distribution, List<ReviewDto> Reviews)
{
    public int Percent(int stars) => Count == 0 ? 0 : (int)Math.Round(100.0 * Distribution[stars - 1] / Count);
}

public class ReviewSaveDto
{
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
}
