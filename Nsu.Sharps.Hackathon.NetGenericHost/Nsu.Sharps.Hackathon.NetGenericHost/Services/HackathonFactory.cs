using Microsoft.Extensions.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class HackathonFactory
{
    private readonly IDataLoader _dataLoader;

    private readonly DataPathOptions _dataPathOptions;
    private readonly HRDirector _hrDirector;
    private readonly IMatchingStrategy _matchingStrategy;
    private readonly WishlistGenerator _wishlistGenerator;

    public HackathonFactory(IDataLoader dataLoader, WishlistGenerator wishlistGenerator, HRDirector hrDirector,
        IOptions<DataPathOptions> dataPathOptions, IMatchingStrategy matchingStrategy)
    {
        _dataLoader = dataLoader;
        _wishlistGenerator = wishlistGenerator;
        _hrDirector = hrDirector;
        _dataPathOptions = dataPathOptions.Value;
        _matchingStrategy = matchingStrategy;
    }

    public Hackathon CreateHackathon()
    {
        var juniors = _dataLoader.LoadJuniors(_dataPathOptions.PathForJuniors);
        var teamLeads = _dataLoader.LoadTeamLeads(_dataPathOptions.PathForTeamLeads);

        return new Hackathon(juniors, teamLeads, _wishlistGenerator, _hrDirector, _matchingStrategy);
    }
}