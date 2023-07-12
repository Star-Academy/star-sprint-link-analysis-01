using System.Globalization;

namespace TransactionVisualizer.Utility.Parsers.DateTimeParsers;

public static class DateTimeParser
{
    private static readonly string[] Formats = { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy", "MM/dd/yyyy" };


    // private const string Format = "yyyy/MM/dd";
    // private static readonly CultureInfo Culture = new("fa-IR");

    private static readonly CultureInfo Culture = new("en-US");


    public static DateTime ParseExact(string date)
    {
        Culture.DateTimeFormat.Calendar = new GregorianCalendar();
        return DateTime.ParseExact(date, Formats, Culture, DateTimeStyles.None);
    }
}