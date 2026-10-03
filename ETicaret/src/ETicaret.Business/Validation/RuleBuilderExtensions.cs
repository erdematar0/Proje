using FluentValidation;

namespace ETicaret.Business.Validation;

public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, string?> PhoneNumber<T>(this IRuleBuilder<T, string?> rule) =>
        rule.MaximumLength(20).WithMessage("Telefon en fazla 20 karakter olabilir")
            .Matches(@"^\+?[0-9 ()\-]{10,20}$").WithMessage("Geçerli bir telefon numarası girin");

    public const int MinUniquePasswordChars = 4;

    public static IRuleBuilderOptions<T, string> StrongPassword<T>(this IRuleBuilder<T, string> rule) =>
        rule.NotEmpty().WithMessage("Şifre zorunludur")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalı")
            .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermeli")
            .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermeli")
            .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermeli")
            .Must(p => !HasTripleRepeat(p)).WithMessage("Şifrede aynı karakter art arda 3 kez kullanılamaz")
            .Must(p => UniqueChars(p) >= MinUniquePasswordChars)
                .WithMessage($"Şifre en az {MinUniquePasswordChars} farklı karakter içermeli");

    private static string Fold(string? s) => (s ?? "").ToLowerInvariant();

    public static bool HasTripleRepeat(string? password)
    {
        var p = Fold(password);
        for (var i = 2; i < p.Length; i++)
            if (p[i] == p[i - 1] && p[i] == p[i - 2]) return true;
        return false;
    }

    public static int UniqueChars(string? password) => Fold(password).Distinct().Count();
}
