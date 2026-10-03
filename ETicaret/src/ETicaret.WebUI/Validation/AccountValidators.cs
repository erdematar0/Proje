using ETicaret.Business.Validation;
using ETicaret.WebUI.Models;
using FluentValidation;

namespace ETicaret.WebUI.Validation;

public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
{
    public RegisterViewModelValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad zorunludur")
            .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad zorunludur")
            .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir");
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("E-posta zorunludur")
            .EmailAddress().WithMessage("Geçerli bir e-posta girin");
        RuleFor(x => x.Password).StrongPassword();
        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor");
    }
}

public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
{
    public LoginViewModelValidator()
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("E-posta zorunludur")
            .EmailAddress().WithMessage("Geçerli bir e-posta girin");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre zorunludur");
    }
}

public class ProfileViewModelValidator : AbstractValidator<ProfileViewModel>
{
    public ProfileViewModelValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad zorunludur")
            .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad zorunludur")
            .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir");
        RuleFor(x => x.PhoneNumber)
            .PhoneNumber()
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
    }
}

public class AdminUserEditViewModelValidator : AbstractValidator<AdminUserEditViewModel>
{
    public AdminUserEditViewModelValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad zorunludur")
            .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad zorunludur")
            .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir");
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("E-posta zorunludur")
            .EmailAddress().WithMessage("Geçerli bir e-posta girin");
        RuleFor(x => x.PhoneNumber)
            .PhoneNumber()
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
        RuleFor(x => x.NewPassword!)
            .StrongPassword()
            .When(x => !string.IsNullOrEmpty(x.NewPassword));
    }
}

public class ChangePasswordViewModelValidator : AbstractValidator<ChangePasswordViewModel>
{
    public ChangePasswordViewModelValidator()
    {
        RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Mevcut şifre zorunludur");
        RuleFor(x => x.NewPassword)
            .StrongPassword()
            .NotEqual(x => x.CurrentPassword).WithMessage("Yeni şifre mevcut şifreyle aynı olamaz");
        RuleFor(x => x.ConfirmNewPassword)
            .Equal(x => x.NewPassword).WithMessage("Şifreler eşleşmiyor");
    }
}
