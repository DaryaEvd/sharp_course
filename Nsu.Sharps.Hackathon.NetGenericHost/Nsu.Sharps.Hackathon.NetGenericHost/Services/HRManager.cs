using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class HRManager
{
    private readonly List<Junior> _juniors;
    private readonly List<TeamLead> _teamLeads;

    public HRManager(List<Junior> juniors, List<TeamLead> teamLeads)
    {
        _juniors = juniors;
        _teamLeads = teamLeads;
    }

    public List<Team> ExecuteMatchingAlgorithm()
    {
        var freeJuniors = new HashSet<int>(_juniors.Select(j => j.Id));
        var pairings = new Dictionary<int, int>();
        var teams = new List<Team>();

        while (freeJuniors.Count != 0)
        {
            var juniorId = freeJuniors.First();
            var junior = _juniors.FirstOrDefault(j => j.Id == juniorId);

            if (junior == null || junior.Wishlist == null || !junior.Wishlist.Any())
            {
                freeJuniors.Remove(juniorId);
                continue;
            }

            var preferredTeamLeadId = junior.Wishlist.First();

            if (!IsTeamLeadPaired(preferredTeamLeadId, pairings))
                FormPair(preferredTeamLeadId, juniorId, pairings, freeJuniors, teams);
            else
                HandleExistingPair(preferredTeamLeadId, juniorId, pairings, freeJuniors, teams);
        }

        return teams;
    }

    private bool IsTeamLeadPaired(int teamLeadId, Dictionary<int, int> pairings)
    {
        return pairings.ContainsKey(teamLeadId);
    }

    private void FormPair(int teamLeadId, int juniorId, Dictionary<int, int> pairings, HashSet<int> freeJuniors,
        List<Team> teams)
    {
        pairings[teamLeadId] = juniorId;
        freeJuniors.Remove(juniorId);

        var teamLead = _teamLeads.First(t => t.Id == teamLeadId);
        var junior = _juniors.First(j => j.Id == juniorId);
        teams.Add(new Team(teamLead, junior));
    }

    private bool DoesTeamLeadPreferNewJunior(TeamLead teamLead, int newJuniorId, int currentJuniorId)
    {
        var newJuniorPreference = teamLead.Wishlist.IndexOf(newJuniorId);
        var currentJuniorPreference = teamLead.Wishlist.IndexOf(currentJuniorId);

        return newJuniorPreference < currentJuniorPreference;
    }

    private void HandleExistingPair(int teamLeadId, int newJuniorId, Dictionary<int, int> pairings,
        HashSet<int> freeJuniors, List<Team> teams)
    {
        var currentJuniorId = pairings[teamLeadId];
        var teamLead = _teamLeads.First(t => t.Id == teamLeadId);

        if (DoesTeamLeadPreferNewJunior(teamLead, newJuniorId, currentJuniorId))
        {
            teams.RemoveAll(team => team.TeamLead.Id == teamLeadId && team.Junior.Id == currentJuniorId);

            pairings[teamLeadId] = newJuniorId;
            freeJuniors.Remove(newJuniorId);
            freeJuniors.Add(currentJuniorId);

            var newJunior = _juniors.First(j => j.Id == newJuniorId);
            teams.Add(new Team(teamLead, newJunior));
        }
        else
        {
            var newJunior = _juniors.FirstOrDefault(j => j.Id == newJuniorId);
            if (newJunior != null) newJunior.Wishlist.Remove(teamLeadId);
        }
    }
}