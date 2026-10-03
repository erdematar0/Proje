using ETicaret.Business.DTOs;
using FluentValidation;

namespace ETicaret.Business.Validation;

public class AddressSaveDtoValidator : AbstractValidator<AddressSaveDto>
{
    public AddressSaveDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Adres başlığı zorunludur (örn. Ev, İş)")
            .MaximumLength(50).WithMessage("Başlık en fazla 50 karakter olabilir");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Ad soyad zorunludur")
            .MaximumLength(150).WithMessage("Ad soyad en fazla 150 karakter olabilir");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Telefon zorunludur")
            .PhoneNumber();

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Şehir zorunludur")
            .MaximumLength(50).WithMessage("Şehir en fazla 50 karakter olabilir");

        RuleFor(x => x.District)
            .MaximumLength(50).WithMessage("İlçe en fazla 50 karakter olabilir");

        RuleFor(x => x.AddressLine)
            .NotEmpty().WithMessage("Adres zorunludur")
            .MaximumLength(500).WithMessage("Adres en fazla 500 karakter olabilir");
    }
}
