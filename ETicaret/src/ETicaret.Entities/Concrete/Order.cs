using ETicaret.Entities.Common;
using ETicaret.Entities.Enums;

namespace ETicaret.Entities.Concrete;

public class Order : BaseEntity
{
    public string OrderNumber { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;

    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public string FullName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public string? CardLast4 { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
