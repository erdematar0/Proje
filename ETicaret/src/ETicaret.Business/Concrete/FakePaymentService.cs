using ETicaret.Business.Abstract;
using ETicaret.Business.Common;

namespace ETicaret.Business.Concrete;

public class FakePaymentService : IPaymentService
{
    public Task<ServiceResult> PayAsync(string cardNumber, decimal amount)
    {
        var digits = new string(cardNumber.Where(char.IsDigit).ToArray());
        if (digits.Length < 15 || !IsLuhnValid(digits))
            return Task.FromResult(ServiceResult.Fail("Kart numarası geçersiz."));
        if (amount <= 0)
            return Task.FromResult(ServiceResult.Fail("Tutar geçersiz."));

        return Task.FromResult(ServiceResult.Ok("Ödeme onaylandı."));
    }

    private static bool IsLuhnValid(string digits)
    {
        int sum = 0; bool dbl = false;
        for (int i = digits.Length - 1; i >= 0; i--)
        {
            int d = digits[i] - '0';
            if (dbl) { d *= 2; if (d > 9) d -= 9; }
            sum += d; dbl = !dbl;
        }
        return sum % 10 == 0;
    }
}
