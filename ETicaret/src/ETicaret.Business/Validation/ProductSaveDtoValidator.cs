using ETicaret.Business.DTOs;
using ETicaret.DataAccess.UnitOfWork;
using FluentValidation;

namespace ETicaret.Business.Validation;

public class ProductSaveDtoValidator : AbstractValidator<ProductSaveDto>
{
    public const string UploadsPrefix = "/uploads/";

    public const string SeedImagesPrefix = "/img/products/";

    public ProductSaveDtoValidator(IUnitOfWork uow)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ürün adı zorunludur")
            .MaximumLength(200).WithMessage("Ürün adı en fazla 200 karakter olabilir");

        RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("Açıklama en fazla 2000 karakter olabilir");

        RuleFor(x => x.Specifications)
            .MaximumLength(2000).WithMessage("Özellikler en fazla 2000 karakter olabilir");

        RuleFor(x => x.Price)
            .InclusiveBetween(0.01m, 1_000_000m).WithMessage("Geçerli bir fiyat girin");

        RuleFor(x => x.Stock)
            .InclusiveBetween(0, 100_000).WithMessage("Stok 0 ile 100.000 arasında olmalı");

        RuleFor(x => x.ImageUrl)
            .MaximumLength(500).WithMessage("Görsel adresi en fazla 500 karakter olabilir")
            .Must(BeValidImageUrl).WithMessage("Geçerli bir görsel adresi girin (http/https)")
            .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl));

        RuleFor(x => x.CategoryId)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Kategori seçin")
            .MustAsync(async (id, _) => await uow.Categories.AnyAsync(c => c.Id == id && c.IsActive))
            .WithMessage("Kategori bulunamadı");
    }

    public static bool BeValidImageUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url)) return true;
        if (url.StartsWith(UploadsPrefix, StringComparison.Ordinal) || url.StartsWith(SeedImagesPrefix, StringComparison.Ordinal))
            return !url.Contains("..") && !url.Contains('\\');

        return Uri.TryCreate(url, UriKind.Absolute, out var uri)
               && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    }
}
