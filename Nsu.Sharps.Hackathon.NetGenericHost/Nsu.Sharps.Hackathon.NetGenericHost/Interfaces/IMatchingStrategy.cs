using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;

public interface IMatchingStrategy
{
    List<Team> ExecuteMatchingAlgorithm(List<Junior> juniors, List<TeamLead> teamLeads);
}