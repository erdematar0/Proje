using ETicaret.Business.Common;

namespace ETicaret.Tests.Business;

public class ProductHelpersTests
{
    [Fact]
    public void Installments_1000TL_ListsPlansAboveMinimumMonthly()
    {
        var options = Installments.For(1000m);

        Assert.Equal(new[] { 1, 2, 3, 6, 9 }, options.Select(o => o.Months));
        Assert.Equal((333.33m, 1000m, true), (options[2].MonthlyAmount, options[2].Total, options[2].IsInterestFree));
        Assert.Equal((175m, 1050m, false), (options[3].MonthlyAmount, options[3].Total, options[3].IsInterestFree));
    }

    [Fact]
    public void Installments_CheapProduct_OnlySinglePayment()
        => Assert.Equal(1, Assert.Single(Installments.For(150m)).Months);

    [Fact]
    public void ProductSpecs_ParsesLines_SplittingOnFirstColon()
    {
        var specs = ProductSpecs.Parse("Pil ömrü: 30 saat\r\n\r\n Oran : 16:9 \nSu geçirmez");

        Assert.Equal(new[]
        {
            new ProductSpec("Pil ömrü", "30 saat"),
            new ProductSpec("Oran", "16:9"),
            new ProductSpec("", "Su geçirmez")
        }, specs);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("   ")]
    public void ProductSpecs_Empty_ReturnsEmptyList(string? text) => Assert.Empty(ProductSpecs.Parse(text));
}
