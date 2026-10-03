using System.ComponentModel.DataAnnotations;
using ETicaret.Business.Common;
using ETicaret.Business.DTOs;

namespace ETicaret.WebUI.Models;

public class HomeIndexViewModel
{
    public PagedResult<ProductDto> Products { get; set; } = new();
    public List<CategoryDto> Categories { get; set; } = new();
    public ProductFilterDto Filter { get; set; } = new();

    public List<ProductDto> Featured { get; set; } = new();
}

public class ProductDetailViewModel
{
    public ProductDto Product { get; set; } = null!;
    public List<ProductSpec> Specs { get; set; } = new();
    public List<InstallmentOption> Installments { get; set; } = new();
    public ReviewSummaryDto Reviews { get; set; } = null!;
    public List<ProductDto> Related { get; set; } = new();

    public bool CanReview { get; set; }
    public ReviewSaveDto NewReview { get; set; } = new() { Rating = 5 };
}

public class RegisterViewModel
{
    [Display(Name = "Ad")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Soyad")]
    public string LastName { get; set; } = null!;

    [Display(Name = "E-posta")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password), Display(Name = "Şifre")]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password), Display(Name = "Şifre (tekrar)")]
    public string ConfirmPassword { get; set; } = null!;
}

public class LoginViewModel
{
    [Display(Name = "E-posta")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password), Display(Name = "Şifre")]
    public string Password { get; set; } = null!;

    [Display(Name = "Beni hatırla")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}

public class CheckoutViewModel
{
    [Display(Name = "Ad Soyad")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Adres")]
    public string Address { get; set; } = null!;

    [Display(Name = "Şehir")]
    public string City { get; set; } = null!;

    [Display(Name = "Telefon")]
    public string Phone { get; set; } = null!;

    [Display(Name = "Kart Üzerindeki İsim")]
    public string CardHolder { get; set; } = null!;

    [Display(Name = "Kart Numarası")]
    public string CardNumber { get; set; } = null!;

    [Display(Name = "Son Kullanma (AA/YY)")]
    public string Expiry { get; set; } = null!;

    [Display(Name = "CVV")]
    public string Cvv { get; set; } = null!;

    public Cart? Cart { get; set; }

    public List<AddressDto> SavedAddresses { get; set; } = new();

    public static string FormatAddress(AddressDto a) =>
        a.District is null ? a.AddressLine : $"{a.AddressLine}, {a.District}";
}

public class ProfileViewModel
{
    [Display(Name = "E-posta")]
    public string? Email { get; set; }

    [Display(Name = "Ad")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Soyad")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Telefon")]
    public string? PhoneNumber { get; set; }
}

public class AdminUserEditViewModel
{
    public string Id { get; set; } = null!;

    [Display(Name = "Ad")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Soyad")]
    public string LastName { get; set; } = null!;

    [Display(Name = "E-posta")]
    public string Email { get; set; } = null!;

    [Display(Name = "E-posta onaylı")]
    public bool EmailConfirmed { get; set; }

    [Display(Name = "Telefon")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Admin yetkisi")]
    public bool IsAdmin { get; set; }

    [Display(Name = "Hesap kilitli (giriş yapamaz)")]
    public bool IsLocked { get; set; }

    [DataType(DataType.Password), Display(Name = "Yeni şifre")]
    public string? NewPassword { get; set; }
}

public class ChangePasswordViewModel
{
    [DataType(DataType.Password), Display(Name = "Mevcut şifre")]
    public string CurrentPassword { get; set; } = null!;

    [DataType(DataType.Password), Display(Name = "Yeni şifre")]
    public string NewPassword { get; set; } = null!;

    [DataType(DataType.Password), Display(Name = "Yeni şifre (tekrar)")]
    public string ConfirmNewPassword { get; set; } = null!;
}
