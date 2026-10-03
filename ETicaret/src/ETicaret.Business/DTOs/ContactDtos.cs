using ETicaret.Entities.Enums;

namespace ETicaret.Business.DTOs;

public record ContactMessageDto(int Id, string Name, string Email, string? Phone, string Subject, string Message,
                                string? UserId, ContactMessageStatus Status, string? AdminNote,
                                DateTime CreatedDate, DateTime? RepliedDate);

public class ContactMessageSaveDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string Subject { get; set; } = null!;
    public string Message { get; set; } = null!;

    public string? Website { get; set; }
}
