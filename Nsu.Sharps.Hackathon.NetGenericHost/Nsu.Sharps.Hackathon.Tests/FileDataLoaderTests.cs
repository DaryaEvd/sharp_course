using Nsu.Sharps.Hackathon.NetGenericHost.Services;

namespace Nsu.Sharps.Hackathon.Tests;

public class FileDataLoaderTests
{
    [Fact]
    public void FileNotFoundExceptionTest()
    {
        var reader = new ReadCsvFile();

        Assert.Throws<FileNotFoundException>(() => reader.ReadFile("Participants/FileDoesntExist.csv"));
    }

    [Theory]
    [InlineData("Invalid,Header\n1;Participant One")]
    [InlineData("Invalid;Header\n1;Participant One")]
    [InlineData(";Header\n1;Participant One")]
    [InlineData(" Header\n1;Participant One")]
    [InlineData("\t\n1;Participant One")]
    public void InvalidHeaderTest(string csvContent)
    {
        var reader = new ReadCsvFile();
        var fileName = "junior_file.csv";

        using var stringReader = new StringReader(csvContent);

        Assert.Throws<InvalidDataException>(() => reader.ReadFromStream(stringReader, fileName));
    }

    [Fact]
    public void IncorrectFileName()
    {
        var reader = new ReadCsvFile();
        var invalidFileName = "wrong_file_name.csv";

        using var stringReader = new StringReader("Id;Name\n1;Participant One");

        Assert.Throws<InvalidDataException>(() => reader.ReadFromStream(stringReader, invalidFileName));
    }

    [Theory]
    [InlineData("Id;Name\nInvalidData")]
    [InlineData("Id;Name\nInvalid,Data")]
    [InlineData("Id;Name\nInvalid Data")]
    [InlineData("Id;Name\n\t\n")]
    [InlineData("Id;Name\nNotNumber;Participant One")]
    [InlineData("Id;Name\n;Participant One")]
    public void InvalidDataInFileTest(string csvContent)
    {
        var reader = new ReadCsvFile();
        var fileName = "teamlead_file.csv";

        using var stringReader = new StringReader(csvContent);

        Assert.Throws<InvalidDataException>(() => reader.ReadFromStream(stringReader, fileName));
    }
    
    [Fact]
    public void  DuplicateIdsFoundTest()
    { 
        var csvContent = "Id;Name\n11;Name11\n11;Name12";
        using var reader = new StringReader(csvContent);
        var readCsvFile = new ReadCsvFile();
 
        var exception = Assert.Throws<InvalidDataException>(() =>
            readCsvFile.ReadFromStream(reader, "junior.csv"));

        Assert.Equal("Duplicate Id found: 11 in line: 11;Name12", exception.Message);
    }

    [Fact]
    public void  DuplicateNamesWithUniqueIdsTest()
    { 
        var csvContent = "Id;Name\n11;NameName\n12;NameName";  
        using var reader = new StringReader(csvContent);
        var readCsvFile = new ReadCsvFile();
 
        var participants = readCsvFile.ReadFromStream(reader, "junior.csv");
 
        Assert.Equal(2, participants.Count);
        Assert.Contains(participants, p => p.Id == 11 && p.Name == "NameName");
        Assert.Contains(participants, p => p.Id == 12 && p.Name == "NameName");
    }
}