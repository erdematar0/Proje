using System.Globalization;

namespace ETicaret.WebUI.Extensions;

public static class FormatExtensions
{
    private static readonly CultureInfo Tr = new("tr-TR");

    public static string ToTL(this decimal value) => value.ToString("C2", Tr);
}
