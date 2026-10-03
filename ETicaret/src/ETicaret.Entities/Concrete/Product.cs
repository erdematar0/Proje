using ETicaret.Entities.Common;

namespace ETicaret.Entities.Concrete;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }

    public string? Specifications { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
