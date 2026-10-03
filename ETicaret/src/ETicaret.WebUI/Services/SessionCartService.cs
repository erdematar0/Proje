using System.Text.Json;
using ETicaret.WebUI.Models;

namespace ETicaret.WebUI.Services;

public class SessionCartService : ICartService
{
    private const string Key = "CART";
    private readonly IHttpContextAccessor _accessor;

    public SessionCartService(IHttpContextAccessor accessor) => _accessor = accessor;

    private ISession Session => _accessor.HttpContext!.Session;

    public Cart GetCart()
    {
        var json = Session.GetString(Key);
        return json is null ? new Cart() : JsonSerializer.Deserialize<Cart>(json) ?? new Cart();
    }

    public void AddItem(CartItem item)
    {
        var cart = GetCart();
        var existing = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
        if (existing is null) cart.Items.Add(item);
        else existing.Quantity += item.Quantity;
        Save(cart);
    }

    public void UpdateQuantity(int productId, int quantity)
    {
        var cart = GetCart();
        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item is null) return;
        if (quantity <= 0) cart.Items.Remove(item);
        else item.Quantity = quantity;
        Save(cart);
    }

    public void RemoveItem(int productId)
    {
        var cart = GetCart();
        cart.Items.RemoveAll(i => i.ProductId == productId);
        Save(cart);
    }

    public void Clear() => Session.Remove(Key);

    private void Save(Cart cart) => Session.SetString(Key, JsonSerializer.Serialize(cart));
}
