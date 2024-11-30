using Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class FileDataLoader : IDataLoader
{
    private readonly IReader _reader;

    public FileDataLoader(IReader reader)
    {
        _reader = reader;
    }

    public List<Junior> LoadJuniors(string filePath)
    {
        return _reader.ReadFile(filePath).OfType<Junior>().ToList();
    }

    public List<TeamLead> LoadTeamLeads(string filePath)
    {
        return _reader.ReadFile(filePath).OfType<TeamLead>().ToList();
    }
}