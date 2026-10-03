using ETicaret.Business.Abstract;
using ETicaret.Business.DTOs;
using ETicaret.Entities.Concrete;
using ETicaret.WebUI.Data;
using ETicaret.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Areas.Admin.Controllers;

public class UsersController : AdminBaseController
{
    private readonly IUserService _userService;
    private readonly IAddressService _addressService;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly TimeProvider _time;

    public UsersController(IUserService userService, IAddressService addressService,
                           UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TimeProvider time)
    {
        _userService = userService;
        _addressService = addressService;
        _userManager = userManager;
        _signInManager = signInManager;
        _time = time;
    }

    public async Task<IActionResult> Index(string? q)
    {
        ViewBag.Query = q;
        return View(await _userService.GetAllAsync(q));
    }

    public async Task<IActionResult> Detail(string id)
    {
        var detail = await _userService.GetDetailAsync(id);
        return detail is null ? NotFound() : View(detail);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null) return NotFound();

        return View(new AdminUserEditViewModel
        {
            Id = user.Id, FirstName = user.FirstName, LastName = user.LastName,
            Email = user.Email!, EmailConfirmed = user.EmailConfirmed, PhoneNumber = user.PhoneNumber,
            IsAdmin = await _userManager.IsInRoleAsync(user, DbSeeder.AdminRole),
            IsLocked = user.LockoutEnd > _time.GetUtcNow()
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AdminUserEditViewModel model)
    {
        var user = string.IsNullOrEmpty(model.Id) ? null : await _userManager.FindByIdAsync(model.Id);
        if (user is null) return NotFound();

        var isSelf = user.Id == _userManager.GetUserId(User);
        if (isSelf && !model.IsAdmin) ModelState.AddModelError(nameof(model.IsAdmin), "Kendi admin yetkini kaldıramazsın.");
        if (isSelf && model.IsLocked) ModelState.AddModelError(nameof(model.IsLocked), "Kendi hesabını kilitleyemezsin.");
        if (!ModelState.IsValid) return View(model);

        var wasAdmin = await _userManager.IsInRoleAsync(user, DbSeeder.AdminRole);
        var wasLocked = user.LockoutEnd > _time.GetUtcNow();
        var email = model.Email.Trim();
        var emailChanged = !string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase);

        user.FirstName = model.FirstName.Trim();
        user.LastName = model.LastName.Trim();
        user.Email = email;
        user.UserName = email;
        user.EmailConfirmed = model.EmailConfirmed;
        user.PhoneNumber = string.IsNullOrWhiteSpace(model.PhoneNumber) ? null : model.PhoneNumber.Trim();
        if (model.IsLocked != wasLocked)
        {
            user.LockoutEnabled = true;
            user.LockoutEnd = model.IsLocked ? DateTimeOffset.MaxValue : null;
            user.AccessFailedCount = 0;
        }

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) return ViewWithErrors(model, result);

        if (model.IsAdmin != wasAdmin)
        {
            result = model.IsAdmin
                ? await _userManager.AddToRoleAsync(user, DbSeeder.AdminRole)
                : await _userManager.RemoveFromRoleAsync(user, DbSeeder.AdminRole);
            if (!result.Succeeded) return ViewWithErrors(model, result);

            if (!model.IsAdmin && !await _userManager.IsInRoleAsync(user, DbSeeder.CustomerRole))
                await _userManager.AddToRoleAsync(user, DbSeeder.CustomerRole);
        }

        if (!string.IsNullOrEmpty(model.NewPassword))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
            if (!result.Succeeded) return ViewWithErrors(model, result, "Diğer bilgiler kaydedildi ancak şifre değiştirilemedi: ");
        }

        if (emailChanged || model.IsAdmin != wasAdmin || model.IsLocked != wasLocked || !string.IsNullOrEmpty(model.NewPassword))
            await _userManager.UpdateSecurityStampAsync(user);

        if (isSelf) await _signInManager.RefreshSignInAsync(user);

        Notify(true, "Kullanıcı bilgileri güncellendi.");
        return RedirectToAction(nameof(Detail), new { id = user.Id });
    }

    private IActionResult ViewWithErrors(AdminUserEditViewModel model, IdentityResult result, string prefix = "")
    {
        ModelState.AddModelError("", prefix + string.Join(" ", result.Errors.Select(e => e.Description)));
        return View(nameof(Edit), model);
    }

    [HttpGet]
    public async Task<IActionResult> CreateAddress(string userId)
    {
        if (await _userManager.FindByIdAsync(userId) is null) return NotFound();
        return AddressForm(userId, null, new AddressSaveDto());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAddress(string userId, AddressSaveDto dto)
    {
        if (await _userManager.FindByIdAsync(userId) is null) return NotFound();
        if (!ModelState.IsValid) return AddressForm(userId, null, dto);

        var result = await _addressService.CreateAsync(userId, dto);
        Notify(result.Success, result.Message);
        return RedirectToAction(nameof(Detail), new { id = userId });
    }

    [HttpGet]
    public async Task<IActionResult> EditAddress(string userId, int addressId)
    {
        var a = await _addressService.GetAsync(userId, addressId);
        if (a is null) return NotFound();

        return AddressForm(userId, addressId, new AddressSaveDto
        {
            Title = a.Title, FullName = a.FullName, Phone = a.Phone, City = a.City,
            District = a.District, AddressLine = a.AddressLine, IsDefault = a.IsDefault
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAddress(string userId, int addressId, AddressSaveDto dto)
    {
        if (!ModelState.IsValid) return AddressForm(userId, addressId, dto);

        var result = await _addressService.UpdateAsync(userId, addressId, dto);
        Notify(result.Success, result.Message);
        return RedirectToAction(nameof(Detail), new { id = userId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAddress(string userId, int addressId)
    {
        var result = await _addressService.DeleteAsync(userId, addressId);
        Notify(result.Success, result.Message);
        return RedirectToAction(nameof(Detail), new { id = userId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> SetDefaultAddress(string userId, int addressId)
    {
        var result = await _addressService.SetDefaultAsync(userId, addressId);
        Notify(result.Success, result.Message);
        return RedirectToAction(nameof(Detail), new { id = userId });
    }

    private ViewResult AddressForm(string userId, int? addressId, AddressSaveDto dto)
    {
        ViewBag.UserId = userId;
        ViewBag.AddressId = addressId;
        return View("AddressForm", dto);
    }
}
