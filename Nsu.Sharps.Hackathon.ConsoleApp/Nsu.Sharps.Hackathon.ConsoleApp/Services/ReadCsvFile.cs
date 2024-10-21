using Nsu.Sharps.Hackathon.ConsoleApp.Interfaces;
using Nsu.Sharps.Hackathon.ConsoleApp.Models;

namespace Nsu.Sharps.Hackathon.ConsoleApp.Services;

public class ReadCsvFile : IReader
{
    public List<Junior> ReadJuniors(string pathToFile)
    {
        var juniors = new List<Junior>();
        using var reader = new StreamReader(pathToFile);
        var line = reader.ReadLine();
        if (line == null)
        {
            throw new Exception($"File {pathToFile} doesn't exist");
        }

        while ((line = reader.ReadLine()) != null)
        {
            var columns = line.Split(';');
            if (columns.Length < 2) continue;
            if (!int.TryParse(columns[0], out var id)) continue;
            var name = columns[1];
            juniors.Add(new Junior(id, name));
        }

        return juniors;
    }

    public List<TeamLead> ReadTeamLeads(string pathToFile)
    {
        var teamLeads = new List<TeamLead>();
        using var reader = new StreamReader(pathToFile);
        var line = reader.ReadLine();
        if (line == null)
        {
            throw new Exception($"File {pathToFile} doesn't exist");
        }

        while ((line = reader.ReadLine()) != null)
        {
            var columns = line.Split(';');
            if (columns.Length < 2) continue;
            if (!int.TryParse(columns[0], out var id)) continue;
            var name = columns[1];
            teamLeads.Add(new TeamLead(id, name));
        }

        return teamLeads;
    }
}