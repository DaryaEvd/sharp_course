using Nsu.Sharps.Hackathon.NetGenericHost.Services;

namespace Nsu.Sharps.Hackathon.Tests;

public class FileDataLoaderTests
{
    private readonly ReadCsvFile _reader;

    public FileDataLoaderTests()
    {
        _reader = new ReadCsvFile();
    }

    [Fact]
    public void WhenFileExtensionIsInvalid()
    {
        var validDataContent = "Id;Name\n1;Юдин Адам\n2;Яшина Яна\n";

        using var reader = new StringReader(validDataContent);

        Assert.Throws<InvalidDataException>(() => _reader.ReadFromStream(reader, "juinior.txt"));
    }

    [Fact]
    public void TestEmptyFile()
    {
        var emptyFileContent = "";
        using var reader = new StringReader(emptyFileContent);

        Assert.Throws<InvalidDataException>(() => _reader.ReadFromStream(reader, "filename.csv"));
    }

    [Fact]
    public void TestInvalidHeader()
    {
        var invalidHeaderContent = "idddd,name\n1;John Doe\n";
        using var reader = new StringReader(invalidHeaderContent);

        Assert.Throws<InvalidDataException>(() => _reader.ReadFromStream(reader, "filename.csv"));
    }

    [Fact]
    public void TestInvalidData()
    {
        var invalidDataContent = "Id;Name\nabc;John Doe\n";
        using var reader = new StringReader(invalidDataContent);

        Assert.Throws<InvalidDataException>(() => _reader.ReadFromStream(reader, "filename.csv"));
    }
}