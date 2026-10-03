using ETicaret.WebUI.Models;

namespace ETicaret.WebUI.Services;

public interface ICartService
{
    Cart GetCart();
    void AddItem(CartItem item);
    void UpdateQuantity(int productId, int quantity);
    void RemoveItem(int productId);
    void Clear();
}
