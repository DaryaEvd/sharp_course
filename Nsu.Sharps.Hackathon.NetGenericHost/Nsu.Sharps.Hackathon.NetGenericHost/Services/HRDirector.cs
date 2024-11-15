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
            var juniorHarmonyLevel = _harmonyCalculator.CalculateHarmonyLevel(team.Junior, team.TeamLead.Id, true);
            var teamLeadHarmonyLevel = _harmonyCalculator.CalculateHarmonyLevel(team.TeamLead, team.Junior.Id, false);

            totalHarmonyLevel += _harmonyCalculator.CalculateHarmonicMean(juniorHarmonyLevel, teamLeadHarmonyLevel);
        }

        return totalHarmonyLevel / teams.Count;
    }
}