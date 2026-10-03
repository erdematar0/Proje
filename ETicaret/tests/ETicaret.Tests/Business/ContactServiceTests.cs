using ETicaret.Business.Concrete;
using ETicaret.Business.DTOs;
using ETicaret.Entities.Enums;

namespace ETicaret.Tests.Business;

public class ContactServiceTests
{
    private readonly ContactService _sut = new(TestDb.Uow(TestDb.Create()), TimeProvider.System);

    private static ContactMessageSaveDto Dto(string subject = "Sipariş", string? website = null) => new()
    {
        Name = "  Ali Yılmaz ", Email = " ali@test.com ", Phone = "  ", Subject = subject,
        Message = "Siparişim ne zaman kargoya verilecek?", Website = website
    };

    [Fact]
    public async Task CreateAsync_SavesTrimmedAsNewWithUser()
    {
        var result = await _sut.CreateAsync(Dto(), TestDb.UserId);

        Assert.True(result.Success);
        var m = Assert.Single(await _sut.GetAllAsync());
        Assert.Equal("Ali Yılmaz", m.Name);
        Assert.Equal("ali@test.com", m.Email);
        Assert.Null(m.Phone);
        Assert.Equal(TestDb.UserId, m.UserId);
        Assert.Equal(ContactMessageStatus.New, m.Status);
    }

    [Fact]
    public async Task CreateAsync_Guest_HasNoUser()
    {
        await _sut.CreateAsync(Dto(), userId: null);
        Assert.Null(Assert.Single(await _sut.GetAllAsync()).UserId);
    }

    [Fact]
    public async Task CreateAsync_HoneypotFilled_ReportsSuccessButSavesNothing()
    {
        var result = await _sut.CreateAsync(Dto(website: "http://spam.example"), null);

        Assert.True(result.Success);
        Assert.Empty(await _sut.GetAllAsync());
    }

    [Fact]
    public async Task MarkReadAsync_OnlyChangesNewMessages()
    {
        await _sut.CreateAsync(Dto(), null);
        var id = (await _sut.GetAllAsync())[0].Id;
        Assert.Equal(1, await _sut.CountNewAsync());

        await _sut.MarkReadAsync(id);
        Assert.Equal(ContactMessageStatus.Read, (await _sut.GetAsync(id))!.Status);
        Assert.Equal(0, await _sut.CountNewAsync());

        await _sut.UpdateAsync(id, ContactMessageStatus.Replied, null);
        await _sut.MarkReadAsync(id);
        Assert.Equal(ContactMessageStatus.Replied, (await _sut.GetAsync(id))!.Status);
    }

    [Fact]
    public async Task UpdateAsync_Replied_SetsDateAndNote_BackToReadClearsDate()
    {
        await _sut.CreateAsync(Dto(), null);
        var id = (await _sut.GetAllAsync())[0].Id;

        await _sut.UpdateAsync(id, ContactMessageStatus.Replied, "  Kargo bilgisi iletildi ");
        var replied = (await _sut.GetAsync(id))!;
        Assert.NotNull(replied.RepliedDate);
        Assert.Equal("Kargo bilgisi iletildi", replied.AdminNote);

        await _sut.UpdateAsync(id, ContactMessageStatus.Read, "");
        var reopened = (await _sut.GetAsync(id))!;
        Assert.Null(reopened.RepliedDate);
        Assert.Null(reopened.AdminNote);
    }

    [Fact]
    public async Task GetAllAsync_FiltersByStatus_AndListsNewFirst()
    {
        await _sut.CreateAsync(Dto("Eski"), null);
        await _sut.CreateAsync(Dto("Yeni"), null);
        var old = (await _sut.GetAllAsync()).Single(m => m.Subject == "Eski");
        await _sut.MarkReadAsync(old.Id);

        Assert.Equal("Yeni", (await _sut.GetAllAsync())[0].Subject);
        Assert.Equal("Eski", Assert.Single(await _sut.GetAllAsync(ContactMessageStatus.Read)).Subject);
        Assert.Empty(await _sut.GetAllAsync(ContactMessageStatus.Replied));
    }

    [Fact]
    public async Task DeleteAsync_RemovesMessage()
    {
        await _sut.CreateAsync(Dto(), null);
        var id = (await _sut.GetAllAsync())[0].Id;

        Assert.True((await _sut.DeleteAsync(id)).Success);
        Assert.Null(await _sut.GetAsync(id));
        Assert.False((await _sut.DeleteAsync(id)).Success);
    }
}
