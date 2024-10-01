using Nsu.Sharps.Hackathon.ConsoleApp.Models;

namespace Nsu.Sharps.Hackathon.ConsoleApp.Services;

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

            var juniorHarmonyLevel = Constants.AmountOfJuniors - junior.Wishlist.IndexOf(teamLeadId);
            var teamLeadHarmonyLevel = Constants.AmountOfTeamLeads - teamLead.Wishlist.IndexOf(juniorId);

            juniorHarmonyLevel = juniorHarmonyLevel > 0 ? juniorHarmonyLevel : 0;
            teamLeadHarmonyLevel = teamLeadHarmonyLevel > 0 ? teamLeadHarmonyLevel : 0;

            double harmonicLevelMean = 0;
            switch (juniorHarmonyLevel)
            {
                case > 0 when teamLeadHarmonyLevel > 0:
                    harmonicLevelMean = Constants.AmountOfParticipantsInNumerator /
                                        ((Constants.NumeratorInCountingHarmony / juniorHarmonyLevel) +
                                         (Constants.NumeratorInCountingHarmony / teamLeadHarmonyLevel));
                    break;
                case > 0:
                    harmonicLevelMean = juniorHarmonyLevel;
                    break;
                default:
                {
                    if (teamLeadHarmonyLevel > 0)
                    {
                        harmonicLevelMean = teamLeadHarmonyLevel;
                    }

                    break;
                }
            }

            totalHarmonyLevel += harmonicLevelMean;
        }

        return totalHarmonyLevel / numberOfPairs;
    }
}