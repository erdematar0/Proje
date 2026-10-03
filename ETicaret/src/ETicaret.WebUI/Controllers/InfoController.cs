using Microsoft.AspNetCore.Mvc;

namespace ETicaret.WebUI.Controllers;

public class InfoController : Controller
{
    [HttpGet("hakkimizda")]
    public IActionResult About() => View();

    [HttpGet("sss")]
    public IActionResult Faq() => View();

    [HttpGet("kargo-ve-teslimat")]
    public IActionResult Shipping() => View();

    [HttpGet("iptal-ve-iade")]
    public IActionResult Returns() => View();

    [HttpGet("gizlilik-politikasi")]
    public IActionResult Privacy() => View();

    [HttpGet("kullanim-kosullari")]
    public IActionResult Terms() => View();
}
