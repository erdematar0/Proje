using ETicaret.Business.DTOs;
using ETicaret.Business.Validation;

namespace ETicaret.Tests.Validation;

public class ProductSaveDtoValidatorTests
{
    private readonly ProductSaveDtoValidator _sut = new(TestDb.Uow(TestDb.Create()));

    private static ProductSaveDto Valid() => new()
    {
        Name = "Kulaklık", Price = 10m, Stock = 1, CategoryId = 1,
        ImageUrl = "https://picsum.photos/seed/x/600/450"
    };

    private async Task<string[]> ErrorsFor(ProductSaveDto dto) =>
        (await _sut.ValidateAsync(dto)).Errors.Select(e => e.PropertyName).ToArray();

    [Fact]
    public async Task ValidProduct_Passes() => Assert.Empty(await ErrorsFor(Valid()));

    [Fact]
    public async Task EmptyName_Fails()
    {
        var dto = Valid(); dto.Name = " ";
        Assert.Contains(nameof(ProductSaveDto.Name), await ErrorsFor(dto));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(1_000_001)]
    public async Task OutOfRangePrice_Fails(decimal price)
    {
        var dto = Valid(); dto.Price = price;
        Assert.Contains(nameof(ProductSaveDto.Price), await ErrorsFor(dto));
    }

    [Fact]
    public async Task NegativeStock_Fails()
    {
        var dto = Valid(); dto.Stock = -1;
        Assert.Contains(nameof(ProductSaveDto.Stock), await ErrorsFor(dto));
    }

    [Theory]
    [InlineData(0, "Kategori seçin")]
    [InlineData(999, "Kategori bulunamadı")]
    public async Task InvalidCategory_Fails(int categoryId, string message)
    {
        var dto = Valid(); dto.CategoryId = categoryId;

        var error = Assert.Single((await _sut.ValidateAsync(dto)).Errors);

        Assert.Equal(message, error.ErrorMessage);
    }

    [Theory]
    [InlineData("https://cdn.site.com/a.jpg", true)]
    [InlineData("http://site.com/a.png", true)]
    [InlineData("/uploads/products/abc.webp", true)]
    [InlineData("/img/products/kulaklik.jpg", true)]
    [InlineData("/img/products/../../appsettings.json", false)]
    [InlineData(null, true)]
    [InlineData("javascript:alert(1)", false)]
    [InlineData("data:image/png;base64,AAAA", false)]
    [InlineData("ftp://site.com/a.jpg", false)]
    [InlineData("/uploads/../appsettings.json", false)]
    [InlineData("resim.jpg", false)]
    public void BeValidImageUrl(string? url, bool expected)
        => Assert.Equal(expected, ProductSaveDtoValidator.BeValidImageUrl(url));
}

public class CategorySaveDtoValidatorTests
{
    private readonly CategorySaveDtoValidator _sut = new();

    [Fact]
    public void EmptyName_Fails()
    {
        var result = _sut.Validate(new CategorySaveDto { Name = "" });
        Assert.Equal("Kategori adı zorunludur", Assert.Single(result.Errors).ErrorMessage);
    }

    [Fact]
    public void TooLongName_Fails()
        => Assert.False(_sut.Validate(new CategorySaveDto { Name = new string('a', 101) }).IsValid);
}

public class ReviewSaveDtoValidatorTests
{
    private readonly ReviewSaveDtoValidator _sut = new();

    [Theory]
    [InlineData(0, "Güzel bir ürün, memnunum.", "1 ile 5 arasında puan ver")]
    [InlineData(6, "Güzel bir ürün, memnunum.", "1 ile 5 arasında puan ver")]
    [InlineData(5, "", "Yorum zorunludur")]
    [InlineData(5, "kısa", "Yorum en az 10 karakter olmalı")]
    public void Invalid_FailsWithOneMessage(int rating, string comment, string expected)
    {
        var result = _sut.Validate(new ReviewSaveDto { Rating = rating, Comment = comment });
        Assert.Equal(expected, Assert.Single(result.Errors).ErrorMessage);
    }

    [Fact]
    public void Valid_Passes()
        => Assert.True(_sut.Validate(new ReviewSaveDto { Rating = 4, Comment = "Güzel bir ürün, memnunum." }).IsValid);
}

public class ContactMessageSaveDtoValidatorTests
{
    private readonly ContactMessageSaveDtoValidator _sut = new();

    private static ContactMessageSaveDto Valid() => new()
    {
        Name = "Ali Yılmaz", Email = "ali@test.com", Subject = "Sipariş", Message = "Siparişim nerede acaba?"
    };

    [Fact]
    public void ValidMessage_Passes() => Assert.True(_sut.Validate(Valid()).IsValid);

    [Fact]
    public void PhoneIsOptional_ButMustBeValidIfGiven()
    {
        var dto = Valid(); dto.Phone = "";
        Assert.True(_sut.Validate(dto).IsValid);
        dto.Phone = "abc";
        Assert.False(_sut.Validate(dto).IsValid);
    }

    [Theory]
    [InlineData("", "Mesaj zorunludur")]
    [InlineData("kısa", "Mesaj en az 10 karakter olmalı")]
    public void ShortOrEmptyMessage_FailsWithOneMessage(string message, string expected)
    {
        var dto = Valid(); dto.Message = message;
        Assert.Equal(expected, Assert.Single(_sut.Validate(dto).Errors).ErrorMessage);
    }

    [Fact]
    public void EmptyEmail_ShowsOnlyRequiredMessage()
    {
        var dto = Valid(); dto.Email = "";
        Assert.Equal("E-posta zorunludur", Assert.Single(_sut.Validate(dto).Errors).ErrorMessage);
    }
}

public class AddressSaveDtoValidatorTests
{
    private readonly AddressSaveDtoValidator _sut = new();

    private static AddressSaveDto Valid() => new()
    {
        Title = "Ev", FullName = "Ali Yılmaz", Phone = "0555 111 22 33", City = "İzmir", AddressLine = "Cemal Gürsel Cad. No:5"
    };

    [Fact]
    public void ValidAddress_Passes() => Assert.True(_sut.Validate(Valid()).IsValid);

    [Theory]
    [InlineData("0555 111 22 33")]
    [InlineData("+90 (555) 111-22-33")]
    [InlineData("05551112233")]
    public void ValidPhones_Pass(string phone)
    {
        var dto = Valid(); dto.Phone = phone;
        Assert.True(_sut.Validate(dto).IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("12345")]
    [InlineData("telefonum yok")]
    public void InvalidPhones_Fail(string phone)
    {
        var dto = Valid(); dto.Phone = phone;
        Assert.Contains(_sut.Validate(dto).Errors, e => e.PropertyName == nameof(AddressSaveDto.Phone));
    }

    [Fact]
    public void MissingRequiredFields_Fail()
    {
        var result = _sut.Validate(new AddressSaveDto());

        var props = result.Errors.Select(e => e.PropertyName).Distinct().ToArray();
        Assert.Equal(new[] { "Title", "FullName", "Phone", "City", "AddressLine" }, props);
    }
}
