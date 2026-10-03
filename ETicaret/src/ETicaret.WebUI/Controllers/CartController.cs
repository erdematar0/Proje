using ETicaret.Business.Abstract;
using ETicaret.WebUI.Models;
using ETicaret.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cart;
    private readonly IProductService _productService;

    public CartController(ICartService cart, IProductService productService)
    {
        _cart = cart;
        _productService = productService;
    }

    public IActionResult Index() => View(_cart.GetCart());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int productId, int quantity = 1, string? returnUrl = null)
    {
        var product = await _productService.GetByIdAsync(productId);
        if (product is null || !product.IsActive) return NotFound();

        quantity = Math.Max(1, quantity);
        var inCart = _cart.GetCart().Items.FirstOrDefault(i => i.ProductId == productId)?.Quantity ?? 0;
        if (inCart + quantity > product.Stock)
        {
            TempData["Error"] = $"Stokta yalnızca {product.Stock} adet var.";
        }
        else
        {
            _cart.AddItem(new CartItem
            {
                ProductId = product.Id, Name = product.Name, ImageUrl = product.ImageUrl,
                UnitPrice = product.Price, Quantity = quantity
            });
            TempData["Success"] = $"{product.Name} sepete eklendi.";
        }

        return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int productId, int quantity)
    {
        if (quantity > 0)
        {
            var product = await _productService.GetByIdAsync(productId);
            if (product is null || !product.IsActive)
            {
                _cart.RemoveItem(productId);
                TempData["Error"] = "Ürün artık satışta değil, sepetten çıkarıldı.";
                return RedirectToAction(nameof(Index));
            }
            if (quantity > product.Stock)
            {
                quantity = product.Stock;
                TempData["Error"] = product.Stock > 0
                    ? $"Stokta yalnızca {product.Stock} adet var, adet güncellendi."
                    : $"{product.Name} tükendi, sepetten çıkarıldı.";
            }
        }

        _cart.UpdateQuantity(productId, quantity);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Remove(int productId)
    {
        _cart.RemoveItem(productId);
        return RedirectToAction(nameof(Index));
    }
}
