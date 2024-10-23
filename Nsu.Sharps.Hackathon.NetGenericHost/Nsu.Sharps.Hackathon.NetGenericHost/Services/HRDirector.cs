using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class HRDirector
{
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

            double harmonicLevelMean = 0;

            harmonicLevelMean = Constants.AmountOfParticipantsInNumerator /
                                (Constants.NumeratorInCountingHarmony / juniorHarmonyLevel +
                                 Constants.NumeratorInCountingHarmony / teamLeadHarmonyLevel);

            totalHarmonyLevel += harmonicLevelMean;
        }

        return totalHarmonyLevel / numberOfPairs;
    }
}