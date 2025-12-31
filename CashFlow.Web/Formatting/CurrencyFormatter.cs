using System.Globalization;

namespace CashFlow.Web.Formatting;

public static class CurrencyFormatter
{
    private static readonly CultureInfo PhpCulture =
        new("en-PH");

    public static string ToPhp(decimal value)
        => value.ToString("C", PhpCulture);
}
