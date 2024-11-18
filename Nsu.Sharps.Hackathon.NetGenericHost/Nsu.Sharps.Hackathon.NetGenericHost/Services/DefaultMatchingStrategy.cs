using Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class DefaultMatchingStrategy : IMatchingStrategy
{
    public List<Team> ExecuteMatchingAlgorithm(List<Junior> juniors, List<TeamLead> teamLeads)
    {
        var freeJuniors = new HashSet<int>(juniors.Select(j => j.Id));
        var pairings = new Dictionary<int, int>();
        var teams = new List<Team>();

        while (freeJuniors.Count != 0)
        {
            var juniorId = freeJuniors.First();
            var junior = juniors.FirstOrDefault(j => j.Id == juniorId);

            if (junior == null || junior.Wishlist == null || !junior.Wishlist.Any())
            {
                freeJuniors.Remove(juniorId);
                continue;
            }

            var preferredTeamLeadId = junior.Wishlist.First();

            if (!IsTeamLeadPaired(preferredTeamLeadId, pairings))
                FormPair(preferredTeamLeadId, juniorId, pairings, freeJuniors, teams, juniors, teamLeads);
            else
                HandleExistingPair(preferredTeamLeadId, juniorId, pairings, freeJuniors, teams, juniors, teamLeads);
        }

        return teams;
    }

    private bool IsTeamLeadPaired(int teamLeadId, Dictionary<int, int> pairings)
    {
        return pairings.ContainsKey(teamLeadId);
    }

    private void FormPair(int teamLeadId, int juniorId, Dictionary<int, int> pairings, HashSet<int> freeJuniors,
        List<Team> teams, List<Junior> juniors, List<TeamLead> teamLeads)
    {
        pairings[teamLeadId] = juniorId;
        freeJuniors.Remove(juniorId);

        var teamLead = teamLeads.First(t => t.Id == teamLeadId);
        var junior = juniors.First(j => j.Id == juniorId);
        teams.Add(new Team(teamLead, junior));
    }

    private bool DoesTeamLeadPreferNewJunior(TeamLead teamLead, int newJuniorId, int currentJuniorId)
    {
        var newJuniorPreference = teamLead.Wishlist.IndexOf(newJuniorId);
        var currentJuniorPreference = teamLead.Wishlist.IndexOf(currentJuniorId);

        return newJuniorPreference < currentJuniorPreference;
    }

    private void HandleExistingPair(int teamLeadId, int newJuniorId, Dictionary<int, int> pairings,
        HashSet<int> freeJuniors, List<Team> teams, List<Junior> juniors, List<TeamLead> teamLeads)
    {
        var currentJuniorId = pairings[teamLeadId];
        var teamLead = teamLeads.First(t => t.Id == teamLeadId);

        if (DoesTeamLeadPreferNewJunior(teamLead, newJuniorId, currentJuniorId))
        {
            teams.RemoveAll(team => team.TeamLead.Id == teamLeadId && team.Junior.Id == currentJuniorId);

            pairings[teamLeadId] = newJuniorId;
            freeJuniors.Remove(newJuniorId);
            freeJuniors.Add(currentJuniorId);

            var newJunior = juniors.First(j => j.Id == newJuniorId);
            teams.Add(new Team(teamLead, newJunior));
        }
        else
        {
            var newJunior = juniors.FirstOrDefault(j => j.Id == newJuniorId);
            if (newJunior != null) newJunior.Wishlist.Remove(teamLeadId);
        }
    }
}