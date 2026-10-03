using ETicaret.Business.Abstract;
using ETicaret.Business.Common;
using ETicaret.Business.Concrete;
using ETicaret.Business.DTOs;
using ETicaret.Entities.Enums;

namespace ETicaret.Tests.Business;

public class OrderServiceTests
{
    private const string ValidCard = "4111111111111111";

    private static CreateOrderDto Order(params CartLineDto[] lines) => new()
    {
        UserId = TestDb.UserId,
        FullName = "Ali Yılmaz",
        Address = "Atatürk Cad. No:1",
        City = "İzmir",
        Phone = "05551112233",
        CardNumber = ValidCard,
        Lines = lines.ToList()
    };

    private static OrderService CreateSut(string dbName, IPaymentService? payment = null)
        => new(TestDb.Uow(TestDb.Create(dbName)), payment ?? new FakePaymentService());

    [Fact]
    public async Task CreateAsync_ValidCart_CreatesPaidOrderAndDecreasesStock()
    {
        var dbName = Guid.NewGuid().ToString();
        var sut = CreateSut(dbName);

        var result = await sut.CreateAsync(Order(new CartLineDto(1, 2), new CartLineDto(4, 3)));

        Assert.True(result.Success, result.Message);
        using var db = TestDb.Create(dbName);
        var order = db.Orders.Single();
        Assert.Equal(result.Data, order.OrderNumber);
        Assert.Equal(OrderStatus.Paid, order.Status);
        Assert.Equal(2 * 1899.90m + 3 * 349.90m, order.TotalPrice);
        Assert.Equal("1111", order.CardLast4);
        Assert.Equal(23, db.Products.Find(1)!.Stock);
        Assert.Equal(97, db.Products.Find(4)!.Stock);
    }

    [Fact]
    public async Task CreateAsync_UsesPriceFromDatabase_NotFromClient()
    {
        var dbName = Guid.NewGuid().ToString();
        var sut = CreateSut(dbName);

        await sut.CreateAsync(Order(new CartLineDto(2, 1)));

        using var db = TestDb.Create(dbName);
        Assert.Equal(3499.00m, db.OrderItems.Single().UnitPrice);
    }

    [Fact]
    public async Task CreateAsync_InsufficientStock_FailsAndKeepsStock()
    {
        var dbName = Guid.NewGuid().ToString();
        var sut = CreateSut(dbName);

        var result = await sut.CreateAsync(Order(new CartLineDto(3, 9)));

        Assert.False(result.Success);
        Assert.Contains("yeterli stok yok", result.Message);
        using var db = TestDb.Create(dbName);
        Assert.Empty(db.Orders);
        Assert.Equal(8, db.Products.Find(3)!.Stock);
    }

    [Fact]
    public async Task CreateAsync_SameProductOnTwoLines_ChecksCombinedStock()
    {
        var sut = CreateSut(Guid.NewGuid().ToString());

        var result = await sut.CreateAsync(Order(new CartLineDto(3, 5), new CartLineDto(3, 4)));

        Assert.False(result.Success);
    }

    [Fact]
    public async Task CreateAsync_InactiveProduct_Fails()
    {
        var dbName = Guid.NewGuid().ToString();
        using (var db = TestDb.Create(dbName))
        {
            db.Products.Find(5)!.IsActive = false;
            db.SaveChanges();
        }

        var result = await CreateSut(dbName).CreateAsync(Order(new CartLineDto(5, 1)));

        Assert.False(result.Success);
        Assert.Equal("Sepetteki bir ürün artık satışta değil.", result.Message);
    }

    [Fact]
    public async Task CreateAsync_EmptyCart_Fails()
    {
        var result = await CreateSut(Guid.NewGuid().ToString()).CreateAsync(Order());
        Assert.False(result.Success);
        Assert.Equal("Sepetiniz boş.", result.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    public async Task CreateAsync_NonPositiveQuantity_FailsAndKeepsStock(int quantity)
    {
        var dbName = Guid.NewGuid().ToString();

        var result = await CreateSut(dbName).CreateAsync(Order(new CartLineDto(2, 1), new CartLineDto(1, quantity)));

        Assert.False(result.Success);
        using var db = TestDb.Create(dbName);
        Assert.Empty(db.Orders);
        Assert.Equal(25, db.Products.Find(1)!.Stock);
    }

    [Fact]
    public async Task CreateAsync_PaymentFails_NothingIsSaved()
    {
        var dbName = Guid.NewGuid().ToString();
        var sut = CreateSut(dbName, new DecliningPaymentService());

        var result = await sut.CreateAsync(Order(new CartLineDto(1, 1)));

        Assert.False(result.Success);
        Assert.Equal("Bakiye yetersiz.", result.Message);
        using var db = TestDb.Create(dbName);
        Assert.Empty(db.Orders);
        Assert.Equal(25, db.Products.Find(1)!.Stock);
    }

    [Fact]
    public async Task GetByUserAsync_ReturnsOnlyThatUsersOrders()
    {
        var dbName = Guid.NewGuid().ToString();
        var sut = CreateSut(dbName);
        await sut.CreateAsync(Order(new CartLineDto(1, 1)));
        var other = Order(new CartLineDto(4, 1));
        other.UserId = TestDb.OtherUserId;
        await sut.CreateAsync(other);

        var orders = await sut.GetByUserAsync(TestDb.UserId);

        var order = Assert.Single(orders);
        Assert.Equal("Kablosuz Kulaklık", Assert.Single(order.Items).ProductName);
    }

    private sealed class DecliningPaymentService : IPaymentService
    {
        public Task<ServiceResult> PayAsync(string cardNumber, decimal amount)
            => Task.FromResult(ServiceResult.Fail("Bakiye yetersiz."));
    }
}
