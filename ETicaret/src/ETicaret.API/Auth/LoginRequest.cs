using FluentValidation;

namespace ETicaret.API.Auth;

public record LoginRequest(string Email, string Password);

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("E-posta zorunludur")
            .EmailAddress().WithMessage("Geçerli bir e-posta girin");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre zorunludur");
    }
}
