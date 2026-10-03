using ETicaret.Entities.Common;
using ETicaret.Entities.Enums;

namespace ETicaret.Entities.Concrete;

public class ContactMessage : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string Subject { get; set; } = null!;
    public string Message { get; set; } = null!;

    public string? UserId { get; set; }
    public AppUser? User { get; set; }

    public ContactMessageStatus Status { get; set; } = ContactMessageStatus.New;
    public string? AdminNote { get; set; }
    public DateTime? RepliedDate { get; set; }
}
