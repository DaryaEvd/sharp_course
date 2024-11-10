using Microsoft.Extensions.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class ParticipantFactory
{
    private readonly AmountValuesOptions _options;

    public ParticipantFactory(IOptions<AmountValuesOptions> options)
    {
        _options = options.Value;
    }

    public Participant CreateParticipant(int id, string name, string type)
    {
        return type.ToLower() switch
        {
            "junior" => new Junior(id, name, _options.AmountOfJuniors),
            "teamlead" => new TeamLead(id, name, _options.AmountOfTeamLeads)
        };
    }
}