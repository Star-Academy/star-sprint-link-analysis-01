using FluentAssertions;
using TransactionVisualizer.Utility.Parsers.FileParser;

namespace TransactionVisualizerTest.UtilityTest.Parsers.FileParser;

public class JsonFileParserTest
{
    //TODO : Setup and mock Elastic data repo

    private const string Path = "E:\\RiderProjects\\Clone\\CodeStarPr\\TransactionVisualizerTest\\UtilityTest\\Parsers\\FileParser\\MainData.json";
    
    [Fact]
    public void Pars_ValidJsonFile_ReturnsParsedRecords()
    {
        // Arrange
        var expectedData = new List<MainDataForTest>
        {
            new MainDataForTest { Id = 1, Name = "John" },
            new MainDataForTest { Id = 2, Name = "Jane" },
            new MainDataForTest { Id = 3, Name = "Alice" }
        };

        var parser = new JsonFileParser<MainDataForTest>();

        // Act
        var parsedData = parser.Pars(Path);

        // Assert
        parsedData.Should().BeEquivalentTo(expectedData);
    }

    // TODO: If type object of excepted different with actual type of json file return List
    [Fact]
    public void Pars_InvalidJsonFile_ThrowsCsvHelperException()
    {
        // // Arrange
        // var parser = new JsonFileParser<MinorDataForTest>();
        //
        // // Act
        // var act = () => parser.Pars(Path);
        //
        // // Assert
        // act.Should().Throw<NullReferenceException>();
    }
    
    [Fact]
    public void Pars_InvalidJsonFilePath_ThrowsFileNotFoundException()
    {
        // Arrange
        var parser = new JsonFileParser<MinorDataForTest>();

        // Act
        Action act = () => parser.Pars("Path");

        // Assert
        act.Should().Throw<FileNotFoundException>();
    }
}