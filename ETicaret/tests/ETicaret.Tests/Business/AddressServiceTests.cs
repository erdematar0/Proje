using ETicaret.Business.Concrete;
using ETicaret.Business.DTOs;

namespace ETicaret.Tests.Business;

public class AddressServiceTests
{
    private readonly AddressService _sut = new(TestDb.Uow(TestDb.Create()));

    private static AddressSaveDto Dto(string title = "Ev", bool isDefault = false) => new()
    {
        Title = title, FullName = "Ali Yılmaz", Phone = "05551112233",
        City = "İzmir", District = "Karşıyaka", AddressLine = "Cemal Gürsel Cad. No:5", IsDefault = isDefault
    };

    [Fact]
    public async Task CreateAsync_FirstAddress_BecomesDefault()
    {
        await _sut.CreateAsync(TestDb.UserId, Dto(isDefault: false));

        Assert.True(Assert.Single(await _sut.GetByUserAsync(TestDb.UserId)).IsDefault);
    }

    [Fact]
    public async Task CreateAsync_NewDefault_UnsetsPreviousDefault()
    {
        await _sut.CreateAsync(TestDb.UserId, Dto("Ev"));
        await _sut.CreateAsync(TestDb.UserId, Dto("İş", isDefault: true));

        var list = await _sut.GetByUserAsync(TestDb.UserId);

        Assert.Equal("İş", Assert.Single(list, a => a.IsDefault).Title);
        Assert.Equal("İş", list[0].Title);
    }

    [Fact]
    public async Task SetDefaultAsync_KeepsExactlyOneDefault()
    {
        var ev = await _sut.CreateAsync(TestDb.UserId, Dto("Ev"));
        await _sut.CreateAsync(TestDb.UserId, Dto("İş"));

        await _sut.SetDefaultAsync(TestDb.UserId, ev.Data);

        var list = await _sut.GetByUserAsync(TestDb.UserId);
        Assert.Equal(ev.Data, Assert.Single(list, a => a.IsDefault).Id);
    }

    [Fact]
    public async Task DeleteAsync_DefaultAddress_PromotesAnother()
    {
        var ev = await _sut.CreateAsync(TestDb.UserId, Dto("Ev"));
        await _sut.CreateAsync(TestDb.UserId, Dto("İş"));

        await _sut.DeleteAsync(TestDb.UserId, ev.Data);

        var remaining = Assert.Single(await _sut.GetByUserAsync(TestDb.UserId));
        Assert.True(remaining.IsDefault);
    }

    [Fact]
    public async Task OtherUsersAddress_CannotBeReadOrChanged()
    {
        var ev = await _sut.CreateAsync(TestDb.UserId, Dto("Ev"));

        Assert.Null(await _sut.GetAsync(TestDb.OtherUserId, ev.Data));
        Assert.Empty(await _sut.GetByUserAsync(TestDb.OtherUserId));
        Assert.False((await _sut.UpdateAsync(TestDb.OtherUserId, ev.Data, Dto("Hack"))).Success);
        Assert.False((await _sut.DeleteAsync(TestDb.OtherUserId, ev.Data)).Success);
        Assert.False((await _sut.SetDefaultAsync(TestDb.OtherUserId, ev.Data)).Success);

        Assert.Equal("Ev", (await _sut.GetAsync(TestDb.UserId, ev.Data))!.Title);
    }

    [Fact]
    public async Task CreateAsync_TrimsAndNormalizesEmptyDistrict()
    {
        var dto = Dto();
        dto.Title = "  Ev ";
        dto.District = "   ";

        var created = await _sut.CreateAsync(TestDb.UserId, dto);

        var a = (await _sut.GetAsync(TestDb.UserId, created.Data))!;
        Assert.Equal("Ev", a.Title);
        Assert.Null(a.District);
    }
}
