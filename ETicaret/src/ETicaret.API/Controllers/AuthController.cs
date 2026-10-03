using ETicaret.API.Auth;
using ETicaret.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly JwtTokenService _tokens;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, JwtTokenService tokens)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokens = tokens;
    }

    [HttpPost("login")]
    [ProducesResponseType<TokenResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null) return Unauthorized(new { Message = "E-posta veya şifre hatalı." });

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);
        if (result.IsLockedOut) return Unauthorized(new { Message = "Çok fazla hatalı deneme. Hesabınız geçici olarak kilitlendi." });
        if (!result.Succeeded) return Unauthorized(new { Message = "E-posta veya şifre hatalı." });

        return Ok(await _tokens.CreateTokenAsync(user));
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult Me() => Ok(new
    {
        Name = User.Identity?.Name,
        Roles = User.FindAll(JwtTokenService.RoleClaim).Select(c => c.Value)
    });
}
