using Nsu.Sharps.Hackathon.ConsoleApp.Interfaces;
using Nsu.Sharps.Hackathon.ConsoleApp.Models;
using Nsu.Sharps.Hackathon.ConsoleApp.Services;

namespace Nsu.Sharps.Hackathon.ConsoleApp;

public class Program
{
    public static void Main()
    {
        IReader reader = new ReadCsvFile();

        var participants = reader.ReadFile(Constants.PathForJuniors);
        var juniors = participants.OfType<Junior>().ToList();

        participants = reader.ReadFile(Constants.PathForTeamLeads);
        var teamLeads = participants.OfType<TeamLead>().ToList();

        var wishlistGenerator = new WishlistGenerator();
        var hrDirector = new HRDirector();

        var hrManager = new HRManager(juniors, teamLeads, wishlistGenerator, hrDirector);

        var averageHarmonyLevel = hrManager.RunHackathons(Constants.AmountOfHackathons);

        Console.WriteLine(
            $"\nAverage harmony level of {Constants.AmountOfHackathons} hackathons is {averageHarmonyLevel}");
    }
}