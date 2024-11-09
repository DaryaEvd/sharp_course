using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;
using Nsu.Sharps.Hackathon.NetGenericHost.Models;
using Nsu.Sharps.Hackathon.NetGenericHost.Options;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class HackathonWorker : IHostedService
{
    private readonly HRDirector _hrDirector;
    private readonly IReader _reader;
    private readonly WishlistGenerator _wishlistGenerator;
    
    private readonly DataPathOptions _dataPathOptions;
    private readonly AmountValuesOptions _amountValuesOptions;
    
    public HackathonWorker(IReader reader, WishlistGenerator wishlistGenerator, HRDirector hrDirector,
        IOptions<DataPathOptions> dataPathOptions, 
        IOptions<AmountValuesOptions> amountValuesOptions)
    {   
        _reader = reader;
        _wishlistGenerator = wishlistGenerator;
        _hrDirector = hrDirector;
        _dataPathOptions = dataPathOptions.Value;
        _amountValuesOptions = amountValuesOptions.Value;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var juniors = _reader.ReadFile(_dataPathOptions.PathForJuniors).OfType<Junior>().ToList();
        var teamLeads = _reader.ReadFile(_dataPathOptions.PathForTeamLeads).OfType<TeamLead>().ToList();

        var hackathon = new Hackathon(juniors, teamLeads, _wishlistGenerator, _hrDirector);

        double totalHarmonyLevel = 0;
        int numberOfHackathons = _amountValuesOptions.AmountOfHackathons;

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