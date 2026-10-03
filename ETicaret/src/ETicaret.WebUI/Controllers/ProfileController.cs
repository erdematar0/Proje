using System.Security.Claims;
using ETicaret.Business.Abstract;
using ETicaret.Business.DTOs;
using ETicaret.Entities.Concrete;
using ETicaret.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IAddressService _addressService;

    public ProfileController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAddressService addressService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _addressService = addressService;
    }

    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Challenge();

        return View(new ProfileViewModel
        {
            Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, PhoneNumber = user.PhoneNumber
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ProfileViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Challenge();

        model.Email = user.Email;
        if (!ModelState.IsValid) return View(model);

        user.FirstName = model.FirstName.Trim();
        user.LastName = model.LastName.Trim();
        var phone = string.IsNullOrWhiteSpace(model.PhoneNumber) ? null : model.PhoneNumber.Trim();
        if (user.PhoneNumber != phone)
        {
            user.PhoneNumber = phone;
            user.PhoneNumberConfirmed = false;
        }

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var e in result.Errors) ModelState.AddModelError("", e.Description);
            return View(model);
        }

        await _signInManager.RefreshSignInAsync(user);
        TempData["Success"] = "Profil bilgilerin güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult ChangePassword() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Challenge();

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", result.Errors.Any(e => e.Code == "PasswordMismatch")
                ? "Mevcut şifre hatalı."
                : string.Join(" ", result.Errors.Select(e => e.Description)));
            return View(model);
        }

        await _signInManager.RefreshSignInAsync(user);
        TempData["Success"] = "Şifren değiştirildi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Addresses() => View(await _addressService.GetByUserAsync(UserId));

    [HttpGet]
    public IActionResult CreateAddress() => View("AddressForm", new AddressSaveDto());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAddress(AddressSaveDto dto)
    {
        if (!ModelState.IsValid) return View("AddressForm", dto);

        var result = await _addressService.CreateAsync(UserId, dto);
        Notify(result.Success, result.Message);
        return RedirectToAction(nameof(Addresses));
    }

    [HttpGet]
    public async Task<IActionResult> EditAddress(int id)
    {
        var a = await _addressService.GetAsync(UserId, id);
        if (a is null) return NotFound();

        ViewBag.Id = id;
        return View("AddressForm", new AddressSaveDto
        {
            Title = a.Title, FullName = a.FullName, Phone = a.Phone, City = a.City,
            District = a.District, AddressLine = a.AddressLine, IsDefault = a.IsDefault
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAddress(int id, AddressSaveDto dto)
    {
        if (!ModelState.IsValid) { ViewBag.Id = id; return View("AddressForm", dto); }

        var result = await _addressService.UpdateAsync(UserId, id, dto);
        Notify(result.Success, result.Message);
        return RedirectToAction(nameof(Addresses));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        var result = await _addressService.DeleteAsync(UserId, id);
        Notify(result.Success, result.Message);
        return RedirectToAction(nameof(Addresses));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> SetDefaultAddress(int id)
    {
        var result = await _addressService.SetDefaultAsync(UserId, id);
        Notify(result.Success, result.Message);
        return RedirectToAction(nameof(Addresses));
    }

    private void Notify(bool success, string? message)
        => TempData[success ? "Success" : "Error"] = message;
}
