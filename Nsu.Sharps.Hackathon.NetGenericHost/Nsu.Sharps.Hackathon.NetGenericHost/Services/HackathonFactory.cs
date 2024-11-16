using Microsoft.Extensions.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class HackathonFactory
{
    private readonly AmountValuesOptions _amountValuesOptions;
    private readonly DataPathOptions _dataPathOptions;

    private readonly HRDirector _hrDirector;
    private readonly IReader _reader;
    private readonly WishlistGenerator _wishlistGenerator;

    public HackathonFactory(IReader reader, WishlistGenerator wishlistGenerator, HRDirector hrDirector,
        IOptions<DataPathOptions> dataPathOptions)
    {
        _reader = reader;
        _wishlistGenerator = wishlistGenerator;
        _hrDirector = hrDirector;
        _dataPathOptions = dataPathOptions.Value;
    }

    public Hackathon CreateHackathon()
    {
        var juniors = _reader.ReadFile(_dataPathOptions.PathForJuniors).OfType<Junior>().ToList();
        var teamLeads = _reader.ReadFile(_dataPathOptions.PathForTeamLeads).OfType<TeamLead>().ToList();
        return new Hackathon(juniors, teamLeads, _wishlistGenerator, _hrDirector);
    }
}