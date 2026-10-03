using ETicaret.Business.Abstract;
using ETicaret.Business.Common;
using ETicaret.Business.DTOs;
using ETicaret.DataAccess.UnitOfWork;
using ETicaret.Entities.Concrete;
using ETicaret.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Business.Concrete;

public class ContactService : IContactService
{
    private readonly IUnitOfWork _uow;
    private readonly TimeProvider _time;

    public ContactService(IUnitOfWork uow, TimeProvider time)
    {
        _uow = uow;
        _time = time;
    }

    public async Task<ServiceResult> CreateAsync(ContactMessageSaveDto dto, string? userId)
    {
        if (!string.IsNullOrWhiteSpace(dto.Website))
            return ServiceResult.Ok(SuccessMessage);

        await _uow.ContactMessages.AddAsync(new ContactMessage
        {
            Name = dto.Name.Trim(),
            Email = dto.Email.Trim(),
            Phone = string.IsNullOrWhiteSpace(dto.Phone) ? null : dto.Phone.Trim(),
            Subject = dto.Subject.Trim(),
            Message = dto.Message.Trim(),
            UserId = userId
        });
        await _uow.SaveChangesAsync();
        return ServiceResult.Ok(SuccessMessage);
    }

    public Task<List<ContactMessageDto>> GetAllAsync(ContactMessageStatus? status = null)
    {
        var query = _uow.ContactMessages.Query().AsNoTracking();
        if (status.HasValue) query = query.Where(m => m.Status == status);

        return query.OrderBy(m => m.Status == ContactMessageStatus.New ? 0 : 1)
                    .ThenByDescending(m => m.CreatedDate).ThenByDescending(m => m.Id)
                    .Select(Projection)
                    .ToListAsync();
    }

    public Task<ContactMessageDto?> GetAsync(int id) =>
        _uow.ContactMessages.Query().AsNoTracking().Where(m => m.Id == id).Select(Projection).FirstOrDefaultAsync();

    public Task<int> CountNewAsync() => _uow.ContactMessages.CountAsync(m => m.Status == ContactMessageStatus.New);

    public async Task MarkReadAsync(int id)
    {
        var m = await _uow.ContactMessages.GetByIdAsync(id);
        if (m is null || m.Status != ContactMessageStatus.New) return;

        m.Status = ContactMessageStatus.Read;
        _uow.ContactMessages.Update(m);
        await _uow.SaveChangesAsync();
    }

    public async Task<ServiceResult> UpdateAsync(int id, ContactMessageStatus status, string? adminNote)
    {
        var m = await _uow.ContactMessages.GetByIdAsync(id);
        if (m is null) return ServiceResult.Fail("Mesaj bulunamadı.");

        if (status == ContactMessageStatus.Replied && m.Status != ContactMessageStatus.Replied)
            m.RepliedDate = _time.GetUtcNow().UtcDateTime;
        else if (status != ContactMessageStatus.Replied)
            m.RepliedDate = null;

        m.Status = status;
        m.AdminNote = string.IsNullOrWhiteSpace(adminNote) ? null : adminNote.Trim();
        _uow.ContactMessages.Update(m);
        await _uow.SaveChangesAsync();
        return ServiceResult.Ok("Mesaj güncellendi.");
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var m = await _uow.ContactMessages.GetByIdAsync(id);
        if (m is null) return ServiceResult.Fail("Mesaj bulunamadı.");

        _uow.ContactMessages.Remove(m);
        await _uow.SaveChangesAsync();
        return ServiceResult.Ok("Mesaj silindi.");
    }

    public const string SuccessMessage = "Mesajın bize ulaştı, en kısa sürede dönüş yapacağız.";

    private static readonly System.Linq.Expressions.Expression<Func<ContactMessage, ContactMessageDto>> Projection = m =>
        new ContactMessageDto(m.Id, m.Name, m.Email, m.Phone, m.Subject, m.Message, m.UserId,
                              m.Status, m.AdminNote, m.CreatedDate, m.RepliedDate);
}
