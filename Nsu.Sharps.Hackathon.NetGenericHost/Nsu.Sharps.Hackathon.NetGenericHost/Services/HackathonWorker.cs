using Microsoft.Extensions.Hosting;
using Nsu.Sharps.Hackathon.NetGenericHost.Interfaces;

namespace Nsu.Sharps.Hackathon.NetGenericHost.Services;

public class HackathonWorker : IHostedService
{
    private readonly HRManager _hrManager;
    private readonly IReader _reader;

    public HackathonWorker(IReader reader, HRManager hrManager)
    {
        _reader = reader;
        _hrManager = hrManager;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var juniors = _reader.ReadJuniors(Constants.PathForJuniors);
        var teamLeads = _reader.ReadTeamLeads(Constants.PathForTeamLeads);

        _hrManager.LoadParticipants(juniors, teamLeads);
        var averageHarmonyLevel = _hrManager.RunHackathons(Constants.AmountOfHackathons);

        Console.WriteLine(
            $"\nAverage harmony level of {Constants.AmountOfHackathons} hackathons is {averageHarmonyLevel}");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}