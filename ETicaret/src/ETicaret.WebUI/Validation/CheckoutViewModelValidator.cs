using System.Globalization;
using ETicaret.Business.Validation;
using ETicaret.WebUI.Models;
using FluentValidation;

namespace ETicaret.WebUI.Validation;

public class CheckoutViewModelValidator : AbstractValidator<CheckoutViewModel>
{
    private readonly TimeProvider _time;

    public CheckoutViewModelValidator(TimeProvider time)
    {
        _time = time;

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Ad soyad zorunludur")
            .MaximumLength(150).WithMessage("Ad soyad en fazla 150 karakter olabilir");
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Adres zorunludur")
            .MaximumLength(500).WithMessage("Adres en fazla 500 karakter olabilir");
        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Şehir zorunludur")
            .MaximumLength(50).WithMessage("Şehir en fazla 50 karakter olabilir");
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Telefon zorunludur")
            .PhoneNumber();

        RuleFor(x => x.CardHolder)
            .NotEmpty().WithMessage("Kart üzerindeki isim zorunludur")
            .MaximumLength(100).WithMessage("İsim en fazla 100 karakter olabilir");
        RuleFor(x => x.CardNumber)
            .NotEmpty().WithMessage("Kart numarası zorunludur")
            .Matches(@"^[\d ]{15,19}$").WithMessage("Kart numarası geçersiz");
        RuleFor(x => x.Expiry)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Son kullanma tarihi zorunludur")
            .Matches(@"^(0[1-9]|1[0-2])\/\d{2}$").WithMessage("AA/YY formatında girin")
            .Must(NotBeExpired).WithMessage("Kartın son kullanma tarihi geçmiş");
        RuleFor(x => x.Cvv)
            .NotEmpty().WithMessage("CVV zorunludur")
            .Matches(@"^\d{3,4}$").WithMessage("CVV geçersiz");
    }

    private bool NotBeExpired(string expiry)
    {
        var month = int.Parse(expiry[..2], CultureInfo.InvariantCulture);
        var year = 2000 + int.Parse(expiry[3..], CultureInfo.InvariantCulture);
        var now = _time.GetLocalNow();
        return year > now.Year || (year == now.Year && month >= now.Month);
    }
}
