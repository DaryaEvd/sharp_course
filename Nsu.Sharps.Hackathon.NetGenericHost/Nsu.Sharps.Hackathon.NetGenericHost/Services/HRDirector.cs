using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class HRDirector
{
    private readonly HarmonyCalculator _harmonyCalculator;

    public HRDirector(HarmonyCalculator harmonyCalculator)
    {
        _harmonyCalculator = harmonyCalculator;
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

            var juniorHarmonyLevel = _harmonyCalculator.CalculateHarmonyLevel(junior, teamLeadId, true);
            var teamLeadHarmonyLevel = _harmonyCalculator.CalculateHarmonyLevel(teamLead, juniorId, false);

            totalHarmonyLevel += _harmonyCalculator.CalculateHarmonicMean(juniorHarmonyLevel, teamLeadHarmonyLevel);
        }

        return totalHarmonyLevel / numberOfPairs;
    }
}