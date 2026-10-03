using ETicaret.Business.Concrete;

namespace ETicaret.Tests.Business;

public class FakePaymentServiceTests
{
    private readonly FakePaymentService _sut = new();

    [Theory]
    [InlineData("4111111111111111")]
    [InlineData("4111 1111 1111 1111")]
    [InlineData("5555555555554444")]
    public async Task PayAsync_ValidLuhnCard_Succeeds(string card)
    {
        var result = await _sut.PayAsync(card, 100m);
        Assert.True(result.Success);
    }

    [Theory]
    [InlineData("4111111111111112")]
    [InlineData("1234567812345678")]
    [InlineData("41111111")]
    [InlineData("")]
    public async Task PayAsync_InvalidCard_Fails(string card)
    {
        var result = await _sut.PayAsync(card, 100m);
        Assert.False(result.Success);
        Assert.Equal("Kart numarası geçersiz.", result.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public async Task PayAsync_NonPositiveAmount_Fails(decimal amount)
    {
        var result = await _sut.PayAsync("4111111111111111", amount);
        Assert.False(result.Success);
    }
}
