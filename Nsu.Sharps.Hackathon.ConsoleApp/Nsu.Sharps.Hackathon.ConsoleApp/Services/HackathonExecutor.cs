using Nsu.Sharps.Hackathon.ConsoleApp.Models;

namespace Nsu.Sharps.Hackathon.ConsoleApp.Services;

public class HackathonExecutor
{
    private readonly HRDirector _hrDirector;
    private readonly HRManager _hrManager;
    private readonly List<Junior> _juniors;
    private readonly List<TeamLead> _teamLeads;
    private readonly WishlistGenerator _wishlistGenerator;

    public HackathonExecutor(List<Junior> juniors, List<TeamLead> teamLeads, HRManager hrManager, HRDirector hrDirector)
    {
        _juniors = juniors;
        _teamLeads = teamLeads;
        _hrManager = hrManager;
        _hrDirector = hrDirector;
        _wishlistGenerator = new WishlistGenerator();
    }

    public double RunHackathons(int numberOfHackathons)
    {
        double totalHarmonyLevel = 0;

        for (var i = 1; i <= numberOfHackathons; i++)
        {
            RunOneHackathon();
            var harmonyLevel = _hrDirector.CalculateHarmonyLevel(_hrManager.GetPairings(), _juniors, _teamLeads);
            totalHarmonyLevel += harmonyLevel;

            Console.WriteLine($"Harmony level for hackathon #{i}: {harmonyLevel}");
        }

        return totalHarmonyLevel / numberOfHackathons;
    }

    private void RunOneHackathon()
    {
        _wishlistGenerator.GenerateWishlists(_juniors, _teamLeads);
        _hrManager.BuildTeams();
    }
}