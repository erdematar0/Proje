using ETicaret.Business.Abstract;
using ETicaret.Business.Common;
using ETicaret.Business.DTOs;
using ETicaret.DataAccess.UnitOfWork;
using ETicaret.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Business.Concrete;

public class AddressService : IAddressService
{
    private readonly IUnitOfWork _uow;
    public AddressService(IUnitOfWork uow) => _uow = uow;

    public Task<List<AddressDto>> GetByUserAsync(string userId) =>
        _uow.Addresses.Query().AsNoTracking()
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.IsDefault).ThenByDescending(a => a.Id)
            .Select(a => new AddressDto(a.Id, a.Title, a.FullName, a.Phone, a.City, a.District, a.AddressLine, a.IsDefault))
            .ToListAsync();

    public async Task<AddressDto?> GetAsync(string userId, int id)
    {
        var a = await FindAsync(userId, id);
        return a is null ? null : ToDto(a);
    }

    public async Task<ServiceResult<int>> CreateAsync(string userId, AddressSaveDto dto)
    {
        var address = new Address { UserId = userId };
        Map(dto, address);

        var hasAny = await _uow.Addresses.AnyAsync(a => a.UserId == userId);
        address.IsDefault = dto.IsDefault || !hasAny;
        if (address.IsDefault) await ClearDefaultAsync(userId);

        await _uow.Addresses.AddAsync(address);
        await _uow.SaveChangesAsync();
        return ServiceResult<int>.Ok(address.Id, "Adres eklendi.");
    }

    public async Task<ServiceResult> UpdateAsync(string userId, int id, AddressSaveDto dto)
    {
        var address = await FindAsync(userId, id);
        if (address is null) return ServiceResult.Fail("Adres bulunamadı.");

        Map(dto, address);
        if (dto.IsDefault && !address.IsDefault)
        {
            await ClearDefaultAsync(userId);
            address.IsDefault = true;
        }

        _uow.Addresses.Update(address);
        await _uow.SaveChangesAsync();
        return ServiceResult.Ok("Adres güncellendi.");
    }

    public async Task<ServiceResult> DeleteAsync(string userId, int id)
    {
        var address = await FindAsync(userId, id);
        if (address is null) return ServiceResult.Fail("Adres bulunamadı.");

        _uow.Addresses.Remove(address);

        if (address.IsDefault)
        {
            var next = await _uow.Addresses.Query()
                                 .Where(a => a.UserId == userId && a.Id != id)
                                 .OrderByDescending(a => a.Id)
                                 .FirstOrDefaultAsync();
            if (next is not null) next.IsDefault = true;
        }

        await _uow.SaveChangesAsync();
        return ServiceResult.Ok("Adres silindi.");
    }

    public async Task<ServiceResult> SetDefaultAsync(string userId, int id)
    {
        var address = await FindAsync(userId, id);
        if (address is null) return ServiceResult.Fail("Adres bulunamadı.");

        await ClearDefaultAsync(userId);
        address.IsDefault = true;
        await _uow.SaveChangesAsync();
        return ServiceResult.Ok("Varsayılan adres güncellendi.");
    }

    private Task<Address?> FindAsync(string userId, int id) =>
        _uow.Addresses.GetAsync(a => a.Id == id && a.UserId == userId);

    private async Task ClearDefaultAsync(string userId)
    {
        var defaults = await _uow.Addresses.Query()
                                 .Where(a => a.UserId == userId && a.IsDefault)
                                 .ToListAsync();
        foreach (var d in defaults) d.IsDefault = false;
    }

    private static void Map(AddressSaveDto dto, Address a)
    {
        a.Title = dto.Title.Trim();
        a.FullName = dto.FullName.Trim();
        a.Phone = dto.Phone.Trim();
        a.City = dto.City.Trim();
        a.District = string.IsNullOrWhiteSpace(dto.District) ? null : dto.District.Trim();
        a.AddressLine = dto.AddressLine.Trim();
    }

    private static AddressDto ToDto(Address a) =>
        new(a.Id, a.Title, a.FullName, a.Phone, a.City, a.District, a.AddressLine, a.IsDefault);
}
