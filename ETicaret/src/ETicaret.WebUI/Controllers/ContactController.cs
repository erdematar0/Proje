using System.Security.Claims;
using ETicaret.Business.Abstract;
using ETicaret.Business.DTOs;
using ETicaret.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ETicaret.WebUI.Controllers;

public class ContactController : Controller
{
    public const string RateLimitPolicy = "contact";

    public static readonly string[] Subjects =
        ["Sipariş", "İade ve değişim", "Ürün hakkında", "Ödeme", "Öneri / şikayet", "Diğer"];

    private readonly IContactService _contactService;
    private readonly UserManager<AppUser> _userManager;

    public ContactController(IContactService contactService, UserManager<AppUser> userManager)
    {
        _contactService = contactService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var dto = new ContactMessageSaveDto();

        if (await _userManager.GetUserAsync(User) is { } user)
        {
            dto.Name = $"{user.FirstName} {user.LastName}";
            dto.Email = user.Email!;
            dto.Phone = user.PhoneNumber;
        }
        return View(dto);
    }

    [HttpPost, ValidateAntiForgeryToken]
    [EnableRateLimiting(RateLimitPolicy)]
    public async Task<IActionResult> Index(ContactMessageSaveDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        var result = await _contactService.CreateAsync(dto, User.FindFirstValue(ClaimTypes.NameIdentifier));
        TempData["Success"] = result.Message;
        return RedirectToAction(nameof(Index));
    }
}
