using System.Security.Claims;
using System.Text;
using ETicaret.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace ETicaret.API.Auth;

public record TokenResponse(string Token, DateTime ExpiresAtUtc);

public class JwtTokenService
{
    public const string NameClaim = JwtRegisteredClaimNames.Name;
    public const string RoleClaim = "role";

    private readonly JwtOptions _jwt;
    private readonly UserManager<AppUser> _userManager;
    private readonly TimeProvider _time;

    public JwtTokenService(IOptions<JwtOptions> options, UserManager<AppUser> userManager, TimeProvider time)
    {
        _jwt = options.Value;
        _userManager = userManager;
        _time = time;
    }

    public async Task<TokenResponse> CreateTokenAsync(AppUser user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(NameClaim, user.UserName!)
        };
        claims.AddRange((await _userManager.GetRolesAsync(user)).Select(r => new Claim(RoleClaim, r)));

        var expires = _time.GetUtcNow().UtcDateTime.AddMinutes(_jwt.ExpiresMinutes);
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _jwt.Issuer,
            Audience = _jwt.Audience,
            Expires = expires,
            SigningCredentials = new SigningCredentials(CreateKey(_jwt.Key), SecurityAlgorithms.HmacSha256)
        };

        return new TokenResponse(new JsonWebTokenHandler().CreateToken(descriptor), expires);
    }

    public static SymmetricSecurityKey CreateKey(string key) => new(Encoding.UTF8.GetBytes(key));
}
