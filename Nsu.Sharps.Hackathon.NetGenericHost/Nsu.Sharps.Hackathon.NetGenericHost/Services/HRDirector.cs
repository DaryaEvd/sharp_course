using Microsoft.Extensions.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class HRDirector
{
    private readonly CalculationDataOptions _calculationDataOptions;

    public HRDirector(
        IOptions<CalculationDataOptions> calculationDataOptions)
    {
        _calculationDataOptions = calculationDataOptions.Value;
    }

    public double CalculateHarmonyLevel(Dictionary<int, int> juniorPairings, List<Junior> juniors,
        List<TeamLead> teamLeads)
    {
        double totalHarmonyLevel = 0;
        var numberOfPairs = juniorPairings.Count;

        foreach (var pair in juniorPairings)
        {
            var juniorId = pair.Key;
            var teamLeadId = pair.Value;

            var junior = juniors.First(j => j.Id == juniorId);
            var teamLead = teamLeads.First(tl => tl.Id == teamLeadId);

            var juniorHarmonyLevel = junior.CalculateHarmonyLevel(teamLeadId);
            var teamLeadHarmonyLevel = teamLead.CalculateHarmonyLevel(juniorId);

            var harmonicMean = _calculationDataOptions.AmountOfParticipantsInNumerator /
                               (_calculationDataOptions.NumeratorInCountingHarmony / juniorHarmonyLevel +
                                _calculationDataOptions.NumeratorInCountingHarmony / teamLeadHarmonyLevel);

            totalHarmonyLevel += harmonicMean;
        }

        return totalHarmonyLevel / numberOfPairs;
    }
}