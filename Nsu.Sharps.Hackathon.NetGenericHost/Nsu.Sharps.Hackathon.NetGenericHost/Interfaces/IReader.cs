using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;

public interface IReader
{
    // List<Participant> ReadFile(string pathToFile);


    List<Junior> ReadJuniors(string pathToFile);
    List<TeamLead> ReadTeamLeads(string pathToFile);
}