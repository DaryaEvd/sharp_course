using Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class ReadCsvFile : IReader
{
    public List<Participant> ReadFile(string pathToFile)
    {
        if (!File.Exists(pathToFile))
            throw new FileNotFoundException($"File {pathToFile} doesn't exist");

        using var reader = new StreamReader(pathToFile);
        return ReadFromStream(reader, Path.GetFileName(pathToFile));
    }

    public List<Participant> ReadFromStream(TextReader reader, string fileName)
    {
        var extension = Path.GetExtension(fileName);
        if (!string.Equals(extension, ".csv", StringComparison.OrdinalIgnoreCase))
            throw new InvalidDataException($"Invalid file extension. Expected '.csv', got '{extension}'");

        var header = reader.ReadLine();
        if (header != "Id;Name")
            throw new InvalidDataException("File does not have a valid header. Expected 'Id;Name'");

        if (!fileName.StartsWith("junior", StringComparison.OrdinalIgnoreCase) &&
            !fileName.StartsWith("teamlead", StringComparison.OrdinalIgnoreCase))
            throw new InvalidDataException("File should start with 'junior' or 'teamlead'");

        var participants = new List<Participant>();
        var uniqueIds = new HashSet<int>();

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            var columns = line.Split(';');

            if (columns.Length != 2 || !int.TryParse(columns[0], out var id))
                throw new InvalidDataException($"Invalid data format in line: {line}");
            
            if (!uniqueIds.Add(id))
                throw new InvalidDataException($"Duplicate Id found: {id} in line: {line}");

            var name = columns[1];
            if (fileName.StartsWith("junior", StringComparison.OrdinalIgnoreCase))
                participants.Add(new Junior(id, name));
            else
                participants.Add(new TeamLead(id, name));
        }

        return participants;
    }
}