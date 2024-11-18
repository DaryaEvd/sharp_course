using Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class HRManager
{
    private readonly IMatchingStrategy _matchingStrategy;

    public HRManager(IMatchingStrategy matchingStrategy)
    {
        _matchingStrategy = matchingStrategy;
    }

    public List<Team> BuildTeams(List<Junior> juniors, List<TeamLead> teamLeads)
    {
        return _matchingStrategy.ExecuteMatchingAlgorithm(juniors, teamLeads);
    }
}