using ETicaret.Business.DTOs;
using FluentValidation;

namespace ETicaret.Business.Validation;

public class ReviewSaveDtoValidator : AbstractValidator<ReviewSaveDto>
{
    public ReviewSaveDtoValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("1 ile 5 arasında puan ver");

        RuleFor(x => x.Comment)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Yorum zorunludur")
            .MinimumLength(10).WithMessage("Yorum en az 10 karakter olmalı")
            .MaximumLength(1000).WithMessage("Yorum en fazla 1000 karakter olabilir");
    }
}
