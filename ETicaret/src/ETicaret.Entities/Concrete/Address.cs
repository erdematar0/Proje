using ETicaret.Entities.Common;

namespace ETicaret.Entities.Concrete;

public class Address : BaseEntity
{
    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;

    public string Title { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string City { get; set; } = null!;
    public string? District { get; set; }
    public string AddressLine { get; set; } = null!;

    public bool IsDefault { get; set; }
}
