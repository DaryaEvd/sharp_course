using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Services;

namespace Nsu.Sharps.Hackathon.Tests;
 
public class GenerateCorrectWishlistSize
{
    [Fact]
    public void GenerateWishlistsShouldGenerateCorrectWishlistSize()
    {
         var juniors = new List<Junior> 
        {
            new Junior(1, "Junior 1"),
            new Junior(2, "Junior 2")
        };
            
        var teamLeads = new List<TeamLead>
        {
            new TeamLead(1, "TeamLead 1"),
            new TeamLead(2, "TeamLead 2"),
            new TeamLead(3, "TeamLead 3")
        };
            
        var wishlistGenerator = new WishlistGenerator();
        wishlistGenerator.GenerateWishlists(juniors, teamLeads);
 
        Assert.All(juniors, junior => 
            Assert.Equal(teamLeads.Count, junior.Wishlist.Count));

        Assert.All(teamLeads, teamLead => 
            Assert.Equal(juniors.Count, teamLead.Wishlist.Count));
    }
}