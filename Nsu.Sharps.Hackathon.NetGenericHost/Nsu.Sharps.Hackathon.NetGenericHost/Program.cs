using Nsu.Sharps.Hackathon.NetGenericHost;
using Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Services;

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