using Nsu.Sharps.Hackathon.ConsoleApp.Models;

namespace Nsu.Sharps.Hackathon.ConsoleApp.Services;

public class HRManager
{
    private readonly List<Junior> _juniors;
    private readonly List<TeamLead> _teamLeads;
    private Dictionary<int, int> _pairings;

    public HRManager(List<Junior> juniors, List<TeamLead> teamLeads)
    {
        _juniors = juniors;
        _teamLeads = teamLeads;
        _pairings = new Dictionary<int, int>();
    }

    public void BuildTeams()
    {
        _pairings = ExecuteMatchingAlgorithm();
    }

    public Dictionary<int, int> GetPairings()
    {
        return _pairings;
    }

    private Dictionary<int, int> ExecuteMatchingAlgorithm()
    {
        var freeJuniors = new HashSet<int>(_juniors.Select(j => j.Id));
        var pairings = new Dictionary<int, int>();
        var juniorPairings = new Dictionary<int, int>();

        while (freeJuniors.Count != 0)
        {
            var juniorId = freeJuniors.First();
            var junior = _juniors.First(j => j.Id == juniorId);
            var preferredTeamLeadId = junior.Wishlist.First();

            if (!pairings.ContainsKey(preferredTeamLeadId))
            {
                pairings[preferredTeamLeadId] = juniorId;
                juniorPairings[juniorId] = preferredTeamLeadId;
                freeJuniors.Remove(juniorId);
            }
            else
            {
                var currentJuniorId = pairings[preferredTeamLeadId];
                var teamLead = _teamLeads.First(t => t.Id == preferredTeamLeadId);
                if (teamLead.Wishlist.IndexOf(juniorId) < teamLead.Wishlist.IndexOf(currentJuniorId))
                {
                    pairings[preferredTeamLeadId] = juniorId;
                    juniorPairings[juniorId] = preferredTeamLeadId;
                    freeJuniors.Add(currentJuniorId);
                    freeJuniors.Remove(juniorId);
                }
                else
                {
                    junior.Wishlist.Remove(preferredTeamLeadId);
                }
            }
        }

        return juniorPairings;
    }
}