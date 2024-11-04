using Microsoft.Extensions.Hosting;
using Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class HackathonWorker : IHostedService
{
    private readonly HRDirector _hrDirector;
    private readonly IReader _reader;
    private readonly WishlistGenerator _wishlistGenerator;

    public HackathonWorker(IReader reader, WishlistGenerator wishlistGenerator, HRDirector hrDirector)
    {
        _reader = reader;
        _wishlistGenerator = wishlistGenerator;
        _hrDirector = hrDirector;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var juniors = _reader.ReadFile(Constants.PathForJuniors).OfType<Junior>().ToList();
        var teamLeads = _reader.ReadFile(Constants.PathForTeamLeads).OfType<TeamLead>().ToList();

        var hackathon = new Hackathon(juniors, teamLeads, _wishlistGenerator, _hrDirector);

        double totalHarmonyLevel = 0;
        const int numberOfHackathons = Constants.AmountOfHackathons;

        for (var i = 1; i <= numberOfHackathons; i++)
        {
            var harmonyLevel = hackathon.RunOneHackathon();
            totalHarmonyLevel += harmonyLevel;

            Console.WriteLine($"Harmony level for hackathon #{i}: {harmonyLevel}");
        }

        var averageHarmonyLevel = totalHarmonyLevel / numberOfHackathons;
        Console.WriteLine($"Average harmony level: {averageHarmonyLevel}");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}