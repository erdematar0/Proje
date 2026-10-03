using ETicaret.Entities.Common;

namespace ETicaret.Entities.Concrete;

public class Review : BaseEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public string? UserId { get; set; }
    public AppUser? User { get; set; }

    public string AuthorName { get; set; } = null!;

    public int Rating { get; set; }
    public string Comment { get; set; } = null!;

    public bool IsVerifiedPurchase { get; set; }
}
