using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;

public interface IDataLoader
{
    List<Junior> LoadJuniors(string filePath);
    List<TeamLead> LoadTeamLeads(string filePath);
}