using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Services;

namespace Nsu.Sharps.Hackathon.Tests;

public class WishlistGeneratorTests
{
    [Fact]
    public void ShouldHaveSameCountAsParticipants()
    {
        var juniors = new List<Junior> { new(1, "Junior1", 3), new(2, "Junior2", 3) };
        var teamLeads = new List<TeamLead> { new(1, "TeamLead1", 2), new(2, "TeamLead2", 2) };
        var generator = new WishlistGenerator();

        generator.GenerateWishlists(juniors, teamLeads);

        Assert.Equal(teamLeads.Count, juniors[0].Wishlist.Count);
        Assert.Equal(juniors.Count, teamLeads[0].Wishlist.Count);
    }

    [Fact]
    public void ShouldContainSpecificParticipant()
    {
        var juniors = new List<Junior> { new(1, "Junior1", 3), new(2, "Junior2", 3) };
        var teamLeads = new List<TeamLead> { new(1, "TeamLead1", 2), new(2, "TeamLead2", 2) };
        var generator = new WishlistGenerator();

        generator.GenerateWishlists(juniors, teamLeads);

        Assert.Contains(1, juniors[0].Wishlist);
    }
}