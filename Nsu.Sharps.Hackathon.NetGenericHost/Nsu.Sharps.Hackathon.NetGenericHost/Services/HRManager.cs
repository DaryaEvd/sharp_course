using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class HRManager
{
    private readonly HRDirector _hrDirector;
    private readonly List<Junior> _juniors;
    private readonly List<TeamLead> _teamLeads;
    private readonly WishlistGenerator _wishlistGenerator;

    public HRManager(List<Junior> juniors, List<TeamLead> teamLeads, WishlistGenerator wishlistGenerator,
        HRDirector hrDirector)
    {
        _wishlistGenerator = wishlistGenerator;
        _hrDirector = hrDirector;

        _juniors = juniors;
        _teamLeads = teamLeads;
    }

    public Dictionary<int, int> ExecuteMatchingAlgorithm()
    {
        var freeJuniors = new HashSet<int>(_juniors.Select(j => j.Id));
        var pairings = new Dictionary<int, int>();
        var juniorPairings = new Dictionary<int, int>();

        while (freeJuniors.Count != 0)
        {
            var juniorId = freeJuniors.First();
            var junior = _juniors.First(j => j.Id == juniorId);
            var preferredTeamLeadId = junior.Wishlist.First();

            if (!IsTeamLeadPaired(preferredTeamLeadId, pairings))
                FormPair(preferredTeamLeadId, juniorId, pairings, juniorPairings, freeJuniors);
            else
                HandleExistingPair(preferredTeamLeadId, juniorId, pairings, juniorPairings, freeJuniors);
        }

        return juniorPairings;
    }

    private bool IsTeamLeadPaired(int teamLeadId, Dictionary<int, int> pairings)
    {
        return pairings.ContainsKey(teamLeadId);
    }

    private void FormPair(int teamLeadId, int juniorId, Dictionary<int, int> pairings,
        Dictionary<int, int> juniorPairings, HashSet<int> freeJuniors)
    {
        pairings[teamLeadId] = juniorId;
        juniorPairings[juniorId] = teamLeadId;
        freeJuniors.Remove(juniorId);
    }

    private bool DoesTeamLeadPreferNewJunior(TeamLead teamLead, int newJuniorId, int currentJuniorId)
    {
        var newJuniorPreference = teamLead.Wishlist.IndexOf(newJuniorId);
        var currentJuniorPreference = teamLead.Wishlist.IndexOf(currentJuniorId);

        return newJuniorPreference < currentJuniorPreference;
    }

    private void HandleExistingPair(int teamLeadId, int newJuniorId, Dictionary<int, int> pairings,
        Dictionary<int, int> juniorPairings, HashSet<int> freeJuniors)
    {
        var currentJuniorId = pairings[teamLeadId];
        var teamLead = _teamLeads.First(t => t.Id == teamLeadId);

        if (!DoesTeamLeadPreferNewJunior(teamLead, newJuniorId, currentJuniorId))
        {
            var newJunior = _juniors.FirstOrDefault(j => j.Id == newJuniorId);
            if (newJunior != null) newJunior.Wishlist.Remove(teamLeadId);
        }

        pairings[teamLeadId] = newJuniorId;
        juniorPairings.Remove(currentJuniorId);
        juniorPairings[newJuniorId] = teamLeadId;
        freeJuniors.Add(currentJuniorId);
        freeJuniors.Remove(newJuniorId);
    }
}