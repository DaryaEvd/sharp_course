using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class Hackathon
{
    private readonly HRDirector _hrDirector;
    private readonly List<Junior> _juniors;
    private readonly List<TeamLead> _teamLeads;
    private readonly WishlistGenerator _wishlistGenerator;

    public Hackathon(List<Junior> juniors, List<TeamLead> teamLeads, WishlistGenerator wishlistGenerator,
        HRDirector hrDirector)
    {
        _juniors = juniors;
        _teamLeads = teamLeads;
        _wishlistGenerator = wishlistGenerator;
        _hrDirector = hrDirector;
    }

    public double RunOneHackathon()
    {
        _wishlistGenerator.GenerateWishlists(_juniors, _teamLeads);

        var hrManager = new HRManager(_juniors, _teamLeads);
        var teams = hrManager.ExecuteMatchingAlgorithm();

        return _hrDirector.CalculateHarmonyLevel(teams);
    }
}