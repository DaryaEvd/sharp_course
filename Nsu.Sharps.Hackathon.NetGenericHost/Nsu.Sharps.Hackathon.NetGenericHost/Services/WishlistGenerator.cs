using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class WishlistGenerator
{
    private readonly Random _rand;

    public WishlistGenerator()
    {
        _rand = new Random();
    }

    public void GenerateWishlists(List<Junior> juniors, List<TeamLead> teamLeads)
    {
        foreach (var junior in juniors) junior.Wishlist = GetShuffledIds(teamLeads.Select(tl => tl.Id).ToList());

        foreach (var teamLead in teamLeads) teamLead.Wishlist = GetShuffledIds(juniors.Select(j => j.Id).ToList());
    }

    private List<int> GetShuffledIds(List<int> ids)
    {
        for (var i = ids.Count - 1; i > 0; i--)
        {
            var j = _rand.Next(i + 1);
            var temp = ids[i];
            ids[i] = ids[j];
            ids[j] = temp;
        }

        return ids;
    }
}