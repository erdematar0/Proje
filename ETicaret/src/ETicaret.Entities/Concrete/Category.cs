using ETicaret.Entities.Common;

namespace ETicaret.Entities.Concrete;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
