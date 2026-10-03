using ETicaret.Business.Concrete;
using ETicaret.Business.DTOs;
using ETicaret.DataAccess.Context;
using ETicaret.Entities.Concrete;
using ETicaret.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Tests.Business;

public class UserServiceTests
{
    private readonly AppDbContext _db = TestDb.Create();
    private readonly UserService _sut;

    public UserServiceTests()
    {
        var uow = TestDb.Uow(_db);
        _sut = new UserService(_db, new OrderService(uow, new FakePaymentService()), new AddressService(uow), TimeProvider.System);
    }

    private void AddOrder(string userId, decimal total, OrderStatus status, DateTime created)
    {
        _db.Orders.Add(new Order
        {
            OrderNumber = Guid.NewGuid().ToString("N")[..12], UserId = userId, TotalPrice = total, Status = status,
            FullName = "x", Address = "x", City = "İzmir", Phone = "x", CreatedDate = created,
            OrderItems = { new OrderItem { ProductId = 1, Quantity = 1, UnitPrice = total } }
        });
        _db.SaveChanges();
    }

    [Fact]
    public async Task GetAllAsync_SummarizesOrders_ExcludingCancelledFromSpend()
    {
        AddOrder(TestDb.UserId, 100m, OrderStatus.Paid, new DateTime(2026, 9, 1));
        AddOrder(TestDb.UserId, 50m, OrderStatus.Delivered, new DateTime(2026, 9, 10));
        AddOrder(TestDb.UserId, 999m, OrderStatus.Cancelled, new DateTime(2026, 9, 5));

        var ali = (await _sut.GetAllAsync()).Single(u => u.Id == TestDb.UserId);

        Assert.Equal("Ali Yılmaz", ali.FullName);
        Assert.Equal(3, ali.OrderCount);
        Assert.Equal(150m, ali.TotalSpent);
        Assert.Equal(new DateTime(2026, 9, 10), ali.LastOrderDate);
    }

    [Fact]
    public async Task GetAllAsync_UserWithoutOrders_HasZeroes()
    {
        var ayse = (await _sut.GetAllAsync()).Single(u => u.Id == TestDb.OtherUserId);

        Assert.Equal(0, ayse.OrderCount);
        Assert.Equal(0m, ayse.TotalSpent);
        Assert.Null(ayse.LastOrderDate);
    }

    [Fact]
    public async Task GetAllAsync_IncludesRoles()
    {
        _db.Roles.Add(new IdentityRole("Admin") { Id = "r-admin" });
        _db.UserRoles.Add(new IdentityUserRole<string> { UserId = TestDb.OtherUserId, RoleId = "r-admin" });
        _db.SaveChanges();

        var users = await _sut.GetAllAsync();

        Assert.Equal(new[] { "Admin" }, users.Single(u => u.Id == TestDb.OtherUserId).Roles);
        Assert.Empty(users.Single(u => u.Id == TestDb.UserId).Roles);
    }

    [Theory]
    [InlineData("ayse@", TestDb.OtherUserId)]
    [InlineData("Ali Yıl", TestDb.UserId)]
    [InlineData("Demir", TestDb.OtherUserId)]
    public async Task GetAllAsync_Search(string q, string expectedId)
        => Assert.Equal(expectedId, Assert.Single(await _sut.GetAllAsync(q)).Id);

    [Fact]
    public async Task GetDetailAsync_ReturnsOnlyThatUsersOrdersAndAddresses()
    {
        AddOrder(TestDb.UserId, 100m, OrderStatus.Paid, DateTime.UtcNow);
        AddOrder(TestDb.OtherUserId, 200m, OrderStatus.Paid, DateTime.UtcNow);
        await new AddressService(TestDb.Uow(_db)).CreateAsync(TestDb.UserId, new AddressSaveDto
        {
            Title = "Ev", FullName = "Ali", Phone = "05551112233", City = "İzmir", AddressLine = "Adres"
        });

        var detail = (await _sut.GetDetailAsync(TestDb.UserId))!;

        Assert.Equal(100m, Assert.Single(detail.Orders).TotalPrice);
        Assert.Single(detail.Addresses);
        Assert.Equal(1, detail.User.OrderCount);
    }

    [Fact]
    public async Task GetDetailAsync_UnknownUser_ReturnsNull()
        => Assert.Null(await _sut.GetDetailAsync("yok"));
}
