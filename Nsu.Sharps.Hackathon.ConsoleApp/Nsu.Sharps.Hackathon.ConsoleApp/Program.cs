using Nsu.Sharps.Hackathon.ConsoleApp.Interfaces;
using Nsu.Sharps.Hackathon.ConsoleApp.Models;
using Nsu.Sharps.Hackathon.ConsoleApp.Services;

namespace Nsu.Sharps.Hackathon.ConsoleApp;

public class Program
{
    public static void Main()
    {
        IReader reader = new ReadCsvFile();

        List<Junior> juniors = reader.ReadJuniors(Constants.PathForJuniors);
        List<TeamLead> teamLeads = reader.ReadTeamLeads(Constants.PathForTeamLeads);

        WishlistGenerator wishlistGenerator = new WishlistGenerator();
        HRDirector hrDirector = new HRDirector();

        HRManager hrManager = new HRManager(juniors, teamLeads, wishlistGenerator, hrDirector);

        var averageHarmonyLevel = hrManager.RunHackathons(Constants.AmountOfHackathons);

        Console.WriteLine(
            $"\nAverage harmony level of {Constants.AmountOfHackathons} hackathons is {averageHarmonyLevel}");
    }
}