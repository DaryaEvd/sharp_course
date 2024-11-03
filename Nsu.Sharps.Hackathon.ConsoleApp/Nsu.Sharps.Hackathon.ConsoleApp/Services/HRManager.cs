using Nsu.Sharps.Hackathon.ConsoleApp.Models;

namespace Nsu.Sharps.Hackathon.ConsoleApp.Services;

public class HRManager
{
    private readonly HRDirector _hrDirector;
    private readonly List<Junior> _juniors;
    private readonly List<TeamLead> _teamLeads;
    private readonly WishlistGenerator _wishlistGenerator;

    public HRManager(List<Junior> juniors, List<TeamLead> teamLeads, WishlistGenerator wishlistGenerator,
        HRDirector hrDirector)
    {
        _wishlistGenerator = wishlistGenerator;
        _hrDirector = hrDirector;

        _juniors = juniors;
        _teamLeads = teamLeads;
    }

    public void CheckDataLoaded()
    {
        if (_juniors == null || !_juniors.Any() || _teamLeads == null || !_teamLeads.Any())
        {
            throw new InvalidOperationException("Data not loaded");
        }
    }

    public double RunHackathons(int numberOfHackathons)
    {
        CheckDataLoaded();

        double totalHarmonyLevel = 0;

        for (var i = 1; i <= numberOfHackathons; i++)
        {
            var hackathon = new Hackathon(_juniors, _teamLeads, _wishlistGenerator, _hrDirector);

            hackathon.RunOneHackathon();
            totalHarmonyLevel += hackathon.HarmonyLevel;

            Console.WriteLine($"Harmony level for hackathon #{i}: {hackathon.HarmonyLevel}");
        }

        return totalHarmonyLevel / numberOfHackathons;
    }
}