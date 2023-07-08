using System.Globalization;

namespace TransactionVisualizer.Utility.Parsers.DateTimeParser;

public static class DateTimeParser
{
    private const string Format = "yyyy/MM/dd";
    private static readonly CultureInfo Culture = new("fa-IR");

    public static DateTime ParseExact(string date)
    {
        return DateTime.ParseExact(date, Format, Culture);
    }
}