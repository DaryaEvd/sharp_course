using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Services;

namespace Nsu.Sharps.Hackathon.Tests;

public class HRManagerTests
{
    [Fact]
    public void ResultAmountOfTeamsMatchWithPredefinedAmountOfTeams()
    {
        var juniors = new List<Junior>
        {
            new(1, "Junior 1") { Wishlist = new List<int> { 1, 2, 3 } },
            new(2, "Junior 2") { Wishlist = new List<int> { 2, 3, 1 } },
            new(3, "Junior 3") { Wishlist = new List<int> { 3, 1, 2 } }
        };

        var teamLeads = new List<TeamLead>
        {
            new(1, "TeamLead 1") { Wishlist = new List<int> { 1, 2, 3 } },
            new(2, "TeamLead 2") { Wishlist = new List<int> { 2, 3, 1 } },
            new(3, "TeamLead 3") { Wishlist = new List<int> { 3, 1, 2 } }
        };

        var hrManager = new HRManager(juniors, teamLeads);

        var teams = hrManager.ExecuteMatchingAlgorithm();

        Assert.Equal(juniors.Count, teams.Count);
    }


    [Fact]
    public void OnPredefinedPreferencesShouldReturnExpectedDistribution()
    {
        var juniors = new List<Junior>
        {
            new(1, "Junior 1") { Wishlist = new List<int> { 1, 2, 3 } },
            new(2, "Junior 2") { Wishlist = new List<int> { 2, 3, 1 } },
            new(3, "Junior 3") { Wishlist = new List<int> { 3, 1, 2 } }
        };

        var teamLeads = new List<TeamLead>
        {
            new(1, "TeamLead 1") { Wishlist = new List<int> { 1, 2, 3 } },
            new(2, "TeamLead 2") { Wishlist = new List<int> { 2, 3, 1 } },
            new(3, "TeamLead 3") { Wishlist = new List<int> { 3, 1, 2 } }
        };

        var hrManager = new HRManager(juniors, teamLeads);

        var teams = hrManager.ExecuteMatchingAlgorithm();

        var expectedTeams = new List<(int TeamLeadId, int JuniorId)>
        {
            (1, 1),
            (2, 2),
            (3, 3)
        };

        foreach (var expectedTeam in expectedTeams)
            Assert.Contains(teams, team =>
                team.TeamLead.Id == expectedTeam.TeamLeadId &&
                team.Junior.Id == expectedTeam.JuniorId);
    }
}