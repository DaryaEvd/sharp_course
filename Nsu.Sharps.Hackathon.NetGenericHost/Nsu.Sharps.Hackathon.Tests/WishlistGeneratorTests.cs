using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Services;

namespace Nsu.Sharps.Hackathon.Tests;

public class WishlistGeneratorTests
{
    [Fact]
    public void WishlistTests()
    {
        var junior1 = new Junior(1, "Junior1");
        var junior2 = new Junior(2, "Junior2");
        var junior3 = new Junior(3, "Junior3");

        var teamlead1 = new TeamLead(1, "Teamlead1");
        var teamlead2 = new TeamLead(2, "Teamlead2");
        var teamlead3 = new TeamLead(3, "Teamlead3");

        var juniors = new List<Junior> { junior1, junior2, junior3 };
        var teamLeads = new List<TeamLead> { teamlead1, teamlead2, teamlead3 };

        var wishlistGenerator = new WishlistGenerator();
        wishlistGenerator.GenerateWishlists(juniors, teamLeads);

        Assert.Equal(teamLeads.Count, teamLeads[0].Wishlist.Count);
        Assert.Equal(juniors.Count, juniors[0].Wishlist.Count);

        foreach (var junior in juniors)
        foreach (var teamLead in teamLeads)
            Assert.Contains(teamLead.Id, junior.Wishlist);

        foreach (var teamLead in teamLeads)
        foreach (var junior in juniors)
            Assert.Contains(junior.Id, teamLead.Wishlist);
    }
}