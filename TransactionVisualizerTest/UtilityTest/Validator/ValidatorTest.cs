using FluentAssertions;
using TransactionVisualizer.Exception;

namespace TransactionVisualizerTest.UtilityTest.Validator;

public class ValidatorTest
{
    [Fact]
    public void NullValidation_ThrowsArgumentNullException()
    {
        // Arrange
        object? data = null;

        // Act
        var act = () => TransactionVisualizer.Utility.Validator.Validator.NullValidation(data);

        // Assert
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("data");
    }

    [Fact]
    public void ListValidation_WithNullList_ThrowsArgumentNullException()
    {
        // Arrange
        List<string>? list = null;

        // Act
        var act = () => TransactionVisualizer.Utility.Validator.Validator.ListValidation(list);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ListValidation_WithEmptyList_ThrowsEmptyListException()
    {
        // Arrange
        var list = new List<int>();

        // Act
        var act = () => TransactionVisualizer.Utility.Validator.Validator.ListValidation(list, "myList");

        // Assert
        act.Should().Throw<EmptyListException>();
    }

    [Fact]
    public void ListValidation_WithNonEmptyList_DoesNotThrowException()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3 };

        // Act
        var act = () => TransactionVisualizer.Utility.Validator.Validator.ListValidation(list);

        // Assert
        act.Should().NotThrow();
    }
}
