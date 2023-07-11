using CsvHelper;
using FluentAssertions;
using TransactionVisualizer.Utility.Parsers.FileParser;
using TransactionVisualizerTest.UtilityTest.Parsers.FileParsers;

namespace TransactionVisualizerTest.UtilityTest.Parsers.FileParser;

public class CsvFileParserTest
{
    //TODO : Setup and mock Elastic data repo

    private const string Path = "E:\\RiderProjects\\Clone\\CodeStarPr\\TransactionVisualizerTest\\UtilityTest\\Parsers\\FileParser\\MainData.csv";
    
    [Fact]
    public void Pars_ValidCsvFile_ReturnsParsedRecords()
    {
        // Arrange
        var expectedData = new List<MainDataForTest>
        {
            new MainDataForTest { Id = 1, Name = "John" },
            new MainDataForTest { Id = 2, Name = "Jane" },
            new MainDataForTest { Id = 3, Name = "Alice" }
        };

        var parser = new CsvFileParser<MainDataForTest>();

        // Act
        var parsedData = parser.Pars(Path);

        // Assert
        parsedData.Should().BeEquivalentTo(expectedData);
    }

    [Fact]
    public void Pars_InvalidCsvFile_ThrowsCsvHelperException()
    {
        // Arrange
        var parser = new CsvFileParser<MinorDataForTest>();

        // Act
        Action act = () => parser.Pars(Path);

        // Assert
        act.Should().Throw<CsvHelperException>();
    }
    
    [Fact]
    public void Pars_InvalidCsvFilePath_ThrowsFileNotFoundException()
    {
        // Arrange
        var parser = new CsvFileParser<MinorDataForTest>();

        // Act
        Action act = () => parser.Pars("Path");

        // Assert
        act.Should().Throw<FileNotFoundException>();
    }
}