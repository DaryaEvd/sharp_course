using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class HRDirector
{
    private readonly HarmonyCalculator _harmonyCalculator;

    public HRDirector(HarmonyCalculator harmonyCalculator)
    {
        _harmonyCalculator = harmonyCalculator;
    }

    public double CalculateHarmonyLevel(List<Team> teams)
    {
        double totalHarmonyLevel = 0;

        foreach (var team in teams)
        {
            var juniorHarmonyLevel = _harmonyCalculator.CalculateHarmonyLevel(team.Junior, team.TeamLead.Id);
            var teamLeadHarmonyLevel = _harmonyCalculator.CalculateHarmonyLevel(team.TeamLead, team.Junior.Id);

            // totalHarmonyLevel += _harmonyCalculator.CalculateHarmonicMean(juniorHarmonyLevel, teamLeadHarmonyLevel);

            var harmonyLevels = new List<int> { juniorHarmonyLevel, teamLeadHarmonyLevel };
            totalHarmonyLevel += _harmonyCalculator.CalculateHarmonicMean(harmonyLevels);
        }

        return totalHarmonyLevel / teams.Count;
    }
}