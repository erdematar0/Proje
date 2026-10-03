using ETicaret.Business.Abstract;
using ETicaret.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Areas.Admin.Controllers;

public class OrdersController : AdminBaseController
{
    private readonly IOrderService _orderService;
    public OrdersController(IOrderService orderService) => _orderService = orderService;

    public async Task<IActionResult> Index() => View(await _orderService.GetAllAsync());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, OrderStatus status, string? returnUrl = null)
    {
        var result = await _orderService.UpdateStatusAsync(id, status);
        Notify(result.Success, result.Message);

        return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : RedirectToAction(nameof(Index));
    }
}
