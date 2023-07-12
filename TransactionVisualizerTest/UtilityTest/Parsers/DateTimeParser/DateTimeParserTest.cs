using FluentAssertions;

namespace TransactionVisualizerTest.UtilityTest.Parsers.DateTimeParser;

public class DateTimeParserTest
{
    [Fact]
    public void ParseExact_ValidDate_ReturnsParsedDateTime()
    {
        // Arrange
        const string date = "1400/01/01";
        var expectedDateTime = new DateTime(2021, 3, 21);

        // Act
        var parsedDateTime = TransactionVisualizer.Utility.Parsers.DateTimeParser.DateTimeParser.ParseExact(date);

        // Assert
        parsedDateTime.Should().Be(expectedDateTime);
    }

    [Fact]
    public void ParseExact_InvalidDate_ThrowsFormatException()
    {
        // Arrange
        const string date = "InvalidDate";

        // Act
        Action act = () => TransactionVisualizer.Utility.Parsers.DateTimeParser.DateTimeParser.ParseExact(date);

        // Assert
        act.Should().Throw<FormatException>();
    }
}
