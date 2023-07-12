using FluentAssertions;
using TransactionVisualizer.Exception;
using TransactionVisualizer.Utility.Validator;

namespace TransactionVisualizerTest.UtilityTest.ValidatorTest;

public class ValidatorTests
{
    [Fact]
    public void NullValidation_ThrowsArgumentNullException()
    {
        // Arrange
        object? data = null;

        // Act
        Action act = () => Validator.NullValidation(data);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ListValidation_NullList_ThrowsArgumentNullException()
    {
        // Arrange
        List<object>? list = null;

        // Act
        Action act = () => Validator.ListValidation(list);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ListValidation_EmptyList_ThrowsEmptyListException()
    {
        // Arrange
        List<object> list = new List<object>();

        // Act
        Action act = () => Validator.ListValidation(list);

        // Assert
        act.Should().Throw<EmptyListException>();
    }

    [Fact]
    public void ListValidation_ValidList_DoesNotThrowException()
    {
        // Arrange
        List<object> list = new List<object> { 1, 2, 3 };

        // Act
        Action act = () => Validator.ListValidation(list);

        // Assert
        act.Should().NotThrow();
    }
}