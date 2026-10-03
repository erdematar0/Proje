namespace ETicaret.Business.Common;

public record InstallmentOption(int Months, decimal MonthlyAmount, decimal Total, decimal InterestRate)
{
    public bool IsInterestFree => InterestRate == 0;
}

public static class Installments
{
    public const decimal MinMonthlyAmount = 100m;

    private static readonly (int Months, decimal Rate)[] Plans = [(2, 0m), (3, 0m), (6, 0.05m), (9, 0.08m), (12, 0.12m)];

    public static List<InstallmentOption> For(decimal price)
    {
        var options = new List<InstallmentOption> { new(1, price, price, 0m) };
        foreach (var (months, rate) in Plans)
        {
            var total = Math.Round(price * (1 + rate), 2);
            var monthly = Math.Round(total / months, 2);
            if (monthly >= MinMonthlyAmount) options.Add(new(months, monthly, total, rate));
        }
        return options;
    }
}
