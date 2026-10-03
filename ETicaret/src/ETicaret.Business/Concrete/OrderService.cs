using ETicaret.Business.Abstract;
using ETicaret.Business.Common;
using ETicaret.Business.DTOs;
using ETicaret.DataAccess.UnitOfWork;
using ETicaret.Entities.Concrete;
using ETicaret.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Business.Concrete;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _uow;
    private readonly IPaymentService _payment;

    public OrderService(IUnitOfWork uow, IPaymentService payment)
    {
        _uow = uow;
        _payment = payment;
    }

    public async Task<ServiceResult<string>> CreateAsync(CreateOrderDto dto)
    {
        if (dto.Lines.Count == 0) return ServiceResult<string>.Fail("Sepetiniz boş.");
        if (dto.Lines.Any(l => l.Quantity <= 0)) return ServiceResult<string>.Fail("Geçersiz ürün adedi.");

        var ids = dto.Lines.Select(l => l.ProductId).ToList();
        var products = await _uow.Products.Query()
                                 .Where(p => ids.Contains(p.Id) && p.IsActive)
                                 .ToDictionaryAsync(p => p.Id);

        var order = new Order
        {
            OrderNumber = $"SP{DateTime.UtcNow:yyMMddHHmmss}{Random.Shared.Next(100, 999)}",
            UserId = dto.UserId,
            FullName = dto.FullName,
            Address = dto.Address,
            City = dto.City,
            Phone = dto.Phone,
            CardLast4 = dto.CardNumber.Length >= 4 ? dto.CardNumber[^4..] : null
        };

        foreach (var line in dto.Lines)
        {
            if (!products.TryGetValue(line.ProductId, out var product))
                return ServiceResult<string>.Fail("Sepetteki bir ürün artık satışta değil.");
            if (product.Stock < line.Quantity)
                return ServiceResult<string>.Fail($"'{product.Name}' için yeterli stok yok (kalan: {product.Stock}).");

            product.Stock -= line.Quantity;
            order.OrderItems.Add(new OrderItem { ProductId = product.Id, Quantity = line.Quantity, UnitPrice = product.Price });
        }

        order.TotalPrice = order.OrderItems.Sum(i => i.UnitPrice * i.Quantity);

        var payment = await _payment.PayAsync(dto.CardNumber, order.TotalPrice);
        if (!payment.Success) return ServiceResult<string>.Fail(payment.Message ?? "Ödeme başarısız.");

        order.Status = OrderStatus.Paid;
        await _uow.Orders.AddAsync(order);
        await _uow.SaveChangesAsync();

        return ServiceResult<string>.Ok(order.OrderNumber, "Siparişiniz alındı.");
    }

    public Task<List<OrderDto>> GetByUserAsync(string userId) =>
        BaseQuery().Where(o => o.UserId == userId).Select(Projection).ToListAsync();

    public Task<List<OrderDto>> GetAllAsync() =>
        BaseQuery().Select(Projection).ToListAsync();

    public async Task<ServiceResult> UpdateStatusAsync(int id, OrderStatus status)
    {
        var order = await _uow.Orders.GetByIdAsync(id);
        if (order is null) return ServiceResult.Fail("Sipariş bulunamadı.");
        order.Status = status;
        _uow.Orders.Update(order);
        await _uow.SaveChangesAsync();
        return ServiceResult.Ok("Sipariş durumu güncellendi.");
    }

    private IQueryable<Order> BaseQuery() =>
        _uow.Orders.Query().AsNoTracking().OrderByDescending(o => o.CreatedDate);

    private static readonly System.Linq.Expressions.Expression<Func<Order, OrderDto>> Projection = o =>
        new OrderDto(o.Id, o.OrderNumber, o.CreatedDate, o.TotalPrice, o.Status, o.FullName, o.City,
                     o.User.Email,
                     o.OrderItems.Select(i => new OrderItemDto(i.Product.Name, i.Quantity, i.UnitPrice)).ToList(),
                     o.UserId);
}
