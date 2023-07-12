using FluentAssertions;
using TransactionVisualizer.Utility.Parsers.FileParsers;

namespace TransactionVisualizerTest.UtilityTest.Parsers.FileParsers;

public class FileParsersTest
{
    [Fact]
    public void Parse_WithCsvFile_ReturnsParsedData()
    {
        // Arrange
        var fileParsers = new TransactionVisualizer.Utility.Parsers.FileParsers.FileParsers();
        const string filePath =
            "E:\\RiderProjects\\Clone\\CodeStarPr\\TransactionVisualizerTest\\UtilityTest\\Parsers\\FileParsers\\MainData.csv";
        const FileType fileType = FileType.Csv;

        // Act
        var result = fileParsers.Parse<MainDataForTest>(filePath, fileType);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(3);
    }

    [Fact]
    public void Parse_WithJsonFile_ReturnsParsedData()
    {
        // Arrange
        var fileParsers = new TransactionVisualizer.Utility.Parsers.FileParsers.FileParsers();
        const string filePath =
            "E:\\RiderProjects\\Clone\\CodeStarPr\\TransactionVisualizerTest\\UtilityTest\\Parsers\\FileParsers\\MainData.json";
        const FileType fileType = FileType.Json;

        // Act
        var result = fileParsers.Parse<MainDataForTest>(filePath, fileType);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(3);
    }
}