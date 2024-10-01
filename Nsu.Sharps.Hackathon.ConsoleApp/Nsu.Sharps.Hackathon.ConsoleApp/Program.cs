using Nsu.Sharps.Hackathon.ConsoleApp.Interfaces;
using Nsu.Sharps.Hackathon.ConsoleApp.Services;

namespace Nsu.Sharps.Hackathon.ConsoleApp;

public class Program
{
    public static void Main()
    {
        IReader reader = new ReadCsvFile();
        WishlistGenerator wishlistGenerator = new WishlistGenerator();
        HRDirector hrDirector = new HRDirector();
        HRManager hrManager = new HRManager(reader, wishlistGenerator, hrDirector);
        
        var averageHarmonyLevel = hrManager.RunHackathons(Constants.AmountOfHackathons);

        Console.WriteLine(
            $"\n Average harmony level of {Constants.AmountOfHackathons} hackathons is {averageHarmonyLevel}");
    }
}