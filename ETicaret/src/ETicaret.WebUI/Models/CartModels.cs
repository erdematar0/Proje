namespace ETicaret.WebUI.Models;

public class CartItem
{
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal => UnitPrice * Quantity;
}

public class Cart
{
    public List<CartItem> Items { get; set; } = new();
    public decimal Total => Items.Sum(i => i.LineTotal);
    public int ItemCount => Items.Sum(i => i.Quantity);
}
