using FluentAssertions;
using TransactionVisualizer.Utility.Parsers.DateTimeParser;

namespace TransactionVisualizerTest.UtilityTest.Parsers.DateTimeParsers;

public class DateTimeParserTests
{
    [Fact]
    public void ParseExact_ShouldReturnCorrectDateTime_WhenInputIsInValidFormat()
    {
        // Arrange
        var input = "11/07/2023";

        // Act
        var result = DateTimeParser.ParseExact(input);

        // Assert
        result.Should().Be(new DateTime(2023, 7, 11));
    }

    [Fact]
    public void ParseExact_ShouldThrowFormatException_WhenInputIsNotInValidFormat()
    {
        // Arrange
        var input = "11-07-2023";

        // Act
        Action act = () => DateTimeParser.ParseExact(input);

        // Assert
        act.Should().ThrowExactly<FormatException>();
    }
}