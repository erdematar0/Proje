using System.Security.Claims;
using ETicaret.Business.Abstract;
using ETicaret.Business.DTOs;
using ETicaret.WebUI.Models;
using ETicaret.WebUI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IAddressService _addressService;
    private readonly ICartService _cart;

    public OrderController(IOrderService orderService, IAddressService addressService, ICartService cart)
    {
        _orderService = orderService;
        _addressService = addressService;
        _cart = cart;
    }

    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        var cart = _cart.GetCart();
        if (cart.Items.Count == 0) return RedirectToAction("Index", "Cart");

        var model = new CheckoutViewModel { Cart = cart, SavedAddresses = await _addressService.GetByUserAsync(UserId) };

        var def = model.SavedAddresses.FirstOrDefault(a => a.IsDefault);
        if (def is not null)
        {
            model.FullName = def.FullName;
            model.Address = CheckoutViewModel.FormatAddress(def);
            model.City = def.City;
            model.Phone = def.Phone;
        }
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel model)
    {
        var cart = _cart.GetCart();
        model.Cart = cart;
        model.SavedAddresses = await _addressService.GetByUserAsync(UserId);
        if (cart.Items.Count == 0) return RedirectToAction("Index", "Cart");
        if (!ModelState.IsValid) return View(model);

        var result = await _orderService.CreateAsync(new CreateOrderDto
        {
            UserId = UserId,
            FullName = model.FullName,
            Address = model.Address,
            City = model.City,
            Phone = model.Phone,
            CardNumber = model.CardNumber.Replace(" ", ""),
            Lines = cart.Items.Select(i => new CartLineDto(i.ProductId, i.Quantity)).ToList()
        });

        if (!result.Success)
        {
            ModelState.AddModelError("", result.Message!);
            return View(model);
        }

        _cart.Clear();
        return RedirectToAction(nameof(Success), new { orderNumber = result.Data });
    }

    public IActionResult Success(string orderNumber) => View(model: orderNumber);

    public async Task<IActionResult> MyOrders() => View(await _orderService.GetByUserAsync(UserId));
}
