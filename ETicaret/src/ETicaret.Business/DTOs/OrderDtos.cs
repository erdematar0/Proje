using ETicaret.Entities.Enums;

namespace ETicaret.Business.DTOs;

public record CartLineDto(int ProductId, int Quantity);

public class CreateOrderDto
{
    public string UserId { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string CardNumber { get; set; } = null!;
    public List<CartLineDto> Lines { get; set; } = new();
}

public record OrderItemDto(string ProductName, int Quantity, decimal UnitPrice);

public record OrderDto(int Id, string OrderNumber, DateTime CreatedDate, decimal TotalPrice,
                       OrderStatus Status, string FullName, string City, string? UserEmail,
                       List<OrderItemDto> Items, string UserId);
