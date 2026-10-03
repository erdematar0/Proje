namespace ETicaret.API.Auth;

public class JwtOptions
{
    public const string SectionName = "Jwt";

    public const string DevKeyPrefix = "DEV-ONLY-";

    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Key { get; set; } = null!;
    public int ExpiresMinutes { get; set; } = 60;
}
