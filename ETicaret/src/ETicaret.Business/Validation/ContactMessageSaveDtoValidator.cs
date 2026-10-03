using ETicaret.Business.DTOs;
using FluentValidation;

namespace ETicaret.Business.Validation;

public class ContactMessageSaveDtoValidator : AbstractValidator<ContactMessageSaveDto>
{
    public ContactMessageSaveDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad soyad zorunludur")
            .MaximumLength(100).WithMessage("Ad soyad en fazla 100 karakter olabilir");

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("E-posta zorunludur")
            .MaximumLength(256).WithMessage("E-posta en fazla 256 karakter olabilir")
            .EmailAddress().WithMessage("Geçerli bir e-posta girin");

        RuleFor(x => x.Phone)
            .PhoneNumber()
            .When(x => !string.IsNullOrWhiteSpace(x.Phone));

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Konu seçin")
            .MaximumLength(150).WithMessage("Konu en fazla 150 karakter olabilir");

        RuleFor(x => x.Message)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Mesaj zorunludur")
            .MinimumLength(10).WithMessage("Mesaj en az 10 karakter olmalı")
            .MaximumLength(2000).WithMessage("Mesaj en fazla 2000 karakter olabilir");
    }
}
