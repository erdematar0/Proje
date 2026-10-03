using ETicaret.Business.Abstract;
using ETicaret.Business.DTOs;
using ETicaret.DataAccess.Context;
using ETicaret.Entities.Concrete;
using ETicaret.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Business.Concrete;

public class UserService : IUserService
{
    private readonly AppDbContext _db;
    private readonly IOrderService _orders;
    private readonly IAddressService _addresses;
    private readonly TimeProvider _time;

    public UserService(AppDbContext db, IOrderService orders, IAddressService addresses, TimeProvider time)
    {
        _db = db;
        _orders = orders;
        _addresses = addresses;
        _time = time;
    }

    public Task<List<UserSummaryDto>> GetAllAsync(string? search = null)
    {
        var users = _db.Users.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.Trim();
            users = users.Where(u => u.Email!.Contains(s) || (u.FirstName + " " + u.LastName).Contains(s));
        }
        return SummarizeAsync(users.OrderBy(u => u.FirstName).ThenBy(u => u.LastName));
    }

    public async Task<UserDetailDto?> GetDetailAsync(string userId)
    {
        var summary = (await SummarizeAsync(_db.Users.AsNoTracking().Where(u => u.Id == userId))).SingleOrDefault();
        if (summary is null) return null;

        return new UserDetailDto(summary,
            await _addresses.GetByUserAsync(userId),
            await _orders.GetByUserAsync(userId));
    }

    private async Task<List<UserSummaryDto>> SummarizeAsync(IQueryable<AppUser> users)
    {
        var rows = await users.Select(u => new
        {
            u.Id, u.FirstName, u.LastName, u.Email, u.PhoneNumber, u.LockoutEnd,
            OrderCount = u.Orders.Count,
            TotalSpent = u.Orders.Where(o => o.Status != OrderStatus.Cancelled).Sum(o => (decimal?)o.TotalPrice) ?? 0,
            LastOrderDate = u.Orders.Max(o => (DateTime?)o.CreatedDate)
        }).ToListAsync();

        var ids = rows.Select(r => r.Id).ToList();
        var roles = (await (from ur in _db.UserRoles
                            join r in _db.Roles on ur.RoleId equals r.Id
                            where ids.Contains(ur.UserId)
                            select new { ur.UserId, r.Name }).ToListAsync())
                    .ToLookup(x => x.UserId, x => x.Name!);
        var now = _time.GetUtcNow();

        return rows.Select(r => new UserSummaryDto(
            r.Id, $"{r.FirstName} {r.LastName}", r.Email!, r.PhoneNumber,
            roles[r.Id].Order().ToList(),
            r.OrderCount, r.TotalSpent, r.LastOrderDate,
            r.LockoutEnd.HasValue && r.LockoutEnd > now)).ToList();
    }
}
