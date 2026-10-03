using ETicaret.Business.Abstract;
using ETicaret.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Areas.Admin.Controllers;

public class MessagesController : AdminBaseController
{
    private readonly IContactService _contactService;
    public MessagesController(IContactService contactService) => _contactService = contactService;

    public async Task<IActionResult> Index(ContactMessageStatus? status)
    {
        ViewBag.Status = status;
        return View(await _contactService.GetAllAsync(status));
    }

    public async Task<IActionResult> Detail(int id)
    {
        await _contactService.MarkReadAsync(id);
        var message = await _contactService.GetAsync(id);
        return message is null ? NotFound() : View(message);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, ContactMessageStatus status, string? adminNote)
    {
        var result = await _contactService.UpdateAsync(id, status, adminNote);
        Notify(result.Success, result.Message);
        return RedirectToAction(nameof(Detail), new { id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _contactService.DeleteAsync(id);
        Notify(result.Success, result.Message);
        return RedirectToAction(nameof(Index));
    }
}
