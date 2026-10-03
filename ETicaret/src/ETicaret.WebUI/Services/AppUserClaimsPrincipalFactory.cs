using System.Security.Claims;
using ETicaret.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ETicaret.WebUI.Services;

public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole>
{
    public const string FullNameClaim = "full_name";

    public AppUserClaimsPrincipalFactory(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
                                         IOptions<IdentityOptions> options)
        : base(userManager, roleManager, options) { }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        identity.AddClaim(new Claim(FullNameClaim, $"{user.FirstName} {user.LastName}".Trim()));
        return identity;
    }
}

public static class ClaimsPrincipalExtensions
{
    public static string DisplayName(this ClaimsPrincipal user) =>
        user.FindFirstValue(AppUserClaimsPrincipalFactory.FullNameClaim) is { Length: > 0 } name
            ? name
            : user.Identity?.Name ?? "";
}
