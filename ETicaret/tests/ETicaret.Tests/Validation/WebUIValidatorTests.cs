using ETicaret.WebUI.Models;
using ETicaret.WebUI.Validation;

namespace ETicaret.Tests.Validation;

public class CheckoutViewModelValidatorTests
{
    private readonly CheckoutViewModelValidator _sut = new(new FixedTime(new DateTimeOffset(2026, 9, 15, 12, 0, 0, TimeSpan.Zero)));

    private static CheckoutViewModel Valid() => new()
    {
        FullName = "Ali Yılmaz", Address = "Atatürk Cad. No:1", City = "İzmir", Phone = "05551112233",
        CardHolder = "ALI YILMAZ", CardNumber = "4111 1111 1111 1111", Expiry = "12/28", Cvv = "123"
    };

    [Fact]
    public void ValidForm_Passes() => Assert.True(_sut.Validate(Valid()).IsValid);

    [Theory]
    [InlineData("09/26", true)]
    [InlineData("10/26", true)]
    [InlineData("01/27", true)]
    [InlineData("08/26", false)]
    [InlineData("12/25", false)]
    [InlineData("01/20", false)]
    public void Expiry_ChecksDate(string expiry, bool valid)
    {
        var vm = Valid(); vm.Expiry = expiry;
        Assert.Equal(valid, _sut.Validate(vm).IsValid);
    }

    [Theory]
    [InlineData("13/28")]
    [InlineData("1/28")]
    [InlineData("1228")]
    [InlineData("")]
    public void Expiry_BadFormat_FailsWithSingleMessage(string expiry)
    {
        var vm = Valid(); vm.Expiry = expiry;

        Assert.Single(_sut.Validate(vm).Errors);
    }

    [Theory]
    [InlineData("12")]
    [InlineData("12345")]
    [InlineData("abc")]
    public void Cvv_Invalid_Fails(string cvv)
    {
        var vm = Valid(); vm.Cvv = cvv;
        Assert.False(_sut.Validate(vm).IsValid);
    }

    private sealed class FixedTime(DateTimeOffset now) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => now;
        public override TimeZoneInfo LocalTimeZone => TimeZoneInfo.Utc;
    }
}

public class RegisterViewModelValidatorTests
{
    private readonly RegisterViewModelValidator _sut = new();

    private static RegisterViewModel Valid(string password = "Sifre1") => new()
    {
        FirstName = "Ali", LastName = "Yılmaz", Email = "ali@test.com", Password = password, ConfirmPassword = password
    };

    [Fact]
    public void ValidForm_Passes() => Assert.True(_sut.Validate(Valid()).IsValid);

    [Theory]
    [InlineData("Sif1", "Şifre en az 6 karakter olmalı")]
    [InlineData("sifre1", "Şifre en az bir büyük harf içermeli")]
    [InlineData("SIFRE1", "Şifre en az bir küçük harf içermeli")]
    [InlineData("Sifreee", "Şifre en az bir rakam içermeli")]
    [InlineData("Aaaaaa1", "Şifrede aynı karakter art arda 3 kez kullanılamaz")]
    [InlineData("Sifre111", "Şifrede aynı karakter art arda 3 kez kullanılamaz")]
    [InlineData("Ababab1", "Şifre en az 4 farklı karakter içermeli")]
    public void WeakPassword_FailsWithTurkishMessage(string password, string message)
    {
        var result = _sut.Validate(Valid(password));
        Assert.Contains(result.Errors, e => e.ErrorMessage == message);
    }

    [Theory]
    [InlineData("Sifre1")]
    [InlineData("Kitap11a")]
    [InlineData("Abcd12")]
    public void AcceptablePassword_Passes(string password) => Assert.True(_sut.Validate(Valid(password)).IsValid);

    [Fact]
    public void PasswordsDoNotMatch_Fails()
    {
        var vm = Valid(); vm.ConfirmPassword = "Baska1";
        Assert.Contains(_sut.Validate(vm).Errors, e => e.PropertyName == nameof(RegisterViewModel.ConfirmPassword));
    }

    [Fact]
    public void InvalidEmail_Fails()
    {
        var vm = Valid(); vm.Email = "ali.test.com";
        Assert.False(_sut.Validate(vm).IsValid);
    }
}

public class ChangePasswordViewModelValidatorTests
{
    private readonly ChangePasswordViewModelValidator _sut = new();

    [Fact]
    public void SameAsCurrent_Fails()
    {
        var result = _sut.Validate(new ChangePasswordViewModel { CurrentPassword = "Sifre1", NewPassword = "Sifre1", ConfirmNewPassword = "Sifre1" });
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Yeni şifre mevcut şifreyle aynı olamaz");
    }

    [Fact]
    public void Valid_Passes()
        => Assert.True(_sut.Validate(new ChangePasswordViewModel { CurrentPassword = "Eski1a", NewPassword = "Yeni1a", ConfirmNewPassword = "Yeni1a" }).IsValid);
}
