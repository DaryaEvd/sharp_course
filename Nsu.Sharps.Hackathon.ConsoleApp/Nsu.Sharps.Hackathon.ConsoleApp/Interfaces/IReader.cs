using Nsu.Sharps.Hackathon.ConsoleApp.Models;

namespace Nsu.Sharps.Hackathon.ConsoleApp.Interfaces;

public interface IReader
{
    // List<Participant> ReadFile(string pathToFile);
    
    
    List<Junior> ReadJuniors(string pathToFile);
    List<TeamLead> ReadTeamLeads(string pathToFile);
}
